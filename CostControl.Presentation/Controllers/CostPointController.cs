﻿namespace CostControl.Presentation.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using CostControl.BusinessEntity.Models.CostControl;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CostPointController : BaseController
    {
        public IActionResult CostPointList(string param)
        {
            ViewData["title"] = Helper.GetEntityTile<CostPoint>(EnumTitle.List);

            return View(Helper.GetServiceResponse<CostPoint>("Get?PageNumber=1&PageSize=1000&searchKey=null&SortOrder=id&token=1"));
        }

        private IEnumerable<CostPointGroup> GetCostPointGroups()
        => Helper.GetServiceResponseList<CostPointGroup>("Get?PageNumber=1&PageSize=1000&searchKey=null&SortOrder=id&token=1");

        private IEnumerable<SelectListItem> GetCostPointGroupList(long? selected = null)
        => GetCostPointGroups()
                .OrderBy(o => o.Name)
                .Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                    Selected = selected == null ? false : c.Id == selected
                })
                .ToList();

        public IActionResult AddCostPoint()
        {
            ViewData["title"] = Helper.GetEntityTile<CostPoint>(EnumTitle.Add);

            ViewBag.CostPointGroup = GetCostPointGroupList();

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCostPoint(CostPoint CostPoint)
        {
            if (ModelState.IsValid)
            {
                var postResult = Helper.PostValueToSevice<CostPoint>("POST", CostPoint);

                return Json(new { success = postResult.result, message = postResult.message });
            }

            return Json(new
            {
                model = CostPoint,
                success = false,
                message = ModelState
                .Values
                .FirstOrDefault(e => e.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                .Errors
                .FirstOrDefault()
                .ErrorMessage ?? "Model Is Not Vald!"
            });
        }

        public IActionResult EditCostPoint(long id)
        {
            ViewData["title"] = Helper.GetEntityTile<CostPoint>(EnumTitle.Edit);

            var costPoint = GetCostPointById(id);
            var costPointGroup = GetCostPointGroupById(costPoint.CostPointGroupId);

            ViewBag.CostPointGroup = GetCostPointGroupList(costPointGroup.Id);

            return PartialView(costPoint);
        }

        private CostPointGroup GetCostPointGroupById(long id)
        {
            return (Helper.GetServiceResponse<CostPointGroup>("GetById?id=" + id.ToString()).data as List<CostPointGroup>)
                .FirstOrDefault();
        }

        [HttpPost]
        public IActionResult EditCostPoint(long id, CostPoint CostPoint)
        {
            if (ModelState.IsValid)
            {
                CostPoint.State = BusinessEntity.Models.Base.Enums.ObjectState.Active;

                var postResult = Helper.PostValueToSevice<CostPoint>("PUT?id=" + CostPoint.Id.ToString(), CostPoint);

                return Json(new { success = postResult.result, message = postResult.message });
            }

            return Json(new
            {
                model = CostPoint,
                success = false,
                message = ModelState
                .Values
                .FirstOrDefault(e => e.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                .Errors
                .FirstOrDefault()
                .ErrorMessage ?? "Model Is Not Vald!"
            });
        }

        public IActionResult DeleteCostPoint(long id)
        {
            ViewData["title"] = Helper.GetEntityTile<CostPoint>(EnumTitle.Delete);

            return PartialView(GetCostPointById(id));
        }

        [HttpPost]
        public IActionResult DeleteCostPoint(CostPoint CostPoint)
        {
            var postResult = Helper.PostValueToSevice<CostPoint>("Delete?id=" + CostPoint.Id.ToString(), CostPoint);

            return Json(new { success = postResult.result, message = postResult.message });
        }

        private CostPoint GetCostPointById(long id)
        {
            return (Helper.GetServiceResponse<CostPoint>("GetById?id=" + id.ToString()).data as List<CostPoint>)
                .FirstOrDefault();
        }
    }
}