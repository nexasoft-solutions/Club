namespace NexaSoft.Club.Application.Storages
{
    using NexaSoft.Club.Domain.ServicesModel.Reniec;

    public interface IReniecService
    {
        Task<ReniecDniResponse?> GetDniInfoAsync(string dni);
        Task<ReniecRucResponse?> GetRucInfoAsync(string ruc);
    }
}
