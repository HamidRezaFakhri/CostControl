using CostControl.Presentation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CostControl.Presentation.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            var username = HttpContext?.Session?.GetString("userName");

            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login", "User");

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult GetData()
        {   
            string str = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5001/api/DataImport/");

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //HTTP GET
                System.Threading.Tasks.Task<HttpResponseMessage> responseTask = client.GetAsync("GetData");
                responseTask.Wait();

                HttpResponseMessage result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    //var readTask = result.Content.ReadAsAsync<ServiceResponse<Ingredient>>();

                    //readTask.Wait();

                    //values = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    //values = null;

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");

                    str = "Server error. Please contact administrator.";
                }
            }

            return RedirectToAction("Index");
        }

    }
}
