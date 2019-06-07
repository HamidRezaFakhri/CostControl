namespace CostControl.Presentation.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using CostControl.BusinessEntity.Models.CostControl;
	using Microsoft.AspNetCore.Mvc;

	public class CostPointGroupController : BaseController
	{
		public IActionResult CostPointGroupList(string param)
		{
			ViewData["title"] = Helper.GetEntityTitle<CostPointGroup>(EnumTitle.List);

			return View(Helper.GetServiceResponse<CostPointGroup>("Get?PageNumber=1&PageSize=1000&searchKey=null&SortOrder=id&token=1"));
		}

		public IActionResult AddCostPointGroupExternal()
		{
			ViewData["title"] = Helper.GetEntityTitle<CostPointGroup>(EnumTitle.Import);

			return PartialView(Helper.GetServiceResponseList("CostPointGroup", "GetExternalData"));
		}

		[HttpPost]
		public IActionResult AddCostPointGroupExternal(string id)
		{
			try
			{
				var postResult = Helper.PostValueToSevice<CostPointGroup>("AddExternalData?id=" + id.ToString(), null);

				return Json(new { success = postResult.result, message = postResult.message });
			}
			catch (Exception ex)
			{
				return Json(new
				{
					model = id,
					success = false,
					message = "External Insertaion Error!" + Environment.NewLine + ex.Message
				});
			}
		}

		public IActionResult AddCostPointGroup()
		{
			ViewData["title"] = Helper.GetEntityTitle<CostPointGroup>(EnumTitle.Add);

			return PartialView();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult AddCostPointGroup(CostPointGroup CostPointGroup)
		{
			if (ModelState.IsValid)
			{
				var postResult = Helper.PostValueToSevice<CostPointGroup>("POST", CostPointGroup);

				return Json(new { success = postResult.result, message = postResult.message });
			}

			return Json(new
			{
				model = CostPointGroup,
				success = false,
				message = ModelState
				.Values
				.FirstOrDefault(e => e.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
				.Errors
				.FirstOrDefault()
				.ErrorMessage ?? "Model Is Not Vald!"
			});
		}

		public IActionResult EditCostPointGroup(long id)
		{
			ViewData["title"] = Helper.GetEntityTitle<CostPointGroup>(EnumTitle.Edit);

			return PartialView(GetCostPointGroupById(id));
		}

		[HttpPost]
		public IActionResult EditCostPointGroup(long id, CostPointGroup CostPointGroup)
		{
			if (ModelState.IsValid)
			{
				CostPointGroup.State = BusinessEntity.Models.Base.Enums.ObjectState.Active;

				var postResult = Helper.PostValueToSevice<CostPointGroup>("PUT?id=" + CostPointGroup.Id.ToString(), CostPointGroup);

				return Json(new { success = postResult.result, message = postResult.message });
			}

			return Json(new
			{
				model = CostPointGroup,
				success = false,
				message = ModelState
				.Values
				.FirstOrDefault(e => e.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
				.Errors
				.FirstOrDefault()
				.ErrorMessage ?? "Model Is Not Vald!"
			});
		}

		public IActionResult DeleteCostPointGroup(long id)
		{
			ViewData["title"] = Helper.GetEntityTitle<CostPointGroup>(EnumTitle.Delete);

			return PartialView(GetCostPointGroupById(id));
		}

		[HttpPost]
		public IActionResult DeleteCostPointGroup(CostPointGroup CostPointGroup)
		{
			var postResult = Helper.PostValueToSevice<CostPointGroup>("Delete?id=" + CostPointGroup.Id.ToString(), CostPointGroup);

			return Json(new { success = postResult.result, message = postResult.message });
		}

		private CostPointGroup GetCostPointGroupById(long id)
		{
			return (Helper.GetServiceResponse<CostPointGroup>("GetById?id=" + id.ToString()).data as List<CostPointGroup>)
				.FirstOrDefault();
		}
	}
}