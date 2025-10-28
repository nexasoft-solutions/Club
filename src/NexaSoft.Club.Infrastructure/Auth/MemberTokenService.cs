using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NexaSoft.Club.Application.Abstractions.Auth;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Application.Features.Members.Services;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Auth;
using NexaSoft.Club.Domain.Features.Members;
using NexaSoft.Club.Domain.Masters.Users;
using NexaSoft.Club.Infrastructure.ConfigSettings;
using static NexaSoft.Club.Domain.Shareds.Enums;
using BC = BCrypt.Net.BCrypt;

namespace NexaSoft.Club.Infrastructure.Auth;

public class MemberTokenService : IMemberTokenService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IConfiguration _configuration;
    private readonly IGenericRepository<MemberRefreshToken> _refreshTokenRepository;
    private readonly IGenericRepository<Member> _memberRepository;
    private readonly IGenericRepository<User> _userRepository;
    private readonly IMemberQrService _qrService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JwtOptions _jwtOptions;
    private readonly ILogger<MemberTokenService> _logger;

    public MemberTokenService(
        ApplicationDbContext dbContext,
        IConfiguration configuration,
        IGenericRepository<MemberRefreshToken> refreshTokenRepository,
        IGenericRepository<Member> memberRepository,
        IMemberQrService qrService,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider,
        IOptions<JwtOptions> jwtOptions,
        ILogger<MemberTokenService> logger,
        IGenericRepository<User> userRepository)
    {
        _dbContext = dbContext;
        _configuration = configuration;
        _refreshTokenRepository = refreshTokenRepository;
        _memberRepository = memberRepository;
        _qrService = qrService;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
        _jwtOptions = jwtOptions.Value; // ← accedemos a la instancia real aquí
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<Result<MemberTokenResponse>> GenerateMemberToken(Member member, QrData qrData, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Iniciando proceso de creación de Token");
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var claims = CreateMemberClaims(member, qrData);
            var accessToken = GenerateAccessToken(claims);
            var refreshTokenStr = GenerateRefreshToken();

            // Guardar refresh token
            var refreshToken = MemberRefreshToken.Create(
                refreshTokenStr,
                member.Id,
                _dateTimeProvider.CurrentTime.AddHours(8),
                member.CreatedBy!
            );

            await _refreshTokenRepository.AddAsync(refreshToken, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("Member con ID {MemberId} se generó su token", member.Id);

            return Result.Success(new MemberTokenResponse(
                Token: accessToken,
                RefreshToken: refreshTokenStr,
                ExpiresAt: DateTime.UtcNow.AddHours(_jwtOptions.GetExpiresInt())
            ));
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear token de Member");
            return Result.Failure<MemberTokenResponse>(MemberErrores.ErrorGenerarToken);
        }
    }

     public async Task<Result<MemberTokenResponse>> GenerateUserToken(User user, QrData qrData, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Iniciando proceso de creación de Token");
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var claims = CreateUserClaims(user, qrData);
            var accessToken = GenerateAccessToken(claims);
            var refreshTokenStr = GenerateRefreshToken();

            // Guardar refresh token
            var refreshToken = MemberRefreshToken.Create(
                refreshTokenStr,
                user.MemberId ?? 0,
                _dateTimeProvider.CurrentTime.AddHours(8),
                user.CreatedBy!
            );

            await _refreshTokenRepository.AddAsync(refreshToken, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("User con ID {UserId} se generó su token", user.Id);

            return Result.Success(new MemberTokenResponse(
                Token: accessToken,
                RefreshToken: refreshTokenStr,
                ExpiresAt: DateTime.UtcNow.AddHours(_jwtOptions.GetExpiresInt())
            ));
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear token de Member");
            return Result.Failure<MemberTokenResponse>(MemberErrores.ErrorGenerarToken);
        }
    }

    public async Task<Result<MemberTokenResponse>> GenerateMemberTokenWithPassword(User user, string password, QrData qrData, CancellationToken cancellationToken)
    {
        // Validar password antes de generar token
        if (!BC.Verify(password, user.Password))
            return Result.Failure<MemberTokenResponse>(MemberErrores.PasswordInvalido);
        // Si la validación es exitosa, generar el token    
        return await GenerateUserToken(user, qrData, cancellationToken);
    }

    public async Task<Result<MemberTokenResponse>> RefreshMemberToken(string refreshToken, CancellationToken cancellationToken)
    {
        var tokenEntity = await GetByRefreshTokenAsync(refreshToken, cancellationToken);

        if (tokenEntity.Value == null || !tokenEntity.Value.IsActive())
            return Result.Failure<MemberTokenResponse>(MemberErrores.RefreshTokenInvalido);

        var member = await _memberRepository.GetByIdAsync(tokenEntity.Value.MemberId, cancellationToken);
        if (member == null)
            return Result.Failure<MemberTokenResponse>(MemberErrores.NoEncontrado);

        try
        {
            _logger.LogInformation("Iniciando proceso de creación de RefreshToken");
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // Invalidar refresh token anterior
            tokenEntity.Value.Revoke();

            

            // Generar nuevo QR
            var qrData = await _qrService.GenerateOrGetMonthlyQr(member.Id, cancellationToken);

            // Generar nuevos tokens
            var claims = CreateMemberClaims(member, qrData);
            var newAccessToken = GenerateAccessToken(claims);
            var newRefreshTokenStr = GenerateRefreshToken();

            var newRefreshToken = MemberRefreshToken.Create(
                newRefreshTokenStr,
                member.Id,
                _dateTimeProvider.CurrentTime.AddHours(8),
                member.CreatedBy!
            );

            await _refreshTokenRepository.AddAsync(newRefreshToken, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("Member con ID {MemberId} se generó su Refreshtoken", member.Id);

            return Result.Success(new MemberTokenResponse(
                Token: newAccessToken,
                RefreshToken: newRefreshTokenStr,
                ExpiresAt: DateTime.UtcNow.AddHours(_jwtOptions.GetExpiresInt())
            ));
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear token de Member");
            return Result.Failure<MemberTokenResponse>(MemberErrores.ErrorRefreshToken);
        }
    }

    public async Task<Result<MemberRefreshToken>> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var tokenEntity = await _dbContext.Set<MemberRefreshToken>()
            .Include(rt => rt.Member)
            .FirstOrDefaultAsync(t => t.Token == refreshToken && t.DeletedAt == null, cancellationToken);

        return Result.Success(tokenEntity!);
    }

    private List<Claim> CreateMemberClaims(Member member, QrData qrData)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, member.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, member.Email ?? ""),
            new Claim(JwtRegisteredClaimNames.UniqueName, member.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),

            //new Claim("userName", member.UserName!),
            new Claim("FirstName", member.FirstName!),
            new Claim("FullName", $"{member.FirstName} {member.LastName}"),
            new Claim("userType", "Member"),
            new Claim("userName", member.Dni!),
            new Claim("memberId", member.Id.ToString()),
            new Claim("dni", member.Dni!),
            new Claim("email", member.Email ?? ""),
            new Claim("qrCode", qrData.QrCode),
            new Claim("qrImageUrl", qrData.QrImageUrl ?? ""),
            new Claim("qrExpiration", qrData.ExpirationDate.ToString("O")),
            new Claim("memberType", member.MemberType?.TypeName ?? "Regular"),
            new Claim("membershipStatus", ((StatusEnum)member.StatusId!).ToString())
        };

        // Si tienes roles/permissions para members en el futuro, los agregas aquí
        // claims.Add(new Claim("roles", JsonSerializer.Serialize(memberRoles)));

        return claims;
    }

    private List<Claim> CreateUserClaims(User user, QrData qrData)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user.Email ?? ""),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),

            new Claim("FirstName", user.FirstName!),
            new Claim("FullName", $"{user.FullName}"),
            new Claim("userType", "User"),
            new Claim("userName", user.UserName!),
            new Claim("memberId", user.Member!.Id.ToString()),
            new Claim("dni", user.Dni!),
            new Claim("email", user.Email ?? ""),
            new Claim("qrCode", qrData.QrCode),
            new Claim("qrImageUrl", qrData.QrImageUrl ?? ""),
            new Claim("qrExpiration", qrData.ExpirationDate.ToString("O")),
            new Claim("memberType", user.Member.MemberType?.TypeName ?? "Regular"),
            new Claim("membershipStatus", user.Member.StatusId.ToString()!)
        };

        // Si tienes roles/permissions para members en el futuro, los agregas aquí
        // claims.Add(new Claim("roles", JsonSerializer.Serialize(memberRoles)));

        return claims;
    }

    private string GenerateAccessToken(List<Claim> claims)
    {
        var secret = _configuration["Jwt:Secret"]!;
        var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(secret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Issuer"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(_jwtOptions.GetExpiresInt()),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        var bytes = RandomNumberGenerator.GetBytes(64);
        return Convert.ToBase64String(bytes);
    }
  
}