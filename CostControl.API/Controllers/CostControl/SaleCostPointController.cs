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
    public class SaleCostPointController : BaseApiController<SaleCostPoint, SaleCostPointLogic, long>
    {
        [HttpGet("Get")]
        public override ActionResult<ServiceResponse<SaleCostPoint>> Get([FromQuery]Pagination paginate = null, string token = "")
        {
            try
            {
                paginate = (paginate == null || paginate.PageSize <= 0) ? new Pagination() : paginate;
                paginate.TotalCount = PDKBusinessLogic.GetCount();

                return GenerateResponse(paginate,
                                    PDKBusinessLogic.Get(
                                        includeProperties: new List<Expression<Func<IQueryable<SaleCostPoint>, IIncludableQueryable<SaleCostPoint, object>>>>{
                                                a => a.Include(b => b.SalePoint),
                                                a => a.Include(b => b.CostPoint)
                                        },
                                        pageSize: paginate.PageSize,
                                        page: paginate.PageNumber));
            }
            catch (Exception e)
            {
                return GenerateExceptionResponse(e, "Exception!");
            }
        }

        [HttpGet("GetById")]
        public override/*new*/ ActionResult<ServiceResponse<SaleCostPoint>> GetById(long id)
        {
            try
            {                
                return GenerateResponse(null, entity: PDKBusinessLogic.GetById(id,
                                                                includeProperties: new List<Expression<Func<IQueryable<SaleCostPoint>, IIncludableQueryable<SaleCostPoint, object>>>>{
                                                                                            a => a.Include(b => b.SalePoint),
                                                                                            a => a.Include(b => b.CostPoint)
                                                                }));
            }
            catch (Exception e)
            {
                return GenerateExceptionResponse(e, "Exception!");
            }
        }

    }
}