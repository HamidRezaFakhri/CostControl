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

	public class MenuItemController : BaseController
	{
		public IActionResult MenuItemList(string param)
		{
			ViewData["title"] = "MenuItem List";

			ServiceResponse<MenuItem> values = null;

			string str = string.Empty;
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("http://localhost:5001/api/MenuItem/");

				client.DefaultRequestHeaders.Clear();
				client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));

				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				//HTTP GET
				var responseTask = client.GetAsync("Get?PageNumber=1&PageSize=10&searchKey=null&SortOrder=id&token=1");
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<ServiceResponse<MenuItem>>();

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

			//return PartialView("~/Views/MenuItem/MenuItemList.cshtml");
			return View(values);
		}

		public IActionResult AddMenuItem()
		{
			return PartialView();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult AddMenuItem(MenuItem MenuItem)
		{
			//https://johnthiriet.com/efficient-post-calls/
			if (ModelState.IsValid)
			{
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri("http://localhost:5001/api/MenuItem/");

					var result = client.PostAsJsonAsync("POST", MenuItem);
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

		public IActionResult EditMenuItem(long id)
		{
			return PartialView(GetMenuItemById(id));
		}

		[HttpPost]
		public IActionResult EditMenuItem(long id, MenuItem MenuItem)
		{
			try
			{
				if (ModelState.IsValid)
				{
					MenuItem.State = BusinessEntity.Models.Base.Enums.ObjectState.Active;

					using (var client = new HttpClient())
					{
						client.BaseAddress = new Uri("http://localhost:5001/api/MenuItem/");

						var result = client.PostAsJsonAsync("PUT?id=" + id.ToString(), MenuItem);
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

		public IActionResult DeleteMenuItem(long id)
		{
			return PartialView(GetMenuItemById(id));
		}

		[HttpPost]
		public IActionResult DeleteMenuItem(MenuItem MenuItem)
		{
			try
			{
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri("http://localhost:5001/api/MenuItem/");

					var result = client.PostAsJsonAsync("Delete?id=" + MenuItem.Id.ToString(), MenuItem);
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

		private MenuItem GetMenuItemById(long id)
		{
			ServiceResponse<MenuItem> value = null;
			string str = string.Empty;
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("http://localhost:5001/api/MenuItem/");

				client.DefaultRequestHeaders.Clear();
				client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));

				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				//HTTP GET
				var responseTask = client.GetAsync("GetById?id=" + id.ToString());
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<ServiceResponse<MenuItem>>();

					readTask.Wait();

					value = readTask.Result;

					return (value.data as List<MenuItem>).FirstOrDefault();
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

		public IActionResult PostMenuItem(string name)
		{
			return null;
		}
	}
}