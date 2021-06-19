using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceAPI.ViewModel
{
    public class PurchaseOrderViewModel
    {
        public string Id { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public decimal Amount { get; set; }
        public string PIC { get; set; }
        public DateTime? DeletedAt { get; set; }
    }

    public class GetPurchaseOrdersViewModel
    {
        public List<PurchaseOrderViewModel> DataPurchaseOrders  { get; set; }
        public int CountData { get; set; }
    }
}
