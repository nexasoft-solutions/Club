using NexaSoft.Club.Domain.Features.Members;

namespace NexaSoft.Club.Domain.Specifications;

public class MembersNeedingQrRenewalSpec: BaseSpecification<Member>
{
    public MembersNeedingQrRenewalSpec() : base()
    {
        // Members que no tienen QR o cuyo QR expira en menos de 15 días
        // Y que están activos y al día en pagos
        AddCriteria(m => 
            (m.QrExpiration == null || 
             m.QrExpiration <= DateOnly.FromDateTime(DateTime.Now.AddDays(15))) &&
            m.Status == "Completed");
        
        AddInclude(m => m.QrHistory);
    }
}
