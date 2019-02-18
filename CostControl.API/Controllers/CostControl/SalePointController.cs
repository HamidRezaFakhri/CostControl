using CostControl.API.Controllers.Base;
using CostControl.API.Models;
using CostControl.BusinessEntity.Models.CostControl;
using CostControl.BusinessLogic.Logics.CostControl;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CostControl.API.Controllers.CostControl
{
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
                paginate.RowCount = PDKBusinessLogic.GetCount();

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