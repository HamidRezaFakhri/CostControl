namespace CostControl.API.Controllers.CostControl
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using API.Controllers.Base;
	using BusinessEntity.Models.CostControl;
	using BusinessLogic.Logics.CostControl;
	using global::CostControl.API.Models;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Query;

	[Route("api/[controller]")]
	[ApiController]
	[Produces("application/json")]
	public class IntakeRemittanceController : BaseApiController<IntakeRemittance, IntakeRemittanceLogic, long>
	{
		[HttpGet("GetById")]
		public override ActionResult<ServiceResponse<IntakeRemittance>> GetById(long id)
		{
			try
			{
				return GenerateResponse(null, entity: PDKBusinessLogic.GetById(id,
																includeProperties: new List<Expression<Func<IQueryable<IntakeRemittance>, IIncludableQueryable<IntakeRemittance, object>>>>{
																							a => a.Include(b => b.IntakeRemittanceItems)
																									.ThenInclude(c => c.Ingredient)
																									.ThenInclude(d => d.ConsumptionUnit)
																}));
			}
			catch (Exception e)
			{
				return GenerateExceptionResponse(e, "Exception!");
			}
		}

		[HttpPost("Confirm")]
		public ActionResult<ServiceResponse<IntakeRemittance>> Confirm(long id, [FromBody]IntakeRemittance entity)
		{
			if (entity != null /*&& id == entity.Id*/)
			{
				try
				{
					if(!(PDKBusinessLogic as IntakeRemittanceLogic).Confirm(entity))
					{
						throw new Exception("Confirm has been failed!?");
					}

					entity.IsConfirmed = true;


					return GenerateResponse(null,
						new List<IntakeRemittance>() { entity });
				}
				catch (Exception e)
				{
					return GenerateExceptionResponse(e, "Exception!");
				}
			}

			return GenerateExceptionResponse(new Exception("Model is null!"), "Exception!");
		}
	}
}