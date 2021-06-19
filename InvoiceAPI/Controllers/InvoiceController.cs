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
    public class InvoiceController : Controller
    {
        private InvoiceFacade _invoiceFacade;
        public InvoiceController(
           InvoiceFacade invoiceFacade
        )
        {
            _invoiceFacade = invoiceFacade;
        }

        [HttpPost]
        public JsonResult Post([FromBody] InvoiceViewModel param)
        {
            try
            {
                var newInvoice = _invoiceFacade.AddInvoice(param);
                return Json(new ApiResult<InvoiceViewModel>() { isSuccessful = true, Payload = newInvoice });
            }
            catch (Exception ex)
            {
                return Json(new ApiResult<InvoiceViewModel>() { isSuccessful = false, Payload = null, message = ex.Message });
            }
        }

        [HttpPost("GetInvoicesPagging")]
        public JsonResult GetInvoices([FromBody] ParamSearchInvoiceViewModel param)
        {
            try
            {
                var invoicesPagging = _invoiceFacade.GetInvoices(param);
                return Json(new ApiResult<GetInvoicesViewModel>() { isSuccessful = true, Payload = invoicesPagging });
            }
            catch (Exception ex)
            {
                return Json(new ApiResult<GetInvoicesViewModel>() { isSuccessful = false, Payload = null, message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public JsonResult Get(string id)
        {
            try
            {
                var dataInvoice = _invoiceFacade.GetInvoice(id);
                return Json(new ApiResult<InvoiceViewModel>() { isSuccessful = true, Payload = dataInvoice });
            }
            catch (Exception ex)
            {
                return Json(new ApiResult<InvoiceViewModel>() { isSuccessful = false, Payload = null, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public JsonResult Put(string id, [FromBody] InvoiceViewModel param)
        {
            try
            {
                var dataInvoiceUpdated = _invoiceFacade.UpdateInvoice(id, param);
                return Json(new ApiResult<InvoiceViewModel>() { isSuccessful = true, Payload = dataInvoiceUpdated });
            }
            catch (Exception ex)
            {
                return Json(new ApiResult<InvoiceViewModel>() { isSuccessful = false, Payload = null, message = ex.Message });
            }
        }
    }
}
