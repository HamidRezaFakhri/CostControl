namespace CostControl.Presentation.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using CostControl.BusinessEntity.Models.CostControl;
    using Microsoft.AspNetCore.Mvc;

    public class ConsumptionUnitController : BaseController
    {
        public IActionResult ConsumptionUnitList(string param)
        {
            ViewData["title"] = Helper.GetEntityTile<ConsumptionUnit>(EnumTitle.List);

            return View(Helper.GetServiceResponse<ConsumptionUnit>("Get?PageNumber=1&PageSize=1000&searchKey=null&SortOrder=id&token=1"));
        }

        public IActionResult AddConsumptionUnit()
        {
            ViewData["title"] = Helper.GetEntityTile<ConsumptionUnit>(EnumTitle.Add);

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddConsumptionUnit(ConsumptionUnit ConsumptionUnit)
        {
            if (ModelState.IsValid)
            {
                var postResult = Helper.PostValueToSevice<ConsumptionUnit>("POST", ConsumptionUnit);

                return Json(new { success = postResult.result, message = postResult.message });
            }

            return Json(new
            {
                model = ConsumptionUnit,
                success = false,
                message = ModelState
                .Values
                .FirstOrDefault(e => e.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                .Errors
                .FirstOrDefault()
                .ErrorMessage ?? "Model Is Not Vald!"
            });
        }

        public IActionResult EditConsumptionUnit(long id)
        {
            ViewData["title"] = Helper.GetEntityTile<ConsumptionUnit>(EnumTitle.Edit);

            return PartialView(GetConsumptionUnitById(id));
        }

        [HttpPost]
        public IActionResult EditConsumptionUnit(long id, ConsumptionUnit ConsumptionUnit)
        {
            if (ModelState.IsValid)
            {
                ConsumptionUnit.State = BusinessEntity.Models.Base.Enums.ObjectState.Active;

                var postResult = Helper.PostValueToSevice<ConsumptionUnit>("PUT?id=" + ConsumptionUnit.Id.ToString(), ConsumptionUnit);

                return Json(new { success = postResult.result, message = postResult.message });
            }

            return Json(new
            {
                model = ConsumptionUnit,
                success = false,
                message = ModelState
                            .Values
                            .FirstOrDefault(e => e.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                            .Errors
                            .FirstOrDefault()
                            .ErrorMessage ?? "Model Is Not Vald!"
            });
        }

        public IActionResult DeleteConsumptionUnit(long id)
        {
            ViewData["title"] = Helper.GetEntityTile<ConsumptionUnit>(EnumTitle.Delete);

            return PartialView(GetConsumptionUnitById(id));
        }

        [HttpPost]
        public IActionResult DeleteConsumptionUnit(ConsumptionUnit ConsumptionUnit)
        {
            var postResult = Helper.PostValueToSevice<ConsumptionUnit>("Delete?id=" + ConsumptionUnit.Id.ToString(), ConsumptionUnit);

            return Json(new { success = postResult.result, message = postResult.message });
        }

        private ConsumptionUnit GetConsumptionUnitById(long id)
        {
            return (Helper.GetServiceResponse<ConsumptionUnit>("GetById?id=" + id.ToString()).data as List<ConsumptionUnit>)
                .FirstOrDefault();
        }
    }
}