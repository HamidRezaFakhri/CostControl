namespace CostControl.API.Controllers.CostControl
{
	using API.Controllers.Base;
	using BusinessEntity.Models.CostControl;
	using BusinessLogic.Logics.CostControl;
	using Microsoft.AspNetCore.Mvc;

	[Route("api/[controller]")]
	[ApiController]
	[Produces("application/json")]
	public class InventoryController : BaseApiController<Inventory, InventoryLogic, long> { }
}