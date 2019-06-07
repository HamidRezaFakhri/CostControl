namespace CostControl.Presentation.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using CostControl.BusinessEntity.Models.CostControl;
    using Microsoft.AspNetCore.Mvc;

    public class InventoryController : BaseController
    {
        public IActionResult InventoryList(string param)
        {
            ViewData["title"] = Helper.GetEntityTitle<Inventory>(EnumTitle.List);

            return View(Helper.GetServiceResponse<Inventory>("Get?PageNumber=1&PageSize=1000&searchKey=null&SortOrder=id&token=1"));
        }

        public IActionResult AddInventory()
        {
            ViewData["title"] = Helper.GetEntityTitle<Inventory>(EnumTitle.Add);

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddInventory(Inventory Inventory)
        {
            if (ModelState.IsValid)
            {
                var postResult = Helper.PostValueToSevice<Inventory>("POST", Inventory);

                return Json(new { success = postResult.result, message = postResult.message });
            }

            return Json(new
            {
                model = Inventory,
                success = false,
                message = ModelState
                .Values
                .FirstOrDefault(e => e.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                .Errors
                .FirstOrDefault()
                .ErrorMessage ?? "Model Is Not Vald!"
            });
        }

        public IActionResult EditInventory(long id)
        {
            ViewData["title"] = Helper.GetEntityTitle<Inventory>(EnumTitle.Edit);

            return PartialView(GetInventoryById(id));
        }

        [HttpPost]
        public IActionResult EditInventory(long id, Inventory Inventory)
        {
            if (ModelState.IsValid)
            {
                Inventory.State = BusinessEntity.Models.Base.Enums.ObjectState.Active;

                var postResult = Helper.PostValueToSevice<Inventory>("PUT?id=" + Inventory.Id.ToString(), Inventory);

                return Json(new { success = postResult.result, message = postResult.message });
            }

            return Json(new
            {
                model = Inventory,
                success = false,
                message = ModelState
                .Values
                .FirstOrDefault(e => e.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                .Errors
                .FirstOrDefault()
                .ErrorMessage ?? "Model Is Not Vald!"
            });
        }

        public IActionResult DeleteInventory(long id)
        {
            ViewData["title"] = Helper.GetEntityTitle<Inventory>(EnumTitle.Delete);

            return PartialView(GetInventoryById(id));
        }

        [HttpPost]
        public IActionResult DeleteInventory(Inventory Inventory)
        {
            var postResult = Helper.PostValueToSevice<Inventory>("Delete?id=" + Inventory.Id.ToString(), Inventory);

            return Json(new { success = postResult.result, message = postResult.message });

        }

        private Inventory GetInventoryById(long id)
        {
            return (Helper.GetServiceResponse<Inventory>("GetById?id=" + id.ToString()).data as List<Inventory>)
                .FirstOrDefault();
        }
    }
}