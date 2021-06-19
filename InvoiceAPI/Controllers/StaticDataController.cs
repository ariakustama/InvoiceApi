using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvoiceAPI.Facade;
using InvoiceAPI.Models;
using InvoiceAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaticDataController : Controller
    {
        private StaticDataFacade _staticDataFacade;
        public StaticDataController(StaticDataFacade staticDataFacade)
        {
            _staticDataFacade = staticDataFacade;
        }


        [HttpGet("getByGroup/{group}")]
        public JsonResult GetStaticDatasByGroup(string group)
        {
            try
            {
                var staticDatas = _staticDataFacade.GetStaticDataByGroup(group);
                return Json(new ApiResult<List<string>>() { isSuccessful = true, Payload = staticDatas });
            }
            catch (Exception ex)
            {
                return Json(new ApiResult<List<string>>() { isSuccessful = false, Payload = null, message = ex.Message });
            }
        }
    }
}
