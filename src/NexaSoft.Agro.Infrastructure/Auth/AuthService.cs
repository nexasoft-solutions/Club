using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NexaSoft.Agro.Application.Abstractions.Auth;
using NexaSoft.Agro.Application.Masters.Auths.Queries.AuthToken;
using NexaSoft.Agro.Application.Masters.Users;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Auth;
using NexaSoft.Agro.Domain.Masters.Roles;
using NexaSoft.Agro.Domain.Masters.Users;
using BC = BCrypt.Net.BCrypt;

namespace NexaSoft.Agro.Infrastructure.Abstractions.Auth;

public class AuthService(
    ApplicationDbContext _dbContext,
    IConfiguration _configuration,
    IGenericRepository<RefreshToken> _repository,
    IGenericRepository<User> _userRepository,
    IUserRoleRepository _userRoleRepository,
    IUnitOfWork _unitOfWork) : IAuthService
{

    public async Task<Result<User?>> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Set<User>()
            .Where(u => u.Email == email && u.FechaEliminacion == null)
            .SingleOrDefaultAsync(cancellationToken);

        return Result.Success(user);
    }

    public async Task<Result<RefreshToken>> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var tokenEntity = await _dbContext.Set<RefreshToken>()
            .FirstOrDefaultAsync(t => t.Token == refreshToken, cancellationToken);

        return Result.Success(tokenEntity!);
    }

    public async Task<Result<User?>> GetByUserNameAsync(string userName, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Set<User>()
            .Where(u => u.UserName == userName && u.FechaEliminacion == null)
            .SingleOrDefaultAsync(cancellationToken);

        return Result.Success(user);
    }

    public async Task<Result<AuthTokenResponse>> Login(string userName, string password, CancellationToken cancellationToken)
    {
        var entity = await GetByUserNameAsync(userName, cancellationToken);
        if (entity.Value is null)
            return Result.Failure<AuthTokenResponse>(UserErrores.NoEncontrado);

        if (!BC.Verify(password, entity.Value.Password))
            return Result.Failure<AuthTokenResponse>(UserErrores.PasswordInvalido);

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // Obtener roles
            var roles = await _userRoleRepository.GetUserRolesAsync(entity.Value.Id, cancellationToken);
            var roleDefault = roles.FirstOrDefault(r => r.IsDefault);

            if (roleDefault is null)
                return Result.Failure<AuthTokenResponse>(UserErrores.ErrorUserSinRolDefault);

            // Obtener permisos del rol por defecto
            var permissions = await _userRoleRepository.GetPermissionsForDefaultRoleAsync(entity.Value.Id, roleDefault.Id, cancellationToken);

            var accessToken = GenerateAccessToken(entity.Value, roles, permissions, roleDefault.Id);
            var refreshTokenStr = GenerateRefreshToken();

            var refresh = RefreshToken.Create(refreshTokenStr, entity.Value.Id, DateTime.UtcNow.AddHours(8));
            await _repository.AddAsync(refresh, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Success(new AuthTokenResponse(accessToken, refreshTokenStr));
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            return Result.Failure<AuthTokenResponse>(AuthErrores.LoginError);
        }
    }
    public async Task<Result<AuthTokenResponse>> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var tokenEntity = await GetByRefreshTokenAsync(refreshToken, cancellationToken);

        if (tokenEntity.Value == null || !tokenEntity.Value.IsActive())
            return Result.Failure<AuthTokenResponse>(RefreshTokenErrores.Invalido);

        var user = await _userRepository.GetByIdAsync(tokenEntity.Value.UserId, cancellationToken);
        if (user == null)
            return Result.Failure<AuthTokenResponse>(UserErrores.NoEncontrado);

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            tokenEntity.Value.Revoke();

            var newRefreshTokenStr = GenerateRefreshToken();
            var newRefreshToken = RefreshToken.Create(newRefreshTokenStr, user.Id, DateTime.UtcNow.AddHours(8));
            await _repository.AddAsync(newRefreshToken, cancellationToken);

            // Obtener roles
            var roles = await _userRoleRepository.GetUserRolesAsync(user.Id, cancellationToken);
            var roleDefault = roles.FirstOrDefault(r => r.IsDefault);

            if (roleDefault is null)
                return Result.Failure<AuthTokenResponse>(UserErrores.ErrorUserSinRolDefault);

            // Obtener permisos del rol por defecto
            var permissions = await _userRoleRepository.GetPermissionsForDefaultRoleAsync(user.Id, roleDefault.Id, cancellationToken);

            var accessToken = GenerateAccessToken(user, roles, permissions, roleDefault.Id);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Success(new AuthTokenResponse(accessToken, newRefreshTokenStr));
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            return Result.Failure<AuthTokenResponse>(AuthErrores.RefreshTokenError);
        }
    }

    private string GenerateAccessToken(User user, List<UserRoleResponse> roles, List<string> permissions, Guid activeRoleId)
    {
        var secret = _configuration["Jwt:Secret"]!;
        var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(secret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
        new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),

        new Claim("userName", user.UserName!),
        new Claim("userNombres", user.UserNombres!),
        new Claim("userNombreCompleto", user.NombreCompleto!),
        new Claim("roleActive", activeRoleId.ToString())
    };

        /*foreach (var role in roles)
        {
            claims.Add(new Claim("roles", role.Name!));
            if (role.IsDefault)
                claims.Add(new Claim("roleDefault", role.Id.ToString())); // o role.Id.ToString() si prefieres
        }*/

        // Agrega los roles como una lista serializada en JSON
        var rolesInfo = roles.Select(role => new { id = role.Id, name = role.Name }).ToList();
        claims.Add(new Claim("roles", JsonSerializer.Serialize(rolesInfo)));

        // Agrega solo el ID del rol por defecto como antes
        var defaultRole = roles.FirstOrDefault(r => r.IsDefault);
        if (defaultRole != null)
            claims.Add(new Claim("roleDefault", defaultRole.Id.ToString()));


        foreach (var permission in permissions)
        {
            claims.Add(new Claim("permission", permission));
        }

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Issuer"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(int.Parse(_configuration["Jwt:Expires"]!)),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /*public async Task<string> GenerateAccessToken(User user, Guid activeRoleId, CancellationToken cancellationToken)
    {
        var roles = await _userRoleRepository.GetUserRolesAsync(user.Id, cancellationToken);
        var activeRole = roles.FirstOrDefault(r => r.Id == activeRoleId);

        if (activeRole is null)
            throw new InvalidOperationException("El usuario no posee el rol activo solicitado.");

        var permissions = await _userRoleRepository.GetPermissionsForDefaultRoleAsync(user.Id, activeRoleId, cancellationToken);

        return GenerateAccessToken(user, roles, permissions);
    }*/

    public async Task<string> GenerateAccessToken(User user, Guid activeRoleId, CancellationToken cancellationToken)
    {
        var roles = await _userRoleRepository.GetUserRolesAsync(user.Id, cancellationToken);
        var activeRole = roles.FirstOrDefault(r => r.Id == activeRoleId);

        if (activeRole is null)
            throw new InvalidOperationException("El usuario no posee el rol activo solicitado.");

        var permissions = await _userRoleRepository.GetPermissionsForDefaultRoleAsync(user.Id, activeRoleId, cancellationToken);

        return GenerateAccessToken(user, roles, permissions, activeRoleId); 
    }

    private string GenerateRefreshToken()
    {
        var bytes = RandomNumberGenerator.GetBytes(64);
        return Convert.ToBase64String(bytes);
    }

    public async Task<Result<AuthTokenResponse>> ChangeActiveRoleAsync(Guid userId, Guid newRoleId, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        if (user == null)
            return Result.Failure<AuthTokenResponse>(UserErrores.NoEncontrado);

        var roles = await _userRoleRepository.GetUserRolesAsync(user.Id, cancellationToken);
        var selectedRole = roles.FirstOrDefault(r => r.Id == newRoleId);

        if (selectedRole == null)
            return Result.Failure<AuthTokenResponse>(UserErrores.ErrorUserSinRol);

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // Obtener permisos para el nuevo rol
            var permissions = await _userRoleRepository.GetPermissionsForDefaultRoleAsync(user.Id, selectedRole.Id, cancellationToken);

            // Crear nuevo access token
            var accessToken = GenerateAccessToken(user, roles, permissions,newRoleId);

            // Revocar refresh tokens anteriores

            var activeTokens = await _dbContext.Set<RefreshToken>()
                .Where(rt => rt.UserId == userId && rt.FechaEliminacion == null)
                .ToListAsync(cancellationToken);

            activeTokens = activeTokens.Where(rt => rt.IsActive()).ToList();

            foreach (var token in activeTokens)
                token.Revoke();

            // Crear nuevo refresh token
            var refreshTokenStr = GenerateRefreshToken();
            var newRefreshToken = RefreshToken.Create(refreshTokenStr, user.Id, DateTime.UtcNow.AddHours(8));
            await _repository.AddAsync(newRefreshToken, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Success(new AuthTokenResponse(accessToken, refreshTokenStr));
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            // Agrega logging para ver cuál es el problema real
            Console.WriteLine($"Error al cambiar de rol: {ex.Message}");
            Console.WriteLine($"StackTrace: {ex.StackTrace}");
            return Result.Failure<AuthTokenResponse>(AuthErrores.LoginError);
        }
    }
}
