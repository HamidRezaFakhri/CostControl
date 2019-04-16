namespace CostControl.Presentation.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CostControl.BusinessEntity.Models.CostControl;
    using Microsoft.AspNetCore.Mvc;

    public class IntakeRemittanceController : BaseController
    {
        public IActionResult IntakeRemittanceList(string param)
        {
            ViewData["title"] = Helper.GetEntityTile<IntakeRemittance>(EnumTitle.List);

            return View(Helper.GetServiceResponse<IntakeRemittance>("Get?PageNumber=1&PageSize=1000&searchKey=null&SortOrder=id&token=1"));
        }

        public IActionResult AddIntakeRemittance()
        {
            ViewData["title"] = Helper.GetEntityTile<IntakeRemittance>(EnumTitle.Add);

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckIntakeRemittance(/*IntakeRemittanceDate*/DateTime IntakeDate)
        {
            return null;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddIntakeRemittance(IntakeRemittance IntakeRemittance)
        {
            if (ModelState.IsValid)
            {
                var postResult = Helper.PostValueToSevice<IntakeRemittance>("POST", IntakeRemittance);

                return Json(new { success = postResult.result, message = postResult.message });
            }

            return Json(new
            {
                model = IntakeRemittance,
                success = false,
                message = ModelState
                            .Values
                            .FirstOrDefault(e => e.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                            .Errors
                            .FirstOrDefault()
                            .ErrorMessage ?? "Model Is Not Vald!"
            });
        }

        public IActionResult EditIntakeRemittance(long id)
        {
            ViewData["title"] = Helper.GetEntityTile<IntakeRemittance>(EnumTitle.Edit);

            return PartialView(GetIntakeRemittanceById(id));
        }

        [HttpPost]
        public IActionResult EditIntakeRemittance(long id, IntakeRemittance IntakeRemittance)
        {
            if (ModelState.IsValid)
            {
                IntakeRemittance.State = BusinessEntity.Models.Base.Enums.ObjectState.Active;

                var postResult = Helper.PostValueToSevice<IntakeRemittance>("PUT?id=" + IntakeRemittance.Id.ToString(), IntakeRemittance);

                return Json(new { success = postResult.result, message = postResult.message });
            }

            return Json(new
            {
                model = IntakeRemittance,
                success = false,
                message = ModelState
                            .Values
                            .FirstOrDefault(e => e.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                            .Errors
                            .FirstOrDefault()
                            .ErrorMessage ?? "Model Is Not Vald!"
            });
        }

        public IActionResult DeleteIntakeRemittance(long id)
        {
            ViewData["title"] = Helper.GetEntityTile<SalePoint>(EnumTitle.Delete);

            return PartialView(GetIntakeRemittanceById(id));
        }

        [HttpPost]
        public IActionResult DeleteIntakeRemittance(IntakeRemittance IntakeRemittance)
        {
            var postResult = Helper.PostValueToSevice<IntakeRemittance>("Delete?id=" + IntakeRemittance.Id.ToString(), IntakeRemittance);

            return Json(new { success = postResult.result, message = postResult.message });
        }

        private IntakeRemittance GetIntakeRemittanceById(long id)
        {
            return (Helper.GetServiceResponse<IntakeRemittance>("GetById?id=" + id.ToString()).data as List<IntakeRemittance>)
                .FirstOrDefault();
        }
    }
}