namespace CostControl.Presentation.Controllers
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Filters;
	using Microsoft.AspNetCore.Routing;

	public class BaseController : Controller
	{
		public BaseController()
		{
			Helper.Log(
						"Controller Name: " + ControllerContext?.ActionDescriptor?.ControllerName + Environment.NewLine +
						"Action Name: " + ControllerContext?.ActionDescriptor?.ActionName);

			byte[] session = null;

			HttpContext?.Session?.TryGetValue("IUI", out session);

			var username = HttpContext?.Session?.GetString("userName");
			
			Helper.Log(
						"Controller Name: " + ControllerContext?.ActionDescriptor?.ControllerName + Environment.NewLine +
						"Action Name: " + ControllerContext?.ActionDescriptor?.ActionName + Environment.NewLine +
						"User Name: " + username ?? "");
		}

		public override void OnActionExecuting(ActionExecutingContext context)
		{
			byte[] session = null;

			HttpContext?.Session?.TryGetValue("IUI", out session);

			var username = HttpContext?.Session?.GetString("userName");

			var controllerName = ControllerContext?.ActionDescriptor?.ControllerName;
			var actionName = ControllerContext?.ActionDescriptor?.ActionName;

			if (
				!controllerName.Trim().ToLower().Equals("Incomminguser")
				&&
				!actionName.Trim().ToLower().Equals("login")
				&&
				(string.IsNullOrEmpty(username) || session == null || !session.Any())
			)
			{
				//context.Result = new RedirectResult("Logout");

				context.Result = new RedirectToRouteResult("default",
									new RouteValueDictionary(
										new
										{
											action = "",
											controller = "",
											area = ""
										}));

				return;
			}

			base.OnActionExecuting(context);
		}

		//public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		//{
		//	// logic before action goes here

		//	await next(); // the actual action

		//	// logic after the action goes here

		//	//base.OnActionExecutionAsync(context, next);
		//}

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