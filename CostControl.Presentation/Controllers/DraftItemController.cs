﻿using CostControl.API.Models;
using CostControl.BusinessEntity.Models.CostControl;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CostControl.Presentation.Controllers
{
    public class DraftItemController : BaseController
    {
        public IActionResult DraftItemList(string param)
        {
            ViewData["title"] = Helper.GetEntityTile<DraftItem>(EnumTitle.List);

            return View(Helper.GetServiceResponse<DraftItem>("Get?PageNumber=1&PageSize=10&searchKey=null&SortOrder=id&token=1"));
        }

        public IActionResult AddDraftItem()
        {
            ViewData["title"] = Helper.GetEntityTile<DraftItem>(EnumTitle.Add);

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddDraftItem(DraftItem DraftItem)
        {
            if (ModelState.IsValid)
            {
                var postResult = Helper.PostValueToSevice<DraftItem>("POST", DraftItem);

                return Json(new { success = postResult.result, message = postResult.message });
            }

            return Json(new { success = false, message = "Model Is Not Vald!" });
        }

        public IActionResult EditDraftItem(long id)
        {
            ViewData["title"] = Helper.GetEntityTile<DraftItem>(EnumTitle.Edit);

            return PartialView(GetDraftItemById(id));
        }

        [HttpPost]
        public IActionResult EditDraftItem(long id, DraftItem DraftItem)
        {
            if (ModelState.IsValid)
            {
                DraftItem.State = BusinessEntity.Models.Base.Enums.ObjectState.Active;

                var postResult = Helper.PostValueToSevice<DraftItem>("PUT?id=" + DraftItem.Id.ToString(), DraftItem);

                return Json(new { success = postResult.result, message = postResult.message });
            }

            return Json(new { success = false, message = "Model Is Not Valid!" });
        }

        public IActionResult DeleteDraftItem(long id)
        {
            ViewData["title"] = Helper.GetEntityTile<DraftItem>(EnumTitle.Delete);

            return PartialView(GetDraftItemById(id));
        }

        [HttpPost]
        public IActionResult DeleteDraftItem(DraftItem DraftItem)
        {
            var postResult = Helper.PostValueToSevice<DraftItem>("Delete?id=" + DraftItem.Id.ToString(), DraftItem);

            return Json(new { success = postResult.result, message = postResult.message });
        }

        private DraftItem GetDraftItemById(long id)
        {
            return (Helper.GetServiceResponse<DraftItem>("GetById?id=" + id.ToString()).data as List<DraftItem>)
                .FirstOrDefault();
        }
    }
}