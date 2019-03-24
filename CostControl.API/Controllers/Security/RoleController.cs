namespace CostControl.API.Controllers.Security
{
	using System.Collections.Generic;
	using API.Controllers.Base;
	using BusinessEntity.Models.Security;
	using BusinessLogic.Logics.Security;
	using Microsoft.AspNetCore.Mvc;

	[Route("api/[controller]")]
	[ApiController]
	[Produces("application/json")]
	//[ValidateAntiForgeryToken]
	public class RoleController : BaseApiController<Role, RoleLogic, long>
	{
		// GET api/values
		[HttpGet("Get2")]
		public ActionResult<IEnumerable<string>> Get()
		{
			return new string[] { "value1", "value2" };
		}

		[HttpGet("cost1")]
		public ActionResult<IEnumerable<string>> cost1()
		{
			return new string[] { "value1", "value2" };
		}
	}
}