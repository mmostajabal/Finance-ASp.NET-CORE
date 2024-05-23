using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceShared
{
    public class PartnerFinancialItemDTO
    {
        public decimal Partner_Id { get; set; }

        public decimal Parent_Partner_Id { get; set; }

        public decimal Fee_Percent { get; set; }

        public DateTime Date { get; set; }
        public decimal Amount { get; set; }

    }
}
