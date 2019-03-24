namespace CostControl.Presentation.Controllers
{
	using System.Collections.Generic;
	using System.Linq;
	using CostControl.BusinessEntity.Models.CostControl;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Rendering;

	public class SaleCostPointController : BaseController
	{
		public IActionResult SaleCostPointList(string param, int pageNumber, int pageSize)
		{
			ViewData["title"] = Helper.GetEntityTile<SaleCostPoint>(EnumTitle.List);

			return View(Helper.GetServiceResponse<SaleCostPoint>("Get?PageNumber=1&PageSize=10&searchKey=null&SortOrder=id&token=1"));
		}

		public IActionResult AddSaleCostPoint()
		{
			ViewData["title"] = Helper.GetEntityTile<SaleCostPoint>(EnumTitle.Add);

			ViewBag.CostPoints = GetCostPoints()
							.Select(c => new SelectListItem()
							{
								Text = c.Name,
								Value = c.Id.ToString()
							})
							.ToList();

			ViewBag.SalePoints = GetSalePoints()
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
		public IActionResult AddSaleCostPoint(SaleCostPoint SaleCostPoint)
		{
			if (ModelState.IsValid)
			{
				var postResult = Helper.PostValueToSevice<SaleCostPoint>("POST", SaleCostPoint);

				return Json(new { success = postResult.result, message = postResult.message });
			}

			return Json(new { success = false, message = "Model Is Not Vald!" });
		}

		public IActionResult EditSaleCostPoint(long id)
		{
			ViewData["title"] = Helper.GetEntityTile<SaleCostPoint>(EnumTitle.Edit);

			var model = GetSaleCostPointById(id);

			ViewBag.CostPoints = GetCostPoints()
							.Select(c => new SelectListItem()
							{
								Text = c.Name,
								Value = c.Id.ToString(),
								Selected = c.Id == model.CostPointId
							})
							.ToList();

			ViewBag.SalePoints = GetSalePoints()
										.Select(c => new SelectListItem()
										{
											Text = c.Name,
											Value = c.Id.ToString(),
											Selected = c.Id == model.SalePointId
										})
										.ToList();

			return PartialView(GetSaleCostPointById(id));
		}

		[HttpPost]
		public IActionResult EditSaleCostPoint(long id, SaleCostPoint SaleCostPoint)
		{
			if (ModelState.IsValid)
			{
				SaleCostPoint.State = BusinessEntity.Models.Base.Enums.ObjectState.Active;

				var postResult = Helper.PostValueToSevice<SaleCostPoint>("PUT?id=" + SaleCostPoint.Id.ToString(), SaleCostPoint);

				return Json(new { success = postResult.result, message = postResult.message });
			}

			return Json(new { success = false, message = "Model Is Not Valid!" });
		}

		public IActionResult DeleteSaleCostPoint(long id)
		{
			ViewData["title"] = Helper.GetEntityTile<SaleCostPoint>(EnumTitle.Delete);

			return PartialView(GetSaleCostPointById(id));
		}

		[HttpPost]
		public IActionResult DeleteSaleCostPoint(SaleCostPoint SaleCostPoint)
		{
			var postResult = Helper.PostValueToSevice<SaleCostPoint>("Delete?id=" + SaleCostPoint.Id.ToString(), SaleCostPoint);

			return Json(new { success = postResult.result, message = postResult.message });
		}

		private SaleCostPoint GetSaleCostPointById(long id)
		{
			return (Helper.GetServiceResponse<SaleCostPoint>("GetById?id=" + id.ToString()).data as List<SaleCostPoint>)
				.FirstOrDefault();
		}

		private IEnumerable<SalePoint> GetSalePoints()
		{
			return Helper.GetServiceResponseList<SalePoint>("Get?PageNumber=1&PageSize=10&searchKey=null&SortOrder=id&token=1");
		}

		private CostPoint GetCostPointById(long id)
		{
			return (Helper.GetServiceResponse<CostPoint>("GetById?id=" + id.ToString()).data as List<CostPoint>)
				.FirstOrDefault();
		}

		private SalePoint GetSalePointById(long id)
		{
			return (Helper.GetServiceResponse<SalePoint>("GetById?id=" + id.ToString()).data as List<SalePoint>)
				.FirstOrDefault();
		}

		private IEnumerable<CostPoint> GetCostPoints()
		{
			return Helper.GetServiceResponseList<CostPoint>("Get?PageNumber=1&PageSize=10&searchKey=null&SortOrder=id&token=1");
		}
	}
}