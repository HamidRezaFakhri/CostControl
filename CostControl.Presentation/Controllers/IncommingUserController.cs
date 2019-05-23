namespace CostControl.Presentation.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net.Http;
	using System.Net.Http.Headers;
	using CostControl.BusinessEntity.Models.CostControl;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;

	public class IncommingUserController : BaseController
	{
		private class Users
		{
			public List<ServiceUser> UserList { get; set; }

			public ResponseResult Result { get; set; }
		}

		private class ServiceUser
		{
			public int UserID { get; set; }

			public string UserName { get; set; }

			public int OperatorCode { get; set; }
		}

		private class ResponseResult
		{
			public int ErrorCode { get; set; }

			public string ErrorDescF { get; set; }

			public string ErrorDescE { get; set; }
		}

		//[Route("")]
		//[Route("Login")]
		//[Route("~/")]
		public IActionResult Login()
		{
			return View("~/Views/IncommingUser/Login.cshtml", "");
		}

		[HttpPost]
		public IActionResult Login(string userName, string pass)
		{
			if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(pass) || !IsUserValid(userName, pass))
			{
				return View("~/Views/IncommingUser/Login.cshtml", "نام کاربری و یا کلمه عبور اشتباه می باشد!");
			}

			HttpContext.Session.SetInt32("IUI", 1);

			return RedirectToAction("Index", "Home");
		}

		private bool IsUserValid(string userName, string pass)
		{
			try
			{
				using (HttpClient client = new HttpClient())
				{
					client.BaseAddress = new Uri($"{Helper.GetAuthenticationAddress()}isAuthorized?pUserID={userName}&pPassword={pass}");

					//List<IncommingUser> users = GetuserList().ToList();

					var result = client.GetStringAsync("");
					result.Wait();

					if (result.Result.Trim().ToLower() == "true")
					{
						HttpContext.Session.SetString("userName", userName);
						return true;
					}
				}

				return false;
			}
			catch
			{
				return false;
			}
		}

		private IEnumerable<IncommingUser> GetuserList()
		{
			Users values = null;
			IEnumerable<IncommingUser> users = null;

			string str = string.Empty;
			using (HttpClient client = new HttpClient())
			{
				client.BaseAddress = new Uri("http://localhost:8384/api/um");

				client.DefaultRequestHeaders.Clear();
				client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));

				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				//HTTP GET
				System.Threading.Tasks.Task<HttpResponseMessage> responseTask = client.GetAsync("");
				responseTask.Wait();

				HttpResponseMessage result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					System.Threading.Tasks.Task<Users> readTask = result.Content.ReadAsAsync<Users>();

					readTask.Wait();

					values = readTask.Result;

					if ((values != null) && (values.Result.ErrorCode == 0))
					{
						//users = new List<IncommingUser>();
						users = values
									.UserList
									.Select(u =>
												new IncommingUser
												{
													UserID = u.UserID,
													UserName = u.UserName,
													OperatorCode = u.OperatorCode
												});
					}
				}
			}

			return users;
		}

		public IActionResult UserList(string param, int pageNumber, int pageSize)
		{
			ViewData["title"] = "فهرست کاربران";

			Users values = null;

			string str = string.Empty;
			using (HttpClient client = new HttpClient())
			{
				client.BaseAddress = new Uri("http://localhost:8384/api/um");

				client.DefaultRequestHeaders.Clear();
				client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));

				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				//HTTP GET
				System.Threading.Tasks.Task<HttpResponseMessage> responseTask = client.GetAsync("");
				responseTask.Wait();

				HttpResponseMessage result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					System.Threading.Tasks.Task<Users> readTask = result.Content.ReadAsAsync<Users>();

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

			//return PartialView("~/Views/SalePoint/SalePointList.cshtml");
			return View(values);
		}

		[Route("Logout")]
		[HttpGet]
		public IActionResult Logout()
		{
			HttpContext.Session.Remove("userName");
			return RedirectToAction("Login", "IncommingUser");
		}
	}
}