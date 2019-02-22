using CostControl.API.Controllers.Base;
using CostControl.API.Models;
using CostControl.BusinessEntity.Models.CostControl;
using CostControl.BusinessLogic.Logics.CostControl;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CostControl.API.Controllers.CostControl
{
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
                paginate.RowCount = PDKBusinessLogic.GetCount();

                var ip = new List<Expression<Func<CostPoint, object>>> {
                    a => new CostPointGroup()
                };

                return GenerateResponse(paginate,
                    PDKBusinessLogic.Get(/*s => s.Name.Contains(paginate.SearchKey),*/
                        filter: null,
                        includeProperties: ip,
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