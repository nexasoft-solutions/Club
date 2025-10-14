using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Features.Members.Services;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.Members;
using NexaSoft.Club.Domain.Masters.Users;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Infrastructure.services;

public class MemberUserBackgroundGenerator : IMemberUserBackgroundGenerator
{
    private readonly IGenericRepository<Member> _memberRepository;
    private readonly IGenericRepository<User> _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<MemberUserBackgroundGenerator> _logger;

    public MemberUserBackgroundGenerator(
        IGenericRepository<Member> memberRepository,
        IGenericRepository<User> userRepository,
        IUnitOfWork unitOfWork,
        ILogger<MemberUserBackgroundGenerator> logger)
    {
        _memberRepository = memberRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<long?> GenerateMemberUserAsync(long memberId,long userTypeId, string createdBy, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Generando usuario y activando miembro para member {MemberId}", memberId);

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var member = await _memberRepository.GetByIdAsync(memberId, cancellationToken);
            if (member == null)
            {
                _logger.LogWarning("No se encontró el miembro con ID {MemberId}", memberId);
                return null;
            }

            var fullName = UserService.CreateFullName(member.LastName ?? "", member.FirstName ?? "");
            var userName = UserService.CreateUserName(member.LastName ?? "", member.FirstName ?? "");

            bool existsUser = await _userRepository.ExistsAsync(u =>
                u.FullName == fullName ||
                u.UserName == userName ||
                u.Email == member.Email ||
                u.Dni == member.Dni,
                cancellationToken);

            long? createdUserId = null;

            if (!existsUser)
            {
                var user = User.Create(
                    member.FirstName,
                    member.LastName,
                    member.Email,
                    member.Dni,
                    member.Phone,
                    userTypeId,
                    member.BirthDate,
                    memberId,
                    (int)EstadosEnum.Activo,
                    DateTime.UtcNow,
                    createdBy
                );
                await _userRepository.AddAsync(user, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                createdUserId = user.Id;
                _logger.LogInformation("Usuario creado para member {MemberId}", memberId);
            }
            else
            {
                _logger.LogWarning("Usuario ya existe para member {MemberId}, no se crea nuevo", memberId);
            }

            // MARCAR MIEMBRO COMO ACTIVO
            member.MarkAsActive();
            await _memberRepository.UpdateAsync(member);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("Miembro {MemberId} activado correctamente", memberId);
            return createdUserId;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error creando usuario / activando miembro {MemberId}", memberId);
            throw;
        }
    }
}