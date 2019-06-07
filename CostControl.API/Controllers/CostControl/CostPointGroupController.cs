namespace CostControl.API.Controllers.CostControl
{
	using System;
	using System.Collections.Generic;
	using API.Controllers.Base;
	using BusinessEntity.Models.CostControl;
	using BusinessLogic.Logics.CostControl;
	using Microsoft.AspNetCore.Mvc;

	[Route("api/[controller]")]
	[ApiController]
	[Produces("application/json")]
	public class CostPointGroupController : BaseApiController<CostPointGroup, CostPointGroupLogic, long>
	{
		[HttpGet("GetExternalData")]
		public IEnumerable<dynamic> GetExternalData()
		{
			try
			{
				return (PDKBusinessLogic as CostPointGroupLogic).GetExternalData();
			}
			catch (Exception e)
			{
				throw new Exception("Exception!", e);
			}
		}

		[HttpPost("AddExternalData")]
		public bool AddExternalData(string id)
		{
			try
			{
				(PDKBusinessLogic as CostPointGroupLogic).AddExternalData(id);
				return true;
			}
			catch (Exception e)
			{
				throw new Exception("Exception!", e);
			}
		}
	}
}