namespace CostControl.Presentation.Controllers
{
	using System.Collections.Generic;
	using System.Linq;
	using CostControl.BusinessEntity.Models.CostControl;
	using Microsoft.AspNetCore.Mvc;

	public class IngredientController : BaseController
	{
		public IActionResult IngredientList(string param)
		{
			ViewData["title"] = Helper.GetEntityTile<Ingredient>(EnumTitle.List);

			return View(Helper.GetServiceResponse<Ingredient>("Get?PageNumber=1&PageSize=10&searchKey=null&SortOrder=id&token=1"));
		}

		public IActionResult AddIngredient()
		{
			ViewData["title"] = Helper.GetEntityTile<Ingredient>(EnumTitle.Add);

			return PartialView();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult AddIngredient(Ingredient Ingredient)
		{
			if (ModelState.IsValid)
			{
				var postResult = Helper.PostValueToSevice<Ingredient>("POST", Ingredient);

				return Json(new { success = postResult.result, message = postResult.message });
			}

			return Json(new { success = false, message = "Model Is Not Vald!" });
		}

		public IActionResult EditIngredient(long id)
		{
			ViewData["title"] = Helper.GetEntityTile<Ingredient>(EnumTitle.Edit);

			return PartialView(GetIngredientById(id));
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

			return Json(new { success = false, message = "Model Is Not Valid!" });
		}

		public IActionResult DeleteIngredient(long id)
		{
			ViewData["title"] = Helper.GetEntityTile<Ingredient>(EnumTitle.Delete);

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