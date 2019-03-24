namespace CostControl.API.Controllers.CostControl
{
	using API.Controllers.Base;
	using BusinessEntity.Models.CostControl;
	using BusinessLogic.Logics.CostControl;
	using Microsoft.AspNetCore.Mvc;

	[Route("api/[controller]")]
	[ApiController]
	[Produces("application/json")]
	public class SettingController : BaseApiController<Setting, SettingLogic, int> { }
}