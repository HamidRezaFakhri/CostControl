namespace CostControl.API.Controllers.CostControl
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using API.Controllers.Base;
	using API.Models;
	using BusinessEntity.Models.CostControl;
	using BusinessLogic.Logics.CostControl;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Query;

	[Route("api/[controller]")]
	[ApiController]
	[Produces("application/json")]
	public class CostPointController : BaseApiController<CostPoint, CostPointLogic, long>
	{
		[HttpGet("Get")]
		public override ActionResult<ServiceResponse<CostPoint>> Get([FromQuery]Pagination paginate = null, string token = "")
		{
			try
			{
				paginate = (paginate == null || paginate.PageSize <= 0) ? new Pagination() : paginate;
				paginate.TotalCount = PDKBusinessLogic.GetCount();

				return GenerateResponse(paginate,
									PDKBusinessLogic.Get(
										includeProperties: new List<Expression<Func<IQueryable<CostPoint>, IIncludableQueryable<CostPoint, object>>>>{
											a => a.Include(b => b.CostPointGroup)
										},
										pageSize: paginate.PageSize,
										page: paginate.PageNumber));
			}
			catch (Exception e)
			{
				return GenerateExceptionResponse(e, "Exception!");
			}
		}
	}
}