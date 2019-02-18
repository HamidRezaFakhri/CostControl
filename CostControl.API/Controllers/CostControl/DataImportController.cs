using CostControl.API.Controllers.Base;
using CostControl.BusinessEntity.Models.CostControl;
using CostControl.BusinessLogic.Logics.CostControl;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CostControl.API.Controllers.CostControl
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class DataImportController : BaseApiController<DataImport, DataImportLogic, long>
    {
        [HttpGet("GetData")]
        public ActionResult<string> GetData()
        {
            var message = "";

            var state = BusinessEntity.Models.Base.Enums.ObjectState.Active;

            try
            {
                (PDKBusinessLogic as DataImportLogic).GetData();

                message = "Getting data has been completed.";
            }
            catch(Exception ex)
            {
                state = BusinessEntity.Models.Base.Enums.ObjectState.Passive;
                message = "Fail to Get Data!" + Environment.NewLine + ex.Message;
            }
            
            PDKBusinessLogic.Add(new DataImport { ImportTime = DateTime.Now, State = state });

            return message;
        }
    }
}