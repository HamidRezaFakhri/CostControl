namespace CostControl.Presentation.Controllers
{
	using System.Collections.Generic;
	using System.Linq;
	using CostControl.BusinessEntity.Models.CostControl;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Rendering;

	public class CostPointController : BaseController
	{
		public IActionResult CostPointList(string param)
		{
			ViewData["title"] = Helper.GetEntityTile<CostPoint>(EnumTitle.List);

			var a = Helper.GetServiceResponse<CostPoint>("Get?PageNumber=1&PageSize=10&searchKey=null&SortOrder=id&token=1");

			return View(a);
		}

		private IEnumerable<CostPointGroup> GetCostPointGroups()
		{
			return Helper.GetServiceResponseList<CostPointGroup>("Get?PageNumber=1&PageSize=10&searchKey=null&SortOrder=id&token=1");
		}

		public IActionResult AddCostPoint()
		{
			ViewData["title"] = Helper.GetEntityTile<CostPoint>(EnumTitle.Add);

			ViewBag.CostPointGroup = GetCostPointGroups()
										.Select(c => new SelectListItem()
										{
											Text = c.Name,
											Value = c.Id.ToString()
										})
										.ToList();
			return PartialView();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult AddCostPoint(CostPoint CostPoint)
		{
			if (ModelState.IsValid)
			{
				var postResult = Helper.PostValueToSevice<CostPoint>("POST", CostPoint);

				return Json(new { success = postResult.result, message = postResult.message });
			}

			return Json(new { success = false, message = "Model Is Not Vald!" });
		}

		public IActionResult EditCostPoint(long id)
		{
			ViewData["title"] = Helper.GetEntityTile<CostPoint>(EnumTitle.Edit);

			ViewBag.CostPointGroup = GetCostPointGroups()
							.Select(c => new SelectListItem()
							{
								Text = c.Name,
								Value = c.Id.ToString(),
								Selected = c.Id == GetCostPointGroupById(id).Id
							})
							.ToList();

			return PartialView(GetCostPointById(id));
		}

		private CostPointGroup GetCostPointGroupById(long id)
		{
			return (Helper.GetServiceResponse<CostPointGroup>("GetById?id=" + id.ToString()).data as List<CostPointGroup>)
				.FirstOrDefault();
		}

		[HttpPost]
		public IActionResult EditCostPoint(long id, CostPoint CostPoint)
		{
			if (ModelState.IsValid)
			{
				CostPoint.State = BusinessEntity.Models.Base.Enums.ObjectState.Active;

				var postResult = Helper.PostValueToSevice<CostPoint>("PUT?id=" + CostPoint.Id.ToString(), CostPoint);

				return Json(new { success = postResult.result, message = postResult.message });
			}

			return Json(new { success = false, message = "Model Is Not Valid!" });
		}

		public IActionResult DeleteCostPoint(long id)
		{
			ViewData["title"] = Helper.GetEntityTile<CostPoint>(EnumTitle.Delete);

			return PartialView(GetCostPointById(id));
		}

		[HttpPost]
		public IActionResult DeleteCostPoint(CostPoint CostPoint)
		{
			var postResult = Helper.PostValueToSevice<CostPoint>("Delete?id=" + CostPoint.Id.ToString(), CostPoint);

			return Json(new { success = postResult.result, message = postResult.message });
		}

		private CostPoint GetCostPointById(long id)
		{
			return (Helper.GetServiceResponse<CostPoint>("GetById?id=" + id.ToString()).data as List<CostPoint>)
				.FirstOrDefault();
		}
	}
}