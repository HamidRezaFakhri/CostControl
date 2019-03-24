namespace CostControl.API.Controllers.CostControl
{
	using System;
	using API.Controllers.Base;
	using API.Models;
	using BusinessEntity.Models.CostControl;
	using BusinessLogic.Logics.CostControl;
	using Microsoft.AspNetCore.Mvc;

	[Route("api/[controller]")]
	[ApiController]
	[Produces("application/json")]
	public class SalePointController : BaseApiController<SalePoint, SalePointLogic, long>
	{
		[HttpGet("Get")]
		public override ActionResult<ServiceResponse<SalePoint>> Get([FromQuery]Pagination paginate = null, string token = "")
		{
			try
			{
				paginate = (paginate == null || paginate.PageSize <= 0) ? new Pagination() : paginate;
				paginate.TotalCount = PDKBusinessLogic.GetCount();

				return GenerateResponse(paginate,
					PDKBusinessLogic.Get(/*s => s.Name.Contains(paginate.SearchKey),*/ pageSize: paginate.PageSize, page: paginate.PageNumber));
			}
			catch (Exception e)
			{
				return GenerateExceptionResponse(e, "Exception!");
			}
		}
	}
}