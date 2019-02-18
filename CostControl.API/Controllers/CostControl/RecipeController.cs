using CostControl.API.Controllers.Base;
using CostControl.API.Models;
using CostControl.BusinessEntity.Models.CostControl;
using CostControl.BusinessLogic.Logics.CostControl;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq.Expressions;
using AutoMapper.QueryableExtensions;
using System.Linq;

namespace CostControl.API.Controllers.CostControl
{
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
                paginate.RowCount = PDKBusinessLogic.GetCount();
                
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