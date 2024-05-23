using FinanceShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceService.Contracts.PartnerFinancialItem
{
    public  interface IPartnerFinancialItems
    {
        public List<PartnerFinancialItemDTO> GetPartnerFinancialItem();
        public List<PartnerFinancialItemDTO> SumMemberTeam();
        public List<PartnerFinancialItemDTO> SumParentMemberTeam();
        public List<PartnerFinancialItemDTO> TotalBenfitMemberTeam();
    }
}
