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
    public class CustomerController : Controller
    {
        private CustomerFacade _CustomerFacade;
        public CustomerController(CustomerFacade CustomerFacade)
        {
            _CustomerFacade = CustomerFacade;
        }

        [HttpPost]
        public JsonResult Post()
        {
            try
            {
                return Json(new ApiResult<bool>() { isSuccessful = true, Payload = true });
            }
            catch (Exception ex)
            {
                return Json(new ApiResult<bool>() { isSuccessful = false, Payload = false, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public JsonResult Put()
        {
            try
            {
                return Json(new ApiResult<bool>() { isSuccessful = true, Payload = true });
            }
            catch (Exception ex)
            {
                return Json(new ApiResult<bool>() { isSuccessful = false, Payload = false, message = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult Get()
        {
            try
            {
                var dataCUstomers = _CustomerFacade.GetCustomerForDropDown();
                return Json(new ApiResult<List<string>>() { isSuccessful = true, Payload = dataCUstomers });
            }
            catch (Exception ex)
            {
                return Json(new ApiResult<List<string>>() { isSuccessful = false, Payload = null, message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public JsonResult Get(string id)
        {
            try
            {
                var dataCUstomer = _CustomerFacade.GetCustomer(id);
                return Json(new ApiResult<CustomerViewModel>() { isSuccessful = true, Payload = dataCUstomer });
            }
            catch (Exception ex)
            {
                return Json(new ApiResult<CustomerViewModel>() { isSuccessful = false, Payload = null, message = ex.Message });
            }
        }
    }
}
