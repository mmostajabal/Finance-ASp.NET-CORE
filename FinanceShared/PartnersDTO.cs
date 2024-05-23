using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceShared
{
    public class PartnersDTO
    {

        [Key]
        public decimal Partner_Id { get; set; }

        public string Partner_Name { get; set; }

        public decimal Parent_Partner_Id { get; set; } 

        [Range(1,20, ErrorMessage ="Fee percent should be between 1 and 20")]
        public decimal Fee_Percent {  get; set; }

        [NotMapped]
        public decimal Order { get; set; }
    }
}
