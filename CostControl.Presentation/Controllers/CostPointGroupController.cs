using CostControl.BusinessEntity.Models.CostControl;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CostControl.Presentation.Controllers
{
    public class CostPointGroupController : BaseController
    {
        public IActionResult CostPointGroupList(string param)
        {
            ViewData["title"] = Helper.GetEntityTile<CostPointGroup>(EnumTitle.List);

            return View(Helper.GetServiceResponse<CostPointGroup>("Get?PageNumber=1&PageSize=10&searchKey=null&SortOrder=id&token=1"));
        }

        public IActionResult AddCostPointGroup()
        {
            ViewData["title"] = Helper.GetEntityTile<CostPointGroup>(EnumTitle.Add);

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

            return Json(new { success = false, message = "Model Is Not Vald!" });
        }

        public IActionResult EditCostPointGroup(long id)
        {
            ViewData["title"] = Helper.GetEntityTile<CostPointGroup>(EnumTitle.Edit);

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

            return Json(new { success = false, message = "Model Is Not Valid!" });
        }

        public IActionResult DeleteCostPointGroup(long id)
        {
            ViewData["title"] = Helper.GetEntityTile<CostPointGroup>(EnumTitle.Delete);

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