using NexaSoft.Club.Domain.Features.Reservations;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.Specifications;

public class ActiveReservationsSpecification: BaseSpecification<Reservation>
{
    public ActiveReservationsSpecification(long memberId, DateOnly currentTime) : base()
    {
        AddCriteria(x => x.MemberId == memberId &&
                        x.StateReservation == (int)EstadosEnum.Activo &&
                        x.Date > currentTime);// &&
                        /*x.Status != (int)StatusEnum.Ca &&
                        x.Status != "Completed");*/
    }
}