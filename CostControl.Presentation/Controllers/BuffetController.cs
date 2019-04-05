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

	public class BuffetController : BaseController
	{
		public IActionResult BuffetList(string param)
		{
			ViewData["title"] = "Buffet List";

			ServiceResponse<Buffet> values = null;

			string str = string.Empty;
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("http://localhost:5001/api/Buffet/");

				client.DefaultRequestHeaders.Clear();
				client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));

				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				//HTTP GET
				var responseTask = client.GetAsync("Get?PageNumber=1&PageSize=1000&searchKey=null&SortOrder=id&token=1");
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<ServiceResponse<Buffet>>();

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

			//return PartialView("~/Views/Buffet/BuffetList.cshtml");
			return View(values);
		}

		public IActionResult AddBuffet()
		{
			return PartialView();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult AddBuffet(Buffet Buffet)
		{
			//https://johnthiriet.com/efficient-post-calls/
			if (ModelState.IsValid)
			{
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri("http://localhost:5001/api/Buffet/");

					var result = client.PostAsJsonAsync("POST", Buffet);
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

		public IActionResult EditBuffet(long id)
		{
			return PartialView(GetBuffetById(id));
		}

		[HttpPost]
		public IActionResult EditBuffet(long id, Buffet Buffet)
		{
			try
			{
				if (ModelState.IsValid)
				{
					Buffet.State = BusinessEntity.Models.Base.Enums.ObjectState.Active;

					using (var client = new HttpClient())
					{
						client.BaseAddress = new Uri("http://localhost:5001/api/Buffet/");

						var result = client.PostAsJsonAsync("PUT?id=" + id.ToString(), Buffet);
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

		public IActionResult DeleteBuffet(long id)
		{
			return PartialView(GetBuffetById(id));
		}

		[HttpPost]
		public IActionResult DeleteBuffet(Buffet Buffet)
		{
			try
			{
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri("http://localhost:5001/api/Buffet/");

					var result = client.PostAsJsonAsync("Delete?id=" + Buffet.Id.ToString(), Buffet);
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

		private Buffet GetBuffetById(long id)
		{
			ServiceResponse<Buffet> value = null;
			string str = string.Empty;
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("http://localhost:5001/api/Buffet/");

				client.DefaultRequestHeaders.Clear();
				client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));

				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				//HTTP GET
				var responseTask = client.GetAsync("GetById?id=" + id.ToString());
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<ServiceResponse<Buffet>>();

					readTask.Wait();

					value = readTask.Result;

					return (value.data as List<Buffet>).FirstOrDefault();
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

		public IActionResult PostBuffet(string name)
		{
			return null;
		}
	}
}