using FinanceShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceShared
{
    public static class GlobalVariables
    {
        public static IList<PartnersDTO> PARTNER_LIST = new List<PartnersDTO>();
        
        public static IList<FinancialItemsDTO> FINANCIAL_ITEMS_LIST  = new List<FinancialItemsDTO>();
    }
}
