using FinanceService.Contracts.PartnerFinancialItem;
using FinanceService.Services.PartnerFinancialItems;
using FinanceShared;
using Microsoft.AspNetCore.Mvc;

namespace FinanceAPP.Controllers
{
    public class ReportController : Controller
    {
        private IPartnerFinancialItems _partnerFinancialItemsSrv;

        public ReportController(IPartnerFinancialItems partnerFinancialItemsSrv)
        {
            _partnerFinancialItemsSrv = partnerFinancialItemsSrv;

        }
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Total Amounts Member
        /// </summary>
        /// <returns></returns>
        public IActionResult TotalMembers()
        {

            TempData["header"] = "Total Members";
            List<PartnerFinancialItemDTO> partnerFinancial = _partnerFinancialItemsSrv.SumMemberTeam();
            return View(partnerFinancial);
        }
        /// <summary>
        /// Total Benfit of members
        /// </summary>
        /// <returns></returns>
        public IActionResult BenfitMembers()
        {

            TempData["header"] = "Total Benfit Members";
            List<PartnerFinancialItemDTO> partnerFinancial = _partnerFinancialItemsSrv.TotalBenfitMemberTeam();
            return View(partnerFinancial);
        }
        /// <summary>
        /// TotalParentMemberTeam
        /// </summary>
        /// <returns></returns>
        public IActionResult TotalParentMemberTeam()
        {
            TempData["header"] = "Total Members";
            List<PartnerFinancialItemDTO> partnerFinancial = _partnerFinancialItemsSrv.SumParentMemberTeam();
            return View(partnerFinancial);

        }

    }
}
