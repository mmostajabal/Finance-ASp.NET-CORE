using Microsoft.AspNetCore.Mvc;
using FinanceService.Services;
using FinanceService.Services.UploadFile;
using FinanceService.Contracts.UploadFile;
using System.IO;
using FinanceService.Contracts.TransferExcel2List;
using FinanceShared;
using FinanceService.Contracts.Partner;
using Microsoft.AspNetCore.Mvc.Rendering;
using FinanceService.Contracts.FinancialItam;
using FinanceService.Services.Partner;
using Serilog;
using Microsoft.VisualBasic;
using FinanceModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace FinanceAPP.Controllers
{
    public class FinanceItemsController : Controller
    {
        private IWebHostEnvironment _webHostEnvironment;
        private IUploadFile _uploadFileSrv;
        private ITransferExcel2List<FinancialItemsDTO> _transferExcel2ListSrv;
        private IPartner _partnerSrv;
        private IFinancialItem _financialItemSrv;
        public FinanceItemsController(IWebHostEnvironment webHostEnvironment, IUploadFile uploadFileSrv, ITransferExcel2List<FinancialItemsDTO> transferExcel2ListSrv, IPartner partnerSrv, IFinancialItem financialItemSrv)
        {
            _webHostEnvironment = webHostEnvironment;
            _uploadFileSrv = uploadFileSrv;
            _transferExcel2ListSrv = transferExcel2ListSrv;
            _partnerSrv = partnerSrv;
            _financialItemSrv = financialItemSrv;
        }

        public IActionResult Index()
        {
            TempData["header"] = "Upload Finance Items";

            return View();
        }
        /// <summary>
        /// Upload Finance Items
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(IFormFile file)
        {
            try
            {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "financeitem");

                string fileName = _uploadFileSrv.UploadFile(uploadFolder, file).GetAwaiter().GetResult();

                GlobalVariables.FINANCIAL_ITEMS_LIST = _transferExcel2ListSrv.TransferExcel2List(Path.Combine(uploadFolder, fileName));
            }
            catch (Exception ex)
            {
                Log.Error($" Upload Financial item {ex.ToString()}");
            }
            return View("FinancialItem", GlobalVariables.FINANCIAL_ITEMS_LIST);
        }
        /// <summary>
        /// List Of Financial Item
        /// </summary>
        /// <param name="financialItems"></param>
        /// <returns></returns>
        public IActionResult FinancialItem()
        {
            TempData["header"] = "Financial Items";

            return View(_financialItemSrv.GetAll());
        }
        /// <summary>
        /// Create Financial Item
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            try
            {
                ViewBag.PartnerList = _partnerSrv.GetAllPartner().Select(o => new SelectListItem { Value = o.Partner_Id.ToString(), Text = o.Partner_Id.ToString() }).ToList();
                TempData["header"] = "Create Financial Item";
            }
            catch (Exception ex)
            {
                Log.Error($" Create Financial item {ex.ToString()}");
            }
            return View();
        }

        /// <summary>
        ///  Create Financial Item
        /// </summary>
        /// <param name="financialItems"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FinancialItemsDTO financialItems)
        {
            try
            {
                TempData["header"] = "Create Financial Item";

                ViewBag.PartnerList = _partnerSrv.GetAllPartner().Select(o => new SelectListItem { Value = o.Partner_Id.ToString(), Text = o.Partner_Id.ToString() }).ToList();

                if (ModelState.IsValid)
                {
                    _financialItemSrv.Insert(financialItems);
                    TempData["success"] = "A New Financial Item has been added";
                    Log.Information($"Add new financial item {financialItems.FinancialId} by ");
                    ModelState.SetModelValue("Amount", new ValueProviderResult("", System.Globalization.CultureInfo.InvariantCulture));
                }
            }
            catch (Exception ex)
            {
                Log.Error($" Create Financial item {ex.ToString()}");
            }

            return View(financialItems);
        }

        /// <summary>
        /// Edit Financial Item
        /// </summary>
        /// <returns></returns>
        public IActionResult Edit(Guid financialId)
        {
            FinancialItemsDTO financialItem = new FinancialItemsDTO();
            try
            {
                ViewBag.PartnerList = _partnerSrv.GetAllPartner().Select(o => new SelectListItem { Value = o.Partner_Id.ToString(), Text = o.Partner_Id.ToString() }).ToList();

                financialItem = _financialItemSrv.Get(financialId);

                TempData["header"] = "Edit Financial Item";
            }
            catch (Exception ex)
            {
                Log.Error($" Edit Financial item {ex.ToString()}");
            }
            return View(financialItem);
        }

        /// <summary>
        ///  Edit Financial Item
        /// </summary>
        /// <param name="financialItems"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(FinancialItemsDTO financialItems)
        {

            TempData["header"] = "Edit Financial Item";
            try
            {
                ViewBag.PartnerList = _partnerSrv.GetAllPartner().Select(o => new SelectListItem { Value = o.Partner_Id.ToString(), Text = o.Partner_Id.ToString() }).ToList();

                if (ModelState.IsValid)
                {
                    if (_financialItemSrv.Update(financialItems))
                    {
                        Log.Information($"Update financial item {financialItems.FinancialId} by ");
                        TempData["success"] = "Update Financial Item Successfully";
                    }
                    else
                    {
                        Log.Warning($"Could not find financial item {financialItems.FinancialId} by ");
                        TempData["success"] = "We Could not Update Financial Item";
                    }
                    return RedirectToAction("FinancialItem");
                }

            }
            catch (Exception ex)
            {
                Log.Error($" Edit Financial item {ex.ToString()}");
            }
            return View(financialItems);
        }

        /// <summary>
        /// Delete Financial Item
        /// </summary>
        /// <returns></returns>
        public IActionResult Delete(Guid financialId)
        {
            FinancialItemsDTO financialItem = new FinancialItemsDTO();
            try
            {
                ViewBag.PartnerList = _partnerSrv.GetAllPartner().Select(o => new SelectListItem { Value = o.Partner_Id.ToString(), Text = o.Partner_Id.ToString() }).ToList();

                financialItem = _financialItemSrv.Get(financialId);

                TempData["header"] = "Delete Financial Item";
            }
            catch (Exception ex)
            {
                Log.Error($" Delete Financial item {ex.ToString()}");
            }
            return View(financialItem);
        }

        /// <summary>
        ///  Delete Financial Item
        /// </summary>
        /// <param name="financialItems"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(FinancialItemsDTO financialItems)
        {
            try
            {
                TempData["header"] = "Edit Financial Item";

                ViewBag.PartnerList = _partnerSrv.GetAllPartner().Select(o => new SelectListItem { Value = o.Partner_Id.ToString(), Text = o.Partner_Id.ToString() }).ToList();

                if (_financialItemSrv.Delete(financialItems.FinancialId))
                {
                    Log.Information($"Delete financial item {financialItems.FinancialId} by ");
                    TempData["success"] = "Delete Financial Item Successfully";
                }
                else
                {
                    Log.Warning($"Could not find financial item {financialItems.FinancialId} by ");
                    TempData["success"] = "We Could not Update Financial Item";
                }

                return RedirectToAction("FinancialItem");
            }
            catch (Exception ex)
            {
                Log.Error($"Delete Financial item {ex.ToString()}");
            }

            return View(financialItems);
        }

        /// <summary>
        /// Detials of Financial Item
        /// </summary>
        /// <returns></returns>
        public IActionResult Details(Guid financialId)
        {
            try
            {
                FinancialItemsDTO financialItem = new FinancialItemsDTO();
                try
                {
                    ViewBag.PartnerList = _partnerSrv.GetAllPartner().Select(o => new SelectListItem { Value = o.Partner_Id.ToString(), Text = o.Partner_Id.ToString() }).ToList();

                    financialItem = _financialItemSrv.Get(financialId);

                    TempData["header"] = "Details Of Financial Item";
                }
                catch (Exception ex)
                {
                    Log.Error($" Edit Financial item {ex.ToString()}");
                }
                return View(financialItem);
            }
            catch (Exception ex)
            {
                Log.Error($" Details Financial item {ex.ToString()}");
            }

            return RedirectToAction("FinancialItem");
        }

    }
}
