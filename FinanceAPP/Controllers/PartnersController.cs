using FinanceService.Contracts.Partner;
using FinanceShared;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace FinanceAPP.Controllers
{
    public class PartnersController : Controller
    {
        private IPartner _partnerSrv;
        public PartnersController(IPartner partnerSrv)
        {
            _partnerSrv = partnerSrv;
        }

        /// <summary>
        /// Generate Data
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            try
            {
                if (_partnerSrv.GetAllPartner().Count == 0)
                {
                    _partnerSrv.CreatePartner(10, 40, 3, 1, 20);
                    Log.Information($"Create {_partnerSrv.GetAllPartner().Count} Partner ");
                }
                return View(_partnerSrv.GetAllPartner().Select(o => new PartnersDTO
                {
                    Fee_Percent = o.Fee_Percent,
                    Partner_Id = o.Partner_Id,
                    Partner_Name = o.Partner_Name,
                    Parent_Partner_Id = o.Parent_Partner_Id,
                    Order = (o.Parent_Partner_Id == 0 ? o.Partner_Id : o.Parent_Partner_Id)
                }).OrderBy(o => o.Order).ToList());
            }
            catch (Exception ex)
            {
                Log.Error($" Details Financial item {ex.ToString()}");
            }

            return View(new PartnersDTO());
        }
    }
}
