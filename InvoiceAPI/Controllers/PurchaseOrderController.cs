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
    public class PurchaseOrderController : Controller
    {
        private PurchaseOrderFacade _purchaseOrderFacade;
        public PurchaseOrderController(PurchaseOrderFacade purchaseOrderFacade)
        {
            _purchaseOrderFacade = purchaseOrderFacade;
        }

        [HttpPost("GetPurchaseOrdersPagging")]
        public JsonResult GetInvoices([FromBody] ParamSearchPurchaseOrderViewModel param)
        {
            try
            {
                var purchaseOrderPagging = _purchaseOrderFacade.GetPurchaseOrders(param);
                return Json(new ApiResult<GetPurchaseOrdersViewModel>() { isSuccessful = true, Payload = purchaseOrderPagging });
            }
            catch (Exception ex)
            {
                return Json(new ApiResult<GetPurchaseOrdersViewModel>() { isSuccessful = false, Payload = null, message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public JsonResult Get(string id)
        {
            try
            {
                var dataPurchaseOrder = _purchaseOrderFacade.GetPurchaseOrder(id);
                return Json(new ApiResult<PurchaseOrderViewModel>() { isSuccessful = true, Payload = dataPurchaseOrder });
            }
            catch (Exception ex)
            {
                return Json(new ApiResult<PurchaseOrderViewModel>() { isSuccessful = false, Payload = null, message = ex.Message });
            }
        }
    }
}
