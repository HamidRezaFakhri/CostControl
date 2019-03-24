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
	public class OverCostController : BaseApiController<OverCost, OverCostLogic, long>
	{
		[HttpGet("Get")]
		public override ActionResult<ServiceResponse<OverCost>> Get([FromQuery]Pagination paginate = null, string token = "")
		{
			try
			{
				paginate = (paginate == null || paginate.PageSize <= 0) ? new Pagination() : paginate;
				paginate.TotalCount = PDKBusinessLogic.GetCount();

				return GenerateResponse(paginate,
									PDKBusinessLogic.Get(
										includeProperties: new List<Expression<Func<IQueryable<OverCost>, IIncludableQueryable<OverCost, object>>>>{
											a => a.Include(b => b.OverCostType),
											a => a.Include(b => b.SaleCostPoint),
											a => a.Include(b => b.SaleCostPoint.SalePoint),
											a => a.Include(b => b.SaleCostPoint.CostPoint)
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