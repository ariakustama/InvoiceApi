using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceAPI.ViewModel
{
    public class ParamSearchPurchaseOrderViewModel
    {
        public string PurchaseOrderCode { get; set; }
        public string PurchaseOrderPic { get; set; }
        public int page { get; set; }
        public int itemPerPage { get; set; }
    }
}
