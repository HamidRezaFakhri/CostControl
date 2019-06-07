namespace CostControl.Presentation.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using CostControl.BusinessEntity.Models.CostControl;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class SaleCostPointController : BaseController
    {
        public IActionResult SaleCostPointList(string param, int pageNumber, int pageSize)
        {
            ViewData["title"] = Helper.GetEntityTitle<SaleCostPoint>(EnumTitle.List);

            return View(Helper.GetServiceResponse<SaleCostPoint>("Get?PageNumber=1&PageSize=1000&searchKey=null&SortOrder=id&token=1"));
        }

        public IActionResult AddSaleCostPoint()
        {
            ViewData["title"] = Helper.GetEntityTitle<SaleCostPoint>(EnumTitle.Add);

            ViewBag.CostPoints = GetCostPointList();

            ViewBag.SalePoints = GetSalePointList();

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSaleCostPoint(SaleCostPoint SaleCostPoint)
        {
            if (ModelState.IsValid)
            {
                var postResult = Helper.PostValueToSevice<SaleCostPoint>("POST", SaleCostPoint);

                return Json(new { success = postResult.result, message = postResult.message });
            }

            return Json(new
            {
                model = SaleCostPoint,
                success = false,
                message = ModelState
                .Values
                .FirstOrDefault(e => e.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                .Errors
                .FirstOrDefault()
                .ErrorMessage ?? "Model Is Not Vald!"
            });
        }

        public IActionResult EditSaleCostPoint(long id)
        {
            ViewData["title"] = Helper.GetEntityTitle<SaleCostPoint>(EnumTitle.Edit);

            var model = GetSaleCostPointById(id);

            ViewBag.CostPoints = GetCostPointList(model.CostPointId);

            ViewBag.SalePoints = GetSalePointList(model.SalePointId);

            return PartialView(model);
        }

        [HttpPost]
        public IActionResult EditSaleCostPoint(long id, SaleCostPoint SaleCostPoint)
        {
            if (ModelState.IsValid)
            {
                SaleCostPoint.State = BusinessEntity.Models.Base.Enums.ObjectState.Active;

                var postResult = Helper.PostValueToSevice<SaleCostPoint>("PUT?id=" + SaleCostPoint.Id.ToString(), SaleCostPoint);

                return Json(new { success = postResult.result, message = postResult.message });
            }

            return Json(new
            {
                model = SaleCostPoint,
                success = false,
                message = ModelState
                .Values
                .FirstOrDefault(e => e.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                .Errors
                .FirstOrDefault()
                .ErrorMessage ?? "Model Is Not Vald!"
            });
        }

        public IActionResult DeleteSaleCostPoint(long id)
        {
            ViewData["title"] = Helper.GetEntityTitle<SaleCostPoint>(EnumTitle.Delete);

            return PartialView(GetSaleCostPointById(id));
        }

        [HttpPost]
        public IActionResult DeleteSaleCostPoint(SaleCostPoint SaleCostPoint)
        {
            var postResult = Helper.PostValueToSevice<SaleCostPoint>("Delete?id=" + SaleCostPoint.Id.ToString(), SaleCostPoint);

            return Json(new { success = postResult.result, message = postResult.message });
        }

        private SaleCostPoint GetSaleCostPointById(long id)
        => (Helper.GetServiceResponse<SaleCostPoint>("GetById?id=" + id.ToString()).data as List<SaleCostPoint>)
                .FirstOrDefault();

        private CostPoint GetCostPointById(long id)
        {
            return (Helper.GetServiceResponse<CostPoint>("GetById?id=" + id.ToString()).data as List<CostPoint>)
                .FirstOrDefault();
        }

        private SalePoint GetSalePointById(long id)
        {
            return (Helper.GetServiceResponse<SalePoint>("GetById?id=" + id.ToString()).data as List<SalePoint>)
                .FirstOrDefault();
        }

        private IEnumerable<SalePoint> GetSalePoints()
        => Helper.GetServiceResponseList<SalePoint>("Get?PageNumber=1&PageSize=1000&searchKey=null&SortOrder=id&token=1");

        private IEnumerable<SelectListItem> GetSalePointList(long? selected = null)
        => GetSalePoints()
                .OrderBy(o => o.Name)
                .Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                    Selected = selected == null ? false : c.Id == selected
                })
                .ToList();

        private IEnumerable<CostPoint> GetCostPoints()
        => Helper.GetServiceResponseList<CostPoint>("Get?PageNumber=1&PageSize=1000&searchKey=null&SortOrder=id&token=1");
        
        private IEnumerable<SelectListItem> GetCostPointList(long? selected = null)
        => GetCostPoints()
                .OrderBy(o => o.Name)
                .Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                    Selected = selected == null ? false : c.Id == selected
                })
                .ToList();
    }
}