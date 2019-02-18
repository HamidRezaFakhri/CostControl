using CostControl.API.Controllers.Base;
using CostControl.BusinessEntity.Models.CostControl;
using CostControl.BusinessLogic.Logics.CostControl;
using Microsoft.AspNetCore.Mvc;

namespace CostControl.API.Controllers.CostControl
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ConsumptionUnitController : BaseApiController<ConsumptionUnit, ConsumptionUnitLogic, long> { }
}