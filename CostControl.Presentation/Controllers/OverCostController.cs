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
            ViewData["title"] = Helper.GetEntityTile<OverCost>(EnumTitle.List);

            return View(Helper.GetServiceResponse<OverCost>("Get?PageNumber=1&PageSize=1000&searchKey=null&SortOrder=id&token=1"));
        }

        public IActionResult AddOverCost()
        {
            ViewData["title"] = Helper.GetEntityTile<OverCost>(EnumTitle.Add);

            ViewBag.OverCostType = Helper.GetServiceResponseList<OverCostType>("Get?PageNumber=1&PageSize=1000&searchKey=null&SortOrder=id&token=1")
                                        .Select(c => new SelectListItem()
                                        {
                                            Text = c.Name,
                                            Value = c.Id.ToString()
                                        })
                                        .ToList();



            ViewBag.SaleCostPoint = Helper.GetServiceResponseList<SaleCostPoint>("Get?PageNumber=1&PageSize=1000&searchKey=null&SortOrder=id&token=1")
                                        .Select(c => new SelectListItem()
                                        {
                                            Text = $"{c.SalePointName} - {c.CostPointName}",
                                            Value = c.Id.ToString()
                                        })
                                        .ToList();

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
            ViewData["title"] = Helper.GetEntityTile<SalePoint>(EnumTitle.Edit);

            OverCost model = GetOverCostById(id);

            model.StartDate = Convert.ToDateTime(model.StartDate.Date
                                    .ToString("yyyy/MM/dd", new CultureInfo("fa-IR")));

            model.EndDate = Convert.ToDateTime(model.EndDate.Date
                                    .ToString("yyyy/MM/dd", new CultureInfo("fa-IR")));

            ViewBag.OverCostType = Helper.GetServiceResponseList<OverCostType>("Get?PageNumber=1&PageSize=1000&searchKey=null&SortOrder=id&token=1")
                            .Select(c => new SelectListItem()
                            {
                                Text = c.Name,
                                Value = c.Id.ToString(),
                                Selected = c.Id == model.OverCostTypeId
                            })
                            .ToList();

            ViewBag.SaleCostPoint = Helper.GetServiceResponseList<SaleCostPoint>("Get?PageNumber=1&PageSize=1000&searchKey=null&SortOrder=id&token=1")
                                        .Select(c => new SelectListItem()
                                        {
                                            Text = $"{c.SalePointName} - {c.CostPointName}",
                                            Value = c.Id.ToString(),
                                            Selected = c.Id == model.SaleCostPointId
                                        })
                                        .ToList();

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
            ViewData["title"] = Helper.GetEntityTile<OverCost>(EnumTitle.Delete);

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