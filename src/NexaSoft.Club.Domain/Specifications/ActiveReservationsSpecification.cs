using NexaSoft.Club.Domain.Features.Reservations;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.Specifications;

public class ActiveReservationsSpecification: BaseSpecification<Reservation>
{
    public ActiveReservationsSpecification(long memberId, DateTime currentTime) : base()
    {
        AddCriteria(x => x.MemberId == memberId && 
                        x.StateReservation == (int)EstadosEnum.Activo &&
                        x.StartTime > currentTime &&
                        x.Status != "Cancelled" &&
                        x.Status != "Completed");
    }
}