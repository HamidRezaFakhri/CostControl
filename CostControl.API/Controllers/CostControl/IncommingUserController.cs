using CostControl.API.Controllers.Base;
using CostControl.API.Models;
using CostControl.BusinessEntity.Models.CostControl;
using CostControl.BusinessLogic.Logics.CostControl;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CostControl.API.Controllers.CostControl
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class IncommingUserController : BaseApiController<IncommingUser, IncommingUserLogic, int>
    {
        [HttpPost("AddIncommingUser")]
        [Route("api/IncommingUser/AddIncommingUser")]
        public ActionResult<ServiceResponse<IncommingUser>> AddIncommingUser([FromBody]IEnumerable<IncommingUser> users)
        {
            try
            {
                IEnumerable<IncommingUser> exists = PDKBusinessLogic.Get();

                if ((users == null) || (users?.Count() == 0))
                {
                    return null;
                }

                return GenerateResponse(null, PDKBusinessLogic.AddRange(users.ToList().Except(exists)));
            }
            catch (Exception e)
            {
                return GenerateExceptionResponse(e, "Exception!");
            }
        }
    }
}