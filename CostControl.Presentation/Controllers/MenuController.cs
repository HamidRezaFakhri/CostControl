namespace CostControl.Presentation.Controllers
{
	using System.Collections.Generic;
	using System.Linq;
	using CostControl.BusinessEntity.Models.CostControl;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Rendering;

	public class MenuController : BaseController
	{
		public IActionResult MenuList(string param)
		{
			ViewData["title"] = Helper.GetEntityTile<Menu>(EnumTitle.List);

			return View(Helper.GetServiceResponse<Menu>("Get?PageNumber=1&PageSize=10&searchKey=null&SortOrder=id&token=1"));
		}

		public IActionResult AddMenu()
		{
			ViewData["title"] = Helper.GetEntityTile<Menu>(EnumTitle.Add);

			ViewBag.SaleCostPoint = Helper.GetServiceResponseList<SaleCostPoint>("Get?PageNumber=1&PageSize=10&searchKey=null&SortOrder=id&token=1")
										.Select(c => new SelectListItem()
										{
											Text = $"{c.SalePointName} - {c.CostPointName}",
											Value = c.Id.ToString()
										})
										.ToList();

			return PartialView();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult AddMenu(Menu Menu)
		{
			if (ModelState.IsValid)
			{
				var postResult = Helper.PostValueToSevice<Menu>("POST", Menu);

				return Json(new { success = postResult.result, message = postResult.message });
			}

			return Json(new { success = false, message = "Model Is Not Vald!" });
		}

		public IActionResult EditMenu(long id)
		{
			ViewData["title"] = Helper.GetEntityTile<Menu>(EnumTitle.Edit);

			var model = GetMenuById(id);

			ViewBag.SaleCostPoint = Helper.GetServiceResponseList<SaleCostPoint>("Get?PageNumber=1&PageSize=10&searchKey=null&SortOrder=id&token=1")
										.Select(c => new SelectListItem()
										{
											Text = $"{c.SalePointName} - {c.CostPointName}",
											Value = c.Id.ToString(),
											Selected = c.Id == model.SaleCostPointId
										})
										.ToList();

			return PartialView(model);
		}

		[HttpPost]
		public IActionResult EditMenu(long id, Menu Menu)
		{
			if (ModelState.IsValid)
			{
				Menu.State = BusinessEntity.Models.Base.Enums.ObjectState.Active;

				var postResult = Helper.PostValueToSevice<Menu>("PUT?id=" + Menu.Id.ToString(), Menu);

				return Json(new { success = postResult.result, message = postResult.message });
			}

			return Json(new { success = false, message = "Model Is Not Valid!" });
		}

		public IActionResult DeleteMenu(long id)
		{
			ViewData["title"] = Helper.GetEntityTile<Menu>(EnumTitle.Delete);

			return PartialView(GetMenuById(id));
		}

		[HttpPost]
		public IActionResult DeleteMenu(Menu Menu)
		{
			var postResult = Helper.PostValueToSevice<Menu>("Delete?id=" + Menu.Id.ToString(), Menu);

			return Json(new { success = postResult.result, message = postResult.message });
		}

		private Menu GetMenuById(long id)
		{
			return (Helper.GetServiceResponse<Menu>("GetById?id=" + id.ToString()).data as List<Menu>)
				.FirstOrDefault();
		}
	}
}