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

	public class IntakeRemittanceController : BaseController
	{
		public IActionResult IntakeRemittanceList(string param)
		{
			ViewData["title"] = "فهرست حواله های مصرفی";

			ServiceResponse<IntakeRemittance> values = null;

			string str = string.Empty;
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("http://localhost:5001/api/IntakeRemittance/");

				client.DefaultRequestHeaders.Clear();
				client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));

				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				//HTTP GET
				var responseTask = client.GetAsync("Get?PageNumber=1&PageSize=10&searchKey=null&SortOrder=id&token=1");
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<ServiceResponse<IntakeRemittance>>();

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

			//return PartialView("~/Views/IntakeRemittance/IntakeRemittanceList.cshtml");
			return View(values);
		}

		public IActionResult AddIntakeRemittance()
		{
			return PartialView();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult AddIntakeRemittance(IntakeRemittance IntakeRemittance)
		{
			//https://johnthiriet.com/efficient-post-calls/
			if (ModelState.IsValid)
			{
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri("http://localhost:5001/api/IntakeRemittance/");

					var result = client.PostAsJsonAsync("POST", IntakeRemittance);
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

		public IActionResult EditIntakeRemittance(long id)
		{
			return PartialView(GetIntakeRemittanceById(id));
		}

		[HttpPost]
		public IActionResult EditIntakeRemittance(long id, IntakeRemittance IntakeRemittance)
		{
			try
			{
				if (ModelState.IsValid)
				{
					IntakeRemittance.State = BusinessEntity.Models.Base.Enums.ObjectState.Active;

					using (var client = new HttpClient())
					{
						client.BaseAddress = new Uri("http://localhost:5001/api/IntakeRemittance/");

						var result = client.PostAsJsonAsync("PUT?id=" + id.ToString(), IntakeRemittance);
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

		public IActionResult DeleteIntakeRemittance(long id)
		{
			return PartialView(GetIntakeRemittanceById(id));
		}

		[HttpPost]
		public IActionResult DeleteIntakeRemittance(IntakeRemittance IntakeRemittance)
		{
			try
			{
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri("http://localhost:5001/api/IntakeRemittance/");

					var result = client.PostAsJsonAsync("Delete?id=" + IntakeRemittance.Id.ToString(), IntakeRemittance);
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

		private IntakeRemittance GetIntakeRemittanceById(long id)
		{
			ServiceResponse<IntakeRemittance> value = null;
			string str = string.Empty;
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("http://localhost:5001/api/IntakeRemittance/");

				client.DefaultRequestHeaders.Clear();
				client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));

				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				//HTTP GET
				var responseTask = client.GetAsync("GetById?id=" + id.ToString());
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<ServiceResponse<IntakeRemittance>>();

					readTask.Wait();

					value = readTask.Result;

					return (value.data as List<IntakeRemittance>).FirstOrDefault();
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

		public IActionResult PostIntakeRemittance(string name)
		{
			return null;
		}
	}
}