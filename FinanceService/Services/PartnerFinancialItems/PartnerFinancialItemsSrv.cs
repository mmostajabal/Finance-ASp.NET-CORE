using FinanceService.Contracts.PartnerFinancialItem;
using FinanceService.Services.FinanceItem;
using FinanceService.Services.Partner;
using FinanceShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceService.Services.PartnerFinancialItems
{
    public class PartnerFinancialItemsSrv : IPartnerFinancialItems
    {
        private PartnerSrv _partnerSrv;
        private FinancialItemSrv _financialItamSrv;
        public PartnerFinancialItemsSrv(PartnerSrv partnerSrv, FinancialItemSrv financialItamSrv)
        {
            _partnerSrv = partnerSrv;
            _financialItamSrv = financialItamSrv;
        }
        /// <summary>
        /// GetPartnerFinancialItem
        /// </summary>
        /// <returns></returns>
        public List<PartnerFinancialItemDTO> GetPartnerFinancialItem()
        {
            List<PartnerFinancialItemDTO> partnerFinancialItems = new List<PartnerFinancialItemDTO>();


             partnerFinancialItems = _partnerSrv.GetPartnerMember().Join(_financialItamSrv.GetAll(), arg=>arg.Partner_Id , arg=>arg.PARTNER_ID
            , (partner, financeitems)=>new PartnerFinancialItemDTO (){ Fee_Percent= partner.Fee_Percent, 
                    Amount= financeitems.Amount,Partner_Id = partner.Partner_Id, 
                    Parent_Partner_Id = partner.Parent_Partner_Id, Date = financeitems.Date}).ToList();

            return partnerFinancialItems;
        }

        /// <summary>
        /// SumMemberTeam
        /// </summary>
        /// <returns></returns>
        public List<PartnerFinancialItemDTO> SumMemberTeam()
        {
            List<PartnerFinancialItemDTO> partnerFinancialItems = new List<PartnerFinancialItemDTO>();


            partnerFinancialItems = _partnerSrv.GetPartnerMember().Join(_financialItamSrv.GetAll(), arg => arg.Partner_Id, arg => arg.PARTNER_ID
           , (partner, financeitems) => new PartnerFinancialItemDTO()
           {               
               Amount = financeitems.Amount,               
               Parent_Partner_Id = partner.Parent_Partner_Id,               
           }).GroupBy(g=>g.Parent_Partner_Id).Select(g=>new PartnerFinancialItemDTO() {Parent_Partner_Id= g.Key, Amount = g.Sum(s=>s.Amount)}).ToList();

            return partnerFinancialItems;
        }
        /// <summary>
        /// SumParentMemberTeam
        /// </summary>
        /// <returns></returns>
        public List<PartnerFinancialItemDTO> SumParentMemberTeam()
        {
            List<PartnerFinancialItemDTO> partnerFinancialItems = new List<PartnerFinancialItemDTO>();


            partnerFinancialItems = _partnerSrv.GetPartnerMember().Join(_financialItamSrv.GetAll(), arg => arg.Partner_Id, arg => arg.PARTNER_ID
           , (partner, financeitems) => new PartnerFinancialItemDTO()
           {
               Amount = financeitems.Amount,
               Parent_Partner_Id = partner.Parent_Partner_Id,
           }).Union(_partnerSrv.GetPartnerParents().Join(_financialItamSrv.GetAll(), arg => arg.Partner_Id, arg => arg.PARTNER_ID
           , (partner, financeitems) => new PartnerFinancialItemDTO()
           {
               Amount = financeitems.Amount,
               Parent_Partner_Id = partner.Partner_Id,
           }))
           .GroupBy(g => g.Parent_Partner_Id).Select(g => new PartnerFinancialItemDTO() { Parent_Partner_Id = g.Key, Amount = g.Sum(s => s.Amount) }).ToList();

            return partnerFinancialItems;
        }

        /// <summary>
        /// TotalBenfitMemberTeam
        /// </summary>
        /// <returns></returns>
        public List<PartnerFinancialItemDTO> TotalBenfitMemberTeam()
        {
            List<PartnerFinancialItemDTO> partnerFinancialItems = new List<PartnerFinancialItemDTO>();


            partnerFinancialItems = _partnerSrv.GetPartnerMember().Join(_financialItamSrv.GetAll(), arg => arg.Partner_Id, arg => arg.PARTNER_ID
           , (partner, financeitems) => new PartnerFinancialItemDTO()
           {
               Amount = financeitems.Amount * partner.Fee_Percent,
               Parent_Partner_Id = partner.Parent_Partner_Id,
           }).GroupBy(g => g.Parent_Partner_Id).Select(g => new PartnerFinancialItemDTO() { Parent_Partner_Id = g.Key, Amount = g.Sum(s => s.Amount / 100) }).ToList();

            return partnerFinancialItems;
        }

        
    }
}
