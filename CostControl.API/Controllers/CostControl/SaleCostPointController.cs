﻿using CostControl.API.Controllers.Base;
using CostControl.API.Models;
using CostControl.BusinessEntity.Models.CostControl;
using CostControl.BusinessLogic.Logics.CostControl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CostControl.API.Controllers.CostControl
{
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
                paginate.RowCount = PDKBusinessLogic.GetCount();

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
    }
}