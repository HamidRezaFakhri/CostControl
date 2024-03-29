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
    public class SettingController : BaseController
    {
        public IActionResult SettingList(string param)
        {
            ViewData["title"] = Helper.GetEntityTile<Setting>(EnumTitle.List);

            return View(Helper.GetServiceResponse<Setting>("Get?PageNumber=1&PageSize=10&searchKey=null&SortOrder=id&token=1"));
        }

        public IActionResult AddSetting()
        {
            ViewData["title"] = Helper.GetEntityTile<Setting>(EnumTitle.Add);

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSetting(Setting Setting)
        {
            if (ModelState.IsValid)
            {
                var postResult = Helper.PostValueToSevice<Setting>("POST", Setting);

                return Json(new { success = postResult.result, message = postResult.message });
            }

            return Json(new { success = false, message = "Model Is Not Vald!" });
        }

        public IActionResult EditSetting(long id)
        {
            ViewData["title"] = Helper.GetEntityTile<Setting>(EnumTitle.Edit);

            return PartialView(GetSettingById(id));
        }

        [HttpPost]
        public IActionResult EditSetting(long id, Setting Setting)
        {
            if (ModelState.IsValid)
            {
                Setting.State = BusinessEntity.Models.Base.Enums.ObjectState.Active;

                var postResult = Helper.PostValueToSevice<Setting>("PUT?id=" + Setting.Id.ToString(), Setting);

                return Json(new { success = postResult.result, message = postResult.message });
            }

            return Json(new { success = false, message = "Model Is Not Valid!" });
        }

        public IActionResult DeleteSetting(long id)
        {
            ViewData["title"] = Helper.GetEntityTile<Setting>(EnumTitle.Delete);

            return PartialView(GetSettingById(id));
        }

        [HttpPost]
        public IActionResult DeleteSetting(Setting Setting)
        {
            var postResult = Helper.PostValueToSevice<Setting>("Delete?id=" + Setting.Id.ToString(), Setting);

            return Json(new { success = postResult.result, message = postResult.message });
        }

        private Setting GetSettingById(long id)
        {
            return (Helper.GetServiceResponse<Setting>("GetById?id=" + id.ToString()).data as List<Setting>)
                .FirstOrDefault();
        }
    }
}