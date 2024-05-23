using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceShared
{
    public class FinancialItemsDTO
    {
        [Key]
        public Guid FinancialId { get; set; } = new Guid();

        [Required(ErrorMessage ="Partner Id is Mandetory")]
        [DisplayName("PARTNER ID ")]
        public decimal PARTNER_ID { get; set; }     
        
        [Required(ErrorMessage ="Date is Required")]      
        public DateTime Date {  get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Only positive number allowed")]
        public decimal Amount{ get; set; }
    }
}
