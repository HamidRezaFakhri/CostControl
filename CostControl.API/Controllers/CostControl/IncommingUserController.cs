namespace CostControl.API.Controllers.CostControl
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using API.Controllers.Base;
	using API.Models;
	using BusinessEntity.Models.CostControl;
	using BusinessLogic.Logics.CostControl;
	using Microsoft.AspNetCore.Mvc;

	[Route("api/[controller]")]
	[ApiController]
	[Produces("application/json")]
	public class IncommingUserController : BaseApiController<IncommingUser, IncommingUserLogic, int>
	{
		[HttpPost("AddIncommingUser")]
		[Route("api/IncommingUser/AddIncommingUser")]
		public ActionResult<ServiceResponse<IncommingUser>> AddIncommingUser([FromBody]IEnumerable<IncommingUser> users)
		{
			try
			{
				IEnumerable<IncommingUser> exists = PDKBusinessLogic.Get();

				if ((users == null) || (users?.Count() == 0))
				{
					return null;
				}

				return GenerateResponse(null, PDKBusinessLogic.AddRange(users.ToList().Except(exists)));
			}
			catch (Exception e)
			{
				return GenerateExceptionResponse(e, "Exception!");
			}
		}
	}
}