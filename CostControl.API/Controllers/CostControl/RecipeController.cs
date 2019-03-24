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
	public class RecipeController : BaseApiController<Recipe, RecipeLogic, long>
	{
		[HttpGet("GetByParent")]
		public ActionResult<ServiceResponse<Recipe>> GetByParent(long parentId, [FromQuery]Pagination paginate = null, string token = "")
		{
			try
			{
				paginate = (paginate == null || paginate.PageSize <= 0) ? new Pagination() : paginate;
				paginate.TotalCount = PDKBusinessLogic.GetCount();

				return GenerateResponse(paginate,
					PDKBusinessLogic.GetByParentId(parentId, pageSize: paginate.PageSize, page: paginate.PageNumber));
			}
			catch (Exception e)
			{
				return GenerateExceptionResponse(e, "Exception!");
			}
		}
	}
}