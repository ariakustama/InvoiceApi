using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceAPI.ViewModel
{
    public class ParamSearchInvoiceViewModel
    {
        public DateTime? InvoiceDate { get; set; }
        public string InvoiceCode { get; set; }
        public string PurchaseOrderCode { get; set; }
        public int page { get; set; }
        public int itemPerPage { get; set; }
    }
}
