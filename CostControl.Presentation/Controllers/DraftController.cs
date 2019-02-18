using CostControl.BusinessEntity.Models.CostControl;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CostControl.Presentation.Controllers
{
    public class DraftController : BaseController
    {
        public IActionResult DraftList(string param)
        {
            ViewData["title"] = Helper.GetEntityTile<Draft>(EnumTitle.List);

            return View(Helper.GetServiceResponse<Draft>("Get?PageNumber=1&PageSize=10&searchKey=null&SortOrder=id&token=1"));
        }

        public IActionResult AddDraft()
        {
            ViewData["title"] = Helper.GetEntityTile<Draft>(EnumTitle.Add);

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddDraft(Draft Draft)
        {
            if (ModelState.IsValid)
            {
                var postResult = Helper.PostValueToSevice<Draft>("POST", Draft);

                return Json(new { success = postResult.result, message = postResult.message });
            }

            return Json(new { success = false, message = "Model Is Not Vald!" });
        }

        public IActionResult EditDraft(long id)
        {
            ViewData["title"] = Helper.GetEntityTile<Draft>(EnumTitle.Edit);

            return PartialView(GetDraftById(id));
        }

        [HttpPost]
        public IActionResult EditDraft(long id, Draft Draft)
        {
            if (ModelState.IsValid)
            {
                Draft.State = BusinessEntity.Models.Base.Enums.ObjectState.Active;

                var postResult = Helper.PostValueToSevice<Draft>("PUT?id=" + Draft.Id.ToString(), Draft);

                return Json(new { success = postResult.result, message = postResult.message });
            }

            return Json(new { success = false, message = "Model Is Not Valid!" });
        }

        public IActionResult DeleteDraft(long id)
        {
            ViewData["title"] = Helper.GetEntityTile<Draft>(EnumTitle.Delete);

            return PartialView(GetDraftById(id));
        }

        [HttpPost]
        public IActionResult DeleteDraft(Draft Draft)
        {
            var postResult = Helper.PostValueToSevice<Draft>("Delete?id=" + Draft.Id.ToString(), Draft);

            return Json(new { success = postResult.result, message = postResult.message });
        }

        private Draft GetDraftById(long id)
        {
            return (Helper.GetServiceResponse<Draft>("GetById?id=" + id.ToString()).data as List<Draft>)
                .FirstOrDefault();
        }
    }
}