using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Storages;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.Members;
using NexaSoft.Club.Domain.Masters.Users;

namespace NexaSoft.Club.Application.Features.Members.Queries.GetMemberQr;

public class GetMemberQrQueryHandler(
    IGenericRepository<User> _userRepository,
    IFileStorageService _storageService
) : IQueryHandler<GetMemberQrQuery, string>
{
    public async Task<Result<string>> Handle(GetMemberQrQuery query, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(query.UserId, cancellationToken);

        if (user is null)
            return Result.Failure<string>(MemberErrores.NoEncontrado);

        if (string.IsNullOrWhiteSpace(user.QrCode))
            return Result.Failure<string>(MemberErrores.ErrorQrNoGenerado);

        var url = await _storageService.GetPresignedUrlAsync(user.QrCode);
        return Result.Success(url);
    }
}
