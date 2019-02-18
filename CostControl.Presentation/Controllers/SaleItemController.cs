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
    public class SaleItemController : BaseController
    {
        public IActionResult SaleItemList(string param)
        {
            ViewData["title"] = "SaleItem List";

            ServiceResponse<SaleItem> values = null;

            string str = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5001/api/SaleItem/");

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //HTTP GET
                var responseTask = client.GetAsync("Get?PageNumber=1&PageSize=10&searchKey=null&SortOrder=id&token=1");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ServiceResponse<SaleItem>>();

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

            //return PartialView("~/Views/SaleItem/SaleItemList.cshtml");
            return View(values);
        }

        public IActionResult AddSaleItem()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSaleItem(SaleItem SaleItem)
        {
            //https://johnthiriet.com/efficient-post-calls/
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5001/api/SaleItem/");

                    var result = client.PostAsJsonAsync("POST", SaleItem);
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

        public IActionResult EditSaleItem(long id)
        {
            return PartialView(GetSaleItemById(id));
        }

        [HttpPost]
        public IActionResult EditSaleItem(long id, SaleItem SaleItem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SaleItem.State = BusinessEntity.Models.Base.Enums.ObjectState.Active;

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:5001/api/SaleItem/");

                        var result = client.PostAsJsonAsync("PUT?id=" + id.ToString(), SaleItem);
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

        public IActionResult DeleteSaleItem(long id)
        {
            return PartialView(GetSaleItemById(id));
        }

        [HttpPost]
        public IActionResult DeleteSaleItem(SaleItem SaleItem)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5001/api/SaleItem/");

                    var result = client.PostAsJsonAsync("Delete?id=" + SaleItem.Id.ToString(), SaleItem);
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

        private SaleItem GetSaleItemById(long id)
        {
            ServiceResponse<SaleItem> value = null;
            string str = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5001/api/SaleItem/");

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //HTTP GET
                var responseTask = client.GetAsync("GetById?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ServiceResponse<SaleItem>>();

                    readTask.Wait();

                    value = readTask.Result;

                    return (value.data as List<SaleItem>).FirstOrDefault();
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

        public IActionResult PostSaleItem(string name)
        {
            return null;
        }
    }
}