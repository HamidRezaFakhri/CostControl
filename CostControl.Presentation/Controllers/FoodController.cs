using CostControl.API.Models;
using CostControl.BusinessEntity.Models.CostControl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CostControl.Presentation.Controllers
{
    public class FoodController : BaseController
    {
        public IActionResult FoodList(string param)
        {
            ViewData["title"] = Helper.GetEntityTile<Food>(EnumTitle.List);

            return View(Helper.GetServiceResponse<Food>("Get?PageNumber=1&PageSize=10&searchKey=null&SortOrder=id&token=1"));
        }

        public IActionResult AddFood()
        {
            ViewData["title"] = Helper.GetEntityTile<Food>(EnumTitle.Add);
            
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
        public IActionResult AddFood(Food Food)
        {
            if (ModelState.IsValid)
            {
                var postResult = Helper.PostValueToSevice<Food>("POST", Food);

                return Json(new { success = postResult.result, message = postResult.message });
            }

            return Json(new { success = false, message = "Model Is Not Vald!" });
        }

        public IActionResult EditFood(long id)
        {
            ViewData["title"] = Helper.GetEntityTile<Food>(EnumTitle.Edit);

            var model = GetFoodById(id);

            ViewBag.SaleCostPoint = Helper.GetServiceResponseList<SaleCostPoint>("Get?PageNumber=1&PageSize=10&searchKey=null&SortOrder=id&token=1")
                            .Select(c => new SelectListItem()
                            {
                                Text = $"{c.SalePointName} - {c.CostPointName}",
                                Value = c.Id.ToString(),
                                Selected = c.Id == model.SaleCostPointId
                            })
                            .ToList();

            return PartialView(GetFoodById(id));
        }

        [HttpPost]
        public IActionResult EditFood(long id, Food Food)
        {
            if (ModelState.IsValid)
            {
                Food.State = BusinessEntity.Models.Base.Enums.ObjectState.Active;

                var postResult = Helper.PostValueToSevice<Food>("PUT?id=" + Food.Id.ToString(), Food);

                return Json(new { success = postResult.result, message = postResult.message });
            }

            return Json(new { success = false, message = "Model Is Not Valid!" });
        }

        public IActionResult DeleteFood(long id)
        {
            ViewData["title"] = Helper.GetEntityTile<Food>(EnumTitle.Delete);

            return PartialView(GetFoodById(id));
        }

        [HttpPost]
        public IActionResult DeleteFood(Food Food)
        {
            var postResult = Helper.PostValueToSevice<Food>("Delete?id=" + Food.Id.ToString(), Food);

            return Json(new { success = postResult.result, message = postResult.message });
        }

        private Food GetFoodById(long id)
        {
            return (Helper.GetServiceResponse<Food>("GetById?id=" + id.ToString()).data as List<Food>)
                .FirstOrDefault();
        }
    }
}