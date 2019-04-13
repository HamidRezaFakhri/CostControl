namespace CostControl.Presentation.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CostControl.BusinessEntity.Models.CostControl;
    using Microsoft.AspNetCore.Mvc;

    public class SalePointController : BaseController
    {
        public IActionResult SalePointList(string param, int pageNumber, int pageSize)
        {
            ViewData["title"] = Helper.GetEntityTile<SalePoint>(EnumTitle.List);

            return View(Helper.GetServiceResponse<SalePoint>("Get?PageNumber=1&PageSize=1000&searchKey=null&SortOrder=id&token=1"));
        }

        public IActionResult AddSalePoint()
        {
            ViewData["title"] = Helper.GetEntityTile<SalePoint>(EnumTitle.Add);

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSalePoint(SalePoint SalePoint)
        {
            if (ModelState.IsValid)
            {
                var postResult = Helper.PostValueToSevice<SalePoint>("POST", SalePoint);

                return Json(new { success = postResult.result, message = postResult.message });
            }

            //foreach (var error in SalePoint.Validate())
            //{
            //    foreach (var memberName in error.MemberNames)
            //        ModelState.AddModelError(memberName, error.ErrorMessage);
            //}

            //ModelState.AddModelError("Code", "sdrgsdfgsdfg");
            
            return Json(new
            {
                model = SalePoint,
                success = false,
                message = ModelState
                            .Values
                            .FirstOrDefault(e => e.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                            .Errors
                            .FirstOrDefault()
                            .ErrorMessage ?? "Model Is Not Vald!",
                //errors = ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                //                .Select(m => m.ErrorMessage).ToArray()
            });
        }

        public IActionResult EditSalePoint(long id)
        {
            ViewData["title"] = Helper.GetEntityTile<SalePoint>(EnumTitle.Edit);

            return PartialView(GetSalePointById(id));
        }

        [HttpPost]
        public IActionResult EditSalePoint(long id, SalePoint SalePoint)
        {
            if (ModelState.IsValid)
            {
                SalePoint.State = BusinessEntity.Models.Base.Enums.ObjectState.Active;

                var postResult = Helper.PostValueToSevice<SalePoint>("PUT?id=" + SalePoint.Id.ToString(), SalePoint);

                return Json(new { success = postResult.result, message = postResult.message });
            }

            return Json(new
            {
                model = SalePoint,
                success = false,
                message = ModelState
                            .Values
                            .FirstOrDefault(e => e.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                            .Errors
                            .FirstOrDefault()
                            .ErrorMessage ?? "Model Is Not Vald!"
            });
        }

        public IActionResult DeleteSalePoint(long id)
        {
            ViewData["title"] = Helper.GetEntityTile<SalePoint>(EnumTitle.Delete);

            return PartialView(GetSalePointById(id));
        }

        [HttpPost]
        public IActionResult DeleteSalePoint(SalePoint SalePoint)
        {
            var postResult = Helper.PostValueToSevice<SalePoint>("Delete?id=" + SalePoint.Id.ToString(), SalePoint);

            return Json(new { success = postResult.result, message = postResult.message });
        }

        private SalePoint GetSalePointById(long id)
        {
            return (Helper.GetServiceResponse<SalePoint>("GetById?id=" + id.ToString()).data as List<SalePoint>)
                .FirstOrDefault();
        }
    }
}