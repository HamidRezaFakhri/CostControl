namespace CostControl.Presentation.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using CostControl.BusinessEntity.Models.CostControl;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Rendering;

	public class IngredientController : BaseController
	{
		public IActionResult IngredientList(string param)
		{
			ViewData["title"] = Helper.GetEntityTitle<Ingredient>(EnumTitle.List);

			return View(Helper.GetServiceResponse<Ingredient>("Get?PageNumber=1&PageSize=1000&searchKey=null&SortOrder=id&token=1"));
		}

		public IActionResult AddIngredientExternal()
		{
			ViewData["title"] = Helper.GetEntityTitle<Ingredient>(EnumTitle.Import);

			return PartialView(Helper.GetServiceResponseList("Ingredient", "GetExternalData"));
		}

		[HttpPost]
		public IActionResult AddIngredientExternal(string id)
		{
			try
			{
				var postResult = Helper.PostValueToSevice<Ingredient>("AddExternalData?id=" + id.ToString(), null);

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

		public IActionResult AddIngredient()
		{
			ViewData["title"] = Helper.GetEntityTitle<Ingredient>(EnumTitle.Add);

			ViewBag.ConsumptionUnit = GetConsumptionUnitList();

			return PartialView();
		}

		private IEnumerable<ConsumptionUnit> GetConsumptionUnits()
		=> Helper.GetServiceResponseList<ConsumptionUnit>("Get?PageNumber=1&PageSize=1000&searchKey=null&SortOrder=id&token=1");

		private IEnumerable<SelectListItem> GetConsumptionUnitList(long? selected = null)
		=>
			GetConsumptionUnits()
				.OrderBy(o => o.Name)
				.Select(c => new SelectListItem()
				{
					Text = c.Name,
					Value = c.Id.ToString(),
					Selected = selected == null ? false : c.Id == selected
				})
				.ToList();

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult AddIngredient(Ingredient Ingredient)
		{
			if (ModelState.IsValid)
			{
				var postResult = Helper.PostValueToSevice<Ingredient>("POST", Ingredient);

				return Json(new { success = postResult.result, message = postResult.message });
			}

			return Json(new
			{
				model = Ingredient,
				success = false,
				message = ModelState
				.Values
				.FirstOrDefault(e => e.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
				.Errors
				.FirstOrDefault()
				.ErrorMessage ?? "Model Is Not Vald!"
			});
		}

		public IActionResult EditIngredient(long id)
		{
			ViewData["title"] = Helper.GetEntityTitle<Ingredient>(EnumTitle.Edit);

			var ingredient = GetIngredientById(id);
			var consumptionUnit = GetConsumptionUnitById(ingredient.ConsumptionUnitId);

			ViewBag.ConsumptionUnit = GetConsumptionUnitList(consumptionUnit.Id);

			return PartialView(ingredient);
		}

		private ConsumptionUnit GetConsumptionUnitById(long id)
		{
			return (Helper.GetServiceResponse<ConsumptionUnit>("GetById?id=" + id.ToString()).data as List<ConsumptionUnit>)
				.FirstOrDefault();
		}

		[HttpPost]
		public IActionResult EditIngredient(long id, Ingredient Ingredient)
		{
			if (ModelState.IsValid)
			{
				Ingredient.State = BusinessEntity.Models.Base.Enums.ObjectState.Active;

				var postResult = Helper.PostValueToSevice<Ingredient>("PUT?id=" + Ingredient.Id.ToString(), Ingredient);

				return Json(new { success = postResult.result, message = postResult.message });
			}

			return Json(new
			{
				model = Ingredient,
				success = false,
				message = ModelState
				.Values
				.FirstOrDefault(e => e.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
				.Errors
				.FirstOrDefault()
				.ErrorMessage ?? "Model Is Not Vald!"
			});
		}

		public IActionResult DeleteIngredient(long id)
		{
			ViewData["title"] = Helper.GetEntityTitle<Ingredient>(EnumTitle.Delete);

			return PartialView(GetIngredientById(id));
		}

		[HttpPost]
		public IActionResult DeleteIngredient(Ingredient Ingredient)
		{
			var postResult = Helper.PostValueToSevice<Ingredient>("Delete?id=" + Ingredient.Id.ToString(), Ingredient);

			return Json(new { success = postResult.result, message = postResult.message });
		}

		private Ingredient GetIngredientById(long id)
		{
			return (Helper.GetServiceResponse<Ingredient>("GetById?id=" + id.ToString()).data as List<Ingredient>)
				.FirstOrDefault();
		}
	}
}