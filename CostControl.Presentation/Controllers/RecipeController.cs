namespace CostControl.Presentation.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using CostControl.BusinessEntity.Models.CostControl;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class RecipeController : BaseController
    {
        public IActionResult RecipeList(long id, string param)
        {
            ViewData["title"] = Helper.GetEntityTitle<Recipe>(EnumTitle.List);
            ViewBag.ParentId = id;

            return View(Helper.GetServiceResponse<Recipe>
                    ($"GetByParent?parentId={id.ToString()}&PageNumber=1&PageSize=1000&searchKey=null&SortOrder=id&token=1"));
        }

        private IEnumerable<Ingredient> GetIngredients()
        => Helper.GetServiceResponseList<Ingredient>("Get?PageNumber=1&PageSize=1000&searchKey=null&SortOrder=id&token=1");

        private IEnumerable<SelectListItem> GetIngredienList(long? selected = null)
        => GetIngredients()
                .OrderBy(o => o.Name)
                .Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                    Selected = selected == null ? false : c.Id == selected
                })
                .ToList();

        public IActionResult AddRecipe(long id)
        {
            ViewBag.Ingredients = GetIngredienList();

            return PartialView(new Recipe { FoodId = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddRecipe(Recipe Recipe)
        {
            Recipe.Id = 0;
            Recipe.State = BusinessEntity.Models.Base.Enums.ObjectState.Active;

            if (ModelState.IsValid)
            {
                var postResult = Helper.PostValueToSevice<Recipe>("POST", Recipe);

                return Json(new { success = postResult.result, message = postResult.message });
            }

            return Json(new
            {
                model = Recipe,
                success = false,
                message = ModelState
                            .Values
                            .FirstOrDefault(e => e.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                            .Errors
                            .FirstOrDefault()
                            .ErrorMessage ?? "Model Is Not Vald!"
            });
        }

        public IActionResult EditRecipe(long id)
        {
            ViewData["title"] = Helper.GetEntityTitle<Recipe>(EnumTitle.Edit);

            var model = GetRecipeById(id);

            ViewBag.Ingredients = GetIngredienList(model.Id);

            return PartialView(model);
        }

        [HttpPost]
        public IActionResult EditRecipe(long id, Recipe Recipe)
        {
            if (ModelState.IsValid)
            {
                Recipe.State = BusinessEntity.Models.Base.Enums.ObjectState.Active;

                var postResult = Helper.PostValueToSevice<Recipe>("PUT?id=" + Recipe.Id.ToString(), Recipe);

                return Json(new { success = postResult.result, message = postResult.message });
            }

            return Json(new
            {
                model = Recipe,
                success = false,
                message = ModelState
                            .Values
                            .FirstOrDefault(e => e.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                            .Errors
                            .FirstOrDefault()
                            .ErrorMessage ?? "Model Is Not Vald!"
            });
        }

        public IActionResult DeleteRecipe(long id)
        {
            ViewData["title"] = Helper.GetEntityTitle<Recipe>(EnumTitle.Delete);

            return PartialView(GetRecipeById(id));
        }

        [HttpPost]
        public IActionResult DeleteRecipe(Recipe Recipe)
        {
            var postResult = Helper.PostValueToSevice<Recipe>("Delete?id=" + Recipe.Id.ToString(), Recipe);

            return Json(new { success = postResult.result, message = postResult.message });
        }

        private Recipe GetRecipeById(long id)
        {
            return (Helper.GetServiceResponse<Recipe>("GetById?id=" + id.ToString()).data as List<Recipe>)
                 .FirstOrDefault();
        }
    }
}