using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceModels
{
    public class FinancialItems
    {
        [Key]
        public Guid FinancialId { get; set; } = new Guid();
        public decimal PARTNER_ID { get; set; }        
        public DateOnly Date {  get; set; }
        public decimal Amount{ get; set; } = 0;
    }
}
