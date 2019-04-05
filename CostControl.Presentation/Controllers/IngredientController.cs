namespace CostControl.Presentation.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using CostControl.BusinessEntity.Models.CostControl;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class IngredientController : BaseController
    {
        public IActionResult IngredientList(string param)
        {
            ViewData["title"] = Helper.GetEntityTile<Ingredient>(EnumTitle.List);

            return View(Helper.GetServiceResponse<Ingredient>("Get?PageNumber=1&PageSize=1000&searchKey=null&SortOrder=id&token=1"));
        }

        public IActionResult AddIngredient()
        {
            ViewData["title"] = Helper.GetEntityTile<Ingredient>(EnumTitle.Add);

            ViewBag.ConsumptionUnit = GetConsumptionUnits()
                                        .Select(c => new SelectListItem()
                                        {
                                            Text = c.Name,
                                            Value = c.Id.ToString()
                                        })
                                        .ToList();

            return PartialView();
        }

        private IEnumerable<ConsumptionUnit> GetConsumptionUnits()
        {
            return Helper.GetServiceResponseList<ConsumptionUnit>("Get?PageNumber=1&PageSize=1000&searchKey=null&SortOrder=id&token=1");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddIngredient(Ingredient Ingredient)
        {
            if (ModelState.IsValid)
            {
                var postResult = Helper.PostValueToSevice<Ingredient>("POST", Ingredient);

                return Json(new { success = postResult.result, message = postResult.message });
            }

            return Json(new
            {
                model = Ingredient,
                success = false,
                message = ModelState
                .Values
                .FirstOrDefault(e => e.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                .Errors
                .FirstOrDefault()
                .ErrorMessage ?? "Model Is Not Vald!"
            });
        }

        public IActionResult EditIngredient(long id)
        {
            ViewData["title"] = Helper.GetEntityTile<Ingredient>(EnumTitle.Edit);

            var ingredient = GetIngredientById(id);
            var consumptionUnit = GetConsumptionUnitById(ingredient.ConsumptionUnitId);

            ViewBag.ConsumptionUnit = GetConsumptionUnits()
                                        .Select(c => new SelectListItem()
                                        {
                                            Text = c.Name,
                                            Value = c.Id.ToString(),
                                            Selected = c.Id == consumptionUnit.Id
                                        })
                                        .ToList();

            return PartialView(ingredient);
        }

        private ConsumptionUnit GetConsumptionUnitById(long id)
        {
            return (Helper.GetServiceResponse<ConsumptionUnit>("GetById?id=" + id.ToString()).data as List<ConsumptionUnit>)
                .FirstOrDefault();
        }

        [HttpPost]
        public IActionResult EditIngredient(long id, Ingredient Ingredient)
        {
            if (ModelState.IsValid)
            {
                Ingredient.State = BusinessEntity.Models.Base.Enums.ObjectState.Active;

                var postResult = Helper.PostValueToSevice<Ingredient>("PUT?id=" + Ingredient.Id.ToString(), Ingredient);

                return Json(new { success = postResult.result, message = postResult.message });
            }

            return Json(new
            {
                model = Ingredient,
                success = false,
                message = ModelState
                .Values
                .FirstOrDefault(e => e.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                .Errors
                .FirstOrDefault()
                .ErrorMessage ?? "Model Is Not Vald!"
            });
        }

        public IActionResult DeleteIngredient(long id)
        {
            ViewData["title"] = Helper.GetEntityTile<Ingredient>(EnumTitle.Delete);

            return PartialView(GetIngredientById(id));
        }

        [HttpPost]
        public IActionResult DeleteIngredient(Ingredient Ingredient)
        {
            var postResult = Helper.PostValueToSevice<Ingredient>("Delete?id=" + Ingredient.Id.ToString(), Ingredient);

            return Json(new { success = postResult.result, message = postResult.message });
        }

        private Ingredient GetIngredientById(long id)
        {
            return (Helper.GetServiceResponse<Ingredient>("GetById?id=" + id.ToString()).data as List<Ingredient>)
                .FirstOrDefault();
        }
    }
}