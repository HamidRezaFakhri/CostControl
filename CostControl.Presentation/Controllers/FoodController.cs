namespace CostControl.Presentation.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using CostControl.BusinessEntity.Models.CostControl;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class FoodController : BaseController
    {
        public IActionResult FoodList(string param)
        {
            ViewData["title"] = Helper.GetEntityTile<Food>(EnumTitle.List);

            return View(Helper.GetServiceResponse<Food>("Get?PageNumber=1&PageSize=1000&searchKey=null&SortOrder=id&token=1"));
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

        public IActionResult AddFood()
        {
            ViewData["title"] = Helper.GetEntityTile<Food>(EnumTitle.Add);

            ViewBag.SaleCostPoint = GetSaleCostPointGroupList();

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddFood(Food Food)
        {
            if (ModelState.IsValid)
            {
                var postResult = Helper.PostValueToSevice<Food>("POST", Food);

                return Json(new { success = postResult.result, message = postResult.message });
            }

            return Json(new
            {
                model = Food,
                success = false,
                message = ModelState
                .Values
                .FirstOrDefault(e => e.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                .Errors
                .FirstOrDefault()
                .ErrorMessage ?? "Model Is Not Vald!"
            });
        }

        public IActionResult EditFood(long id)
        {
            ViewData["title"] = Helper.GetEntityTile<Food>(EnumTitle.Edit);

            var model = GetFoodById(id);

            ViewBag.SaleCostPoint = GetSaleCostPointGroupList(model.SaleCostPointId);

            return PartialView(model);
        }

        [HttpPost]
        public IActionResult EditFood(long id, Food Food)
        {
            if (ModelState.IsValid)
            {
                Food.State = BusinessEntity.Models.Base.Enums.ObjectState.Active;

                var postResult = Helper.PostValueToSevice<Food>("PUT?id=" + Food.Id.ToString(), Food);

                return Json(new { success = postResult.result, message = postResult.message });
            }

            return Json(new
            {
                model = Food,
                success = false,
                message = ModelState
                .Values
                .FirstOrDefault(e => e.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                .Errors
                .FirstOrDefault()
                .ErrorMessage ?? "Model Is Not Vald!"
            });
        }

        public IActionResult DeleteFood(long id)
        {
            ViewData["title"] = Helper.GetEntityTile<Food>(EnumTitle.Delete);

            return PartialView(GetFoodById(id));
        }

        [HttpPost]
        public IActionResult DeleteFood(Food Food)
        {
            var postResult = Helper.PostValueToSevice<Food>("Delete?id=" + Food.Id.ToString(), Food);

            return Json(new { success = postResult.result, message = postResult.message });
        }

        private Food GetFoodById(long id)
        {
            return (Helper.GetServiceResponse<Food>("GetById?id=" + id.ToString()).data as List<Food>)
                .FirstOrDefault();
        }
    }
}