namespace CostControl.Presentation.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using CostControl.API.Models;
    using CostControl.BusinessEntity.Models.CostControl;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class RecipeController : BaseController
    {
        public IActionResult RecipeList(long id, string param)
        {
            ViewData["title"] = Helper.GetEntityTile<Recipe>(EnumTitle.List);
            ViewBag.ParentId = id;

            return View(Helper.GetServiceResponse<Recipe>
                    ($"GetByParent?parentId={id.ToString()}&PageNumber=1&PageSize=1000&searchKey=null&SortOrder=id&token=1"));
        }

        private IEnumerable<Ingredient> GetIngredients()
        {
            return Helper.GetServiceResponseList<Ingredient>("Get?PageNumber=1&PageSize=1000&searchKey=null&SortOrder=id&token=1");
        }

        public IActionResult AddRecipe(long id)
        {
            ViewBag.Ingredients = GetIngredients()
                                        .Select(c => new SelectListItem()
                                        {
                                            Text = c.Name,
                                            Value = c.Id.ToString()
                                        })
                                        .ToList();

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
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5001/api/Recipe/");

                    var result = client.PostAsJsonAsync("POST", Recipe);
                    result.Wait();
                    var res = result.Result;

                    if (res.IsSuccessStatusCode)
                    {
                        var p1 = res.Content.ReadAsStringAsync();
                        var p = res.Content.ReadAsStringAsync().Result;
                    }
                }
            }

            return Json(new { success = true, message = "Ok" });
        }

        public IActionResult EditRecipe(long id)
        {
            return PartialView(GetRecipeById(id));
        }

        [HttpPost]
        public IActionResult EditRecipe(long id, Recipe Recipe)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Recipe.State = BusinessEntity.Models.Base.Enums.ObjectState.Active;

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:5001/api/Recipe/");

                        var result = client.PostAsJsonAsync("PUT?id=" + id.ToString(), Recipe);
                        result.Wait();
                        var res = result.Result;

                        if (res.IsSuccessStatusCode)
                        {
                            var p1 = res.Content.ReadAsStringAsync();
                            var p = res.Content.ReadAsStringAsync().Result;
                        }
                    }
                }
                return Json(new { success = true, message = "Ok" });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    model = Recipe,
                    success = false,
                    message = ModelState
                    .Values
                    .FirstOrDefault(e => e.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                    .Errors
                    .FirstOrDefault()
                    .ErrorMessage ?? ex?.Message ?? "Model Is Not Vald!"
                });
            }
        }

        public IActionResult DeleteRecipe(long id)
        {
            return PartialView(GetRecipeById(id));
        }

        [HttpPost]
        public IActionResult DeleteRecipe(Recipe Recipe)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5001/api/Recipe/");

                    var result = client.PostAsJsonAsync("Delete?id=" + Recipe.Id.ToString(), Recipe);
                    result.Wait();
                    var res = result.Result;

                    if (res.IsSuccessStatusCode)
                    {
                        var p1 = res.Content.ReadAsStringAsync();
                        var p = res.Content.ReadAsStringAsync().Result;
                    }
                }
                return Json(new { success = true, message = "Ok" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        private Recipe GetRecipeById(long id)
        {
            return (Helper.GetServiceResponse<Recipe>("GetById?id=" + id.ToString()).data as List<Recipe>)
                 .FirstOrDefault();
        }

        public IActionResult PostRecipe(string name)
        {
            return null;
        }
    }
}