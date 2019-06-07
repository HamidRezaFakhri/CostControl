namespace CostControl.Presentation.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using CostControl.BusinessEntity.Models.CostControl;
    using Microsoft.AspNetCore.Mvc;

    public class OverCostTypeController : BaseController
    {
        public IActionResult OverCostTypeList(string param)
        {
            ViewData["title"] = Helper.GetEntityTitle<OverCostType>(EnumTitle.List);

            return View(Helper.GetServiceResponse<OverCostType>("Get?PageNumber=1&PageSize=1000&searchKey=null&SortOrder=id&token=1"));
        }

        public IActionResult AddOverCostType()
        {
            ViewData["title"] = Helper.GetEntityTitle<OverCostType>(EnumTitle.Add);

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOverCostType(OverCostType OverCostType)
        {
            if (ModelState.IsValid)
            {
                var postResult = Helper.PostValueToSevice<OverCostType>("POST", OverCostType);

                return Json(new { success = postResult.result, message = postResult.message });
            }

            return Json(new
            {
                model = OverCostType,
                success = false,
                message = ModelState
                .Values
                .FirstOrDefault(e => e.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                .Errors
                .FirstOrDefault()
                .ErrorMessage ?? "Model Is Not Vald!"
            });
        }

        public IActionResult EditOverCostType(long id)
        {
            ViewData["title"] = Helper.GetEntityTitle<OverCostType>(EnumTitle.Edit);

            return PartialView(GetOverCostTypeById(id));
        }

        [HttpPost]
        public IActionResult EditOverCostType(long id, OverCostType OverCostType)
        {
            if (ModelState.IsValid)
            {
                OverCostType.State = BusinessEntity.Models.Base.Enums.ObjectState.Active;

                var postResult = Helper.PostValueToSevice<OverCostType>("PUT?id=" + OverCostType.Id.ToString(), OverCostType);

                return Json(new { success = postResult.result, message = postResult.message });
            }

            return Json(new
            {
                model = OverCostType,
                success = false,
                message = ModelState
                .Values
                .FirstOrDefault(e => e.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                .Errors
                .FirstOrDefault()
                .ErrorMessage ?? "Model Is Not Vald!"
            });
        }

        public IActionResult DeleteOverCostType(long id)
        {
            ViewData["title"] = Helper.GetEntityTitle<OverCostType>(EnumTitle.Delete);

            return PartialView(GetOverCostTypeById(id));
        }

        [HttpPost]
        public IActionResult DeleteOverCostType(OverCostType OverCostType)
        {
            var postResult = Helper.PostValueToSevice<OverCostType>("Delete?id=" + OverCostType.Id.ToString(), OverCostType);

            return Json(new { success = postResult.result, message = postResult.message });
        }

        private OverCostType GetOverCostTypeById(long id)
        {
            return (Helper.GetServiceResponse<OverCostType>("GetById?id=" + id.ToString()).data as List<OverCostType>)
                .FirstOrDefault();
        }
    }
}