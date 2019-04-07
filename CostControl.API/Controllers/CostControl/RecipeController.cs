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
    public class RecipeController : BaseApiController<Recipe, RecipeLogic, long>
    {
        [HttpGet("GetById")]
        public override ActionResult<ServiceResponse<Recipe>> GetById(long id)
        {
            try
            {
                return GenerateResponse(null, PDKBusinessLogic
                                                .GetById(id,
                                                        includeProperties: new List<Expression<Func<IQueryable<Recipe>, IIncludableQueryable<Recipe, object>>>>{
                                                            a => a.Include(b => b.Ingredient)
                                                        }));
                
            }
            catch (Exception e)
            {
                return GenerateExceptionResponse(e, "Exception!");
            }
        }

        [HttpGet("GetByParent")]
        public ActionResult<ServiceResponse<Recipe>> GetByParent(long parentId, [FromQuery]Pagination paginate = null, string token = "")
        {
            try
            {
                paginate = (paginate == null || paginate.PageSize <= 0) ? new Pagination() : paginate;
                paginate.TotalCount = PDKBusinessLogic.GetCount();

                return GenerateResponse(paginate,
                    PDKBusinessLogic.GetByParentId(parentId,
                                                        includeProperties: new List<Expression<Func<IQueryable<Recipe>, IIncludableQueryable<Recipe, object>>>>{
                                                            a => a.Include(b => b.Ingredient).Include(b => b.Ingredient.ConsumptionUnit)
                                                        },
                                                        pageSize: paginate.PageSize, page: paginate.PageNumber));
            }
            catch (Exception e)
            {
                return GenerateExceptionResponse(e, "Exception!");
            }
        }
    }
}