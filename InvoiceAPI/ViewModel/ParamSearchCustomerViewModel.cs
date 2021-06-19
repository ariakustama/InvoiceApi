using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceAPI.ViewModel
{
    public class ParamSearchCustomerViewModel
    {
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public int page { get; set; }
        public int itemPerPage { get; set; }
    }
}
