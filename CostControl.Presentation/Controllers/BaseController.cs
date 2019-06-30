namespace CostControl.Presentation.Controllers
{
	using System;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;

	public class BaseController : Controller
	{
		public BaseController()
		{
			Helper.Log(
						"Controller Name: " + ControllerContext?.ActionDescriptor?.ControllerName + Environment.NewLine +
						"Action Name: " + ControllerContext?.ActionDescriptor?.ActionName);

			var username = HttpContext?.Session?.GetString("userName");

			if (string.IsNullOrEmpty(username))
				RedirectToAction("Login", "User");

			Helper.Log(
						"Controller Name: " + ControllerContext?.ActionDescriptor?.ControllerName + Environment.NewLine +
						"Action Name: " + ControllerContext?.ActionDescriptor?.ActionName + Environment.NewLine +
						"User Name: " + username ?? "");
		}

		protected string CookieName = "";

		public void Title(string title)
		{
			ViewBag.PageTitle = title;
		}

		public void AddCookie(string param)
		{
			//var cookie = new HttpCookie(CookieName)
			//{
			//    Expires = DateTime.Now.AddMinutes(45),
			//    Value = SerializeObject(param)
			//};
			//HttpContext.Request.Cookies.Remove(CookieName);
			//HttpContext.Request.Cookies.Add(cookie);
		}

		public string GetCookie()
		{
			//var cookie = Request.Cookies[CookieName];
			//if (cookie != null)
			//{
			//    return DeserializeObject<string>(cookie.Value);
			//}

			return null;
		}
	}
}