using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceAPI.ViewModel
{
    public class InvoiceDetailViewModel
    {
        public string Id { get; set; }
        public string InvoiceId { get; set; }
        public string UomId { get; set; }
        public string InvoiceDetailName { get; set; }
        public string UomName { get; set; }
        public decimal InvoiceDetailRate { get; set; }
        public decimal InvoiceDetailQty { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime EditDate { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
