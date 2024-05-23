using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceModels
{
    public class Partners
    {

        [Key]
        public decimal Partner_Id { get; set; }

        [Required]
        public string Partner_Name { get; set; }

        public decimal Parent_Partner_Id { get; set; } = 0;

        [Range(1,20, ErrorMessage ="Fee percent should be between 1 and 20")]
        public decimal Fee_Percent {  get; set; }

        public Partners(decimal partner_id, string partner_name , decimal parent_partner_id, decimal fee_percent)
        {
            Partner_Id = partner_id;

            Partner_Name = partner_name;

            Parent_Partner_Id = parent_partner_id;

            Fee_Percent = fee_percent;
        }
    }
}
