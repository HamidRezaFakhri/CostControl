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
            ViewData["title"] = "فهرست اقلام غذایی";

            ViewBag.ParentId = id;

            ServiceResponse<Recipe> values = null;

            string str = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5001/api/Recipe/");

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //HTTP GET
                var responseTask = client.GetAsync($"GetByParent?parentId={id.ToString()}&PageNumber=1&PageSize=10&searchKey=null&SortOrder=id&token=1");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ServiceResponse<Recipe>>();

                    readTask.Wait();

                    values = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    values = null;

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");

                    str = "Server error. Please contact administrator.";
                }
            }

            //return PartialView("~/Views/Recipe/RecipeList.cshtml");
            return View(values);
        }

        private IEnumerable<ConsumptionUnit> GetConsumptionUnits()
        {
            ServiceResponse<ConsumptionUnit> values = null;

            string str = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5001/api/ConsumptionUnit/");

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //HTTP GET
                var responseTask = client.GetAsync($"Get?PageNumber=1&PageSize=10&searchKey=null&SortOrder=id&token=1");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ServiceResponse<ConsumptionUnit>>();

                    readTask.Wait();

                    values = readTask.Result;

                    return values.data;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    values = null;

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");

                    str = "Server error. Please contact administrator.";

                    return null;
                }
            }
        }

        private IEnumerable<Ingredient> GetIngredients()
        {
            ServiceResponse<Ingredient> values = null;

            string str = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5001/api/Ingredient/");

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //HTTP GET
                var responseTask = client.GetAsync($"Get?PageNumber=1&PageSize=10&searchKey=null&SortOrder=id&token=1");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ServiceResponse<Ingredient>>();

                    readTask.Wait();

                    values = readTask.Result;

                    return values.data;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    values = null;

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");

                    str = "Server error. Please contact administrator.";

                    return null;
                }
            }
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
            ViewBag.ConsumptionUnits = GetConsumptionUnits()
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

            //https://johnthiriet.com/efficient-post-calls/
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
            ServiceResponse<Recipe> value = null;
            string str = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5001/api/Recipe/");

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //HTTP GET
                var responseTask = client.GetAsync("GetById?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ServiceResponse<Recipe>>();

                    readTask.Wait();

                    value = readTask.Result;

                    return (value.data as List<Recipe>).FirstOrDefault();
                }
                else //web api sent error response 
                {
                    //log response status here..

                    value = null;

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");

                    str = "Server error. Please contact administrator.";
                }

                return null;
            }
        }

        public IActionResult PostRecipe(string name)
        {
            return null;
        }
    }
}