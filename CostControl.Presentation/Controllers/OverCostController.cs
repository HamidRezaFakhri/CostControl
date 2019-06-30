namespace CostControl.Presentation.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using CostControl.BusinessEntity.Models.CostControl;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class OverCostController : BaseController
    {
        public IActionResult OverCostList(string param)
        {
            ViewData["title"] = Helper.GetEntityTitle<OverCost>(EnumTitle.List);

            return View(Helper.GetServiceResponse<OverCost>("Get?PageNumber=1&PageSize=1000&searchKey=null&SortOrder=id&token=1"));
        }

        private IEnumerable<SelectListItem> GetSaleCostPointGroupList(long? selected = null)
        => Helper.GetServiceResponseList<SaleCostPoint>("Get?PageNumber=1&PageSize=1000&searchKey=null&SortOrder=id&token=1")
                .OrderBy(o => o.SalePointName)
                .Select(c => new SelectListItem()
                {
                    Text = $"{c.SalePointName} - {c.CostPointName}",
                    Value = c.Id.ToString(),
                    Selected = selected == null ? false : c.Id == selected
                })
                .ToList();

        private IEnumerable<SelectListItem> GetOverCostTypeList(long? selected = null)
        => Helper.GetServiceResponseList<OverCostType>("Get?PageNumber=1&PageSize=1000&searchKey=null&SortOrder=id&token=1")
                .OrderBy(o => o.Name)
                .Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                    Selected = selected == null ? false : c.Id == selected
                })
                .ToList();

        public IActionResult AddOverCost()
        {
            ViewData["title"] = Helper.GetEntityTitle<OverCost>(EnumTitle.Add);

            ViewBag.OverCostType = GetOverCostTypeList();
            
            ViewBag.SaleCostPoint = GetSaleCostPointGroupList();

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOverCost(OverCost OverCost)
        {
            HttpContext.Session.TryGetValue("IUI", out byte[] session);
            string currentUserId = session.LastOrDefault().ToString();

            OverCost.RegisteredUserId = Convert.ToInt64(currentUserId);

            if (ModelState.IsValid)
            {
                (bool result, string message) postResult = Helper.PostValueToSevice<OverCost>("POST", OverCost);

                return Json(new { success = postResult.result, message = postResult.message });
            }

            return Json(new
            {
                model = OverCost,
                success = false,
                message = ModelState
                .Values
                .FirstOrDefault(e => e.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                .Errors
                .FirstOrDefault()
                .ErrorMessage ?? "Model Is Not Vald!"
            });
        }

        public IActionResult EditOverCost(long id)
        {
            ViewData["title"] = Helper.GetEntityTitle<SalePoint>(EnumTitle.Edit);

            var model = GetOverCostById(id);

            ViewBag.OverCostType = GetOverCostTypeList(model.OverCostTypeId);

            ViewBag.SaleCostPoint = GetSaleCostPointGroupList(model.SaleCostPointId);

            return PartialView(model);
        }

        [HttpPost]
        public IActionResult EditOverCost(long id, OverCost OverCost)
        {
            if (ModelState.IsValid)
            {
                OverCost.State = BusinessEntity.Models.Base.Enums.ObjectState.Active;

                (bool result, string message) postResult = Helper.PostValueToSevice<OverCost>("PUT?id=" + OverCost.Id.ToString(), OverCost);

                return Json(new { success = postResult.result, message = postResult.message });
            }

            return Json(new
            {
                model = OverCost,
                success = false,
                message = ModelState
                .Values
                .FirstOrDefault(e => e.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                .Errors
                .FirstOrDefault()
                .ErrorMessage ?? "Model Is Not Vald!"
            });
        }

        public IActionResult DeleteOverCost(long id)
        {
            ViewData["title"] = Helper.GetEntityTitle<OverCost>(EnumTitle.Delete);

            return PartialView(GetOverCostById(id));
        }

        [HttpPost]
        public IActionResult DeleteOverCost(OverCost OverCost)
        {
            (bool result, string message) postResult = Helper.PostValueToSevice<OverCost>("Delete?id=" + OverCost.Id.ToString(), OverCost);

            return Json(new { success = postResult.result, message = postResult.message });
        }

        private OverCost GetOverCostById(long id)
        {
            return (Helper.GetServiceResponse<OverCost>("GetById?id=" + id.ToString()).data as List<OverCost>)
                .FirstOrDefault();
        }
    }
}