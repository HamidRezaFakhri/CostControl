namespace CostControl.API.Controllers.CostControl
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using API.Controllers.Base;
	using BusinessEntity.Models.CostControl;
	using BusinessLogic.Logics.CostControl;
	using API.Models;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Query;

	[Route("api/[controller]")]
	[ApiController]
	[Produces("application/json")]
	public class IngredientController : BaseApiController<Ingredient, IngredientLogic, long>
	{
		[HttpGet("Get")]
		public override ActionResult<ServiceResponse<Ingredient>> Get([FromQuery]Pagination paginate = null, string token = "")
		{
			try
			{
				paginate = (paginate == null || paginate.PageSize <= 0) ? new Pagination() : paginate;
				paginate.TotalCount = PDKBusinessLogic.GetCount();

				return GenerateResponse(paginate,
									PDKBusinessLogic.Get(
										includeProperties: new List<Expression<Func<IQueryable<Ingredient>, IIncludableQueryable<Ingredient, object>>>>{
											a => a.Include(b => b.ConsumptionUnit)
										},
										pageSize: paginate.PageSize,
										page: paginate.PageNumber));
			}
			catch (Exception e)
			{
				return GenerateExceptionResponse(e, "Exception!");
			}
		}

		[HttpGet("GetExternalData")]
		public IEnumerable<dynamic> GetExternalData()
		{
			try
			{
				return (PDKBusinessLogic as IngredientLogic).GetExternalData();
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
				(PDKBusinessLogic as IngredientLogic).AddExternalData(id);
				return true;
			}
			catch (Exception e)
			{
				throw new Exception("Exception!", e);
			}
		}
	}
}