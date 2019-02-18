using CostControl.API.Models;
using CostControl.BusinessEntity.Models.CostControl;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CostControl.Presentation.Controllers
{
    public class SaleController : BaseController
    {
        public IActionResult SaleList(string param)
        {
            ViewData["title"] = "فهرست فروش";

            ServiceResponse<Sale> values = null;

            string str = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5001/api/Sale/");

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //HTTP GET
                var responseTask = client.GetAsync("Get?PageNumber=1&PageSize=10&searchKey=null&SortOrder=id&token=1");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ServiceResponse<Sale>>();

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

            //return PartialView("~/Views/Sale/SaleList.cshtml");
            return View(values);
        }

        public IActionResult AddSale()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSale(Sale Sale)
        {
            //https://johnthiriet.com/efficient-post-calls/
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5001/api/Sale/");

                    var result = client.PostAsJsonAsync("POST", Sale);
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

        public IActionResult EditSale(long id)
        {
            return PartialView(GetSaleById(id));
        }

        [HttpPost]
        public IActionResult EditSale(long id, Sale Sale)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Sale.State = BusinessEntity.Models.Base.Enums.ObjectState.Active;

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:5001/api/Sale/");

                        var result = client.PostAsJsonAsync("PUT?id=" + id.ToString(), Sale);
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
                return Json(new { success = false, message = ex.Message });
            }
        }

        public IActionResult DeleteSale(long id)
        {
            return PartialView(GetSaleById(id));
        }

        [HttpPost]
        public IActionResult DeleteSale(Sale Sale)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5001/api/Sale/");

                    var result = client.PostAsJsonAsync("Delete?id=" + Sale.Id.ToString(), Sale);
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

        private Sale GetSaleById(long id)
        {
            ServiceResponse<Sale> value = null;
            string str = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5001/api/Sale/");

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //HTTP GET
                var responseTask = client.GetAsync("GetById?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ServiceResponse<Sale>>();

                    readTask.Wait();

                    value = readTask.Result;

                    return (value.data as List<Sale>).FirstOrDefault();
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

        public IActionResult PostSale(string name)
        {
            return null;
        }
    }
}