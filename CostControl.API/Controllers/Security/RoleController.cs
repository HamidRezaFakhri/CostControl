using System.Collections.Generic;
using CostControl.API.Controllers.Base;
using CostControl.BusinessEntity.Models.Security;
using CostControl.BusinessLogic.Logics.Security;
using Microsoft.AspNetCore.Mvc;

namespace CostControl.API.Controllers.Security
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    //[ValidateAntiForgeryToken]
    public class RoleController : BaseApiController<Role, RoleLogic, long>
    {
        // GET api/values
        [HttpGet("Get2")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("cost1")]
        public ActionResult<IEnumerable<string>> cost1()
        {
            return new string[] { "value1", "value2" };
        }
    }
}