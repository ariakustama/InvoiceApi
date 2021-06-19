using InvoiceAPI.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceAPI.ViewModel
{
    public class InvoiceViewModel
    {
        public string Id { get; set; }
        public string PurchaseOrderId { get; set; }
        public string CustomerId { get; set; }
        public string CurrencyId { get; set; }
        public string LanguageId { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string CustomerName { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime InvoiceDue { get; set; }
        public string InvoiceFrom { get; set; }
        public decimal InvoiceDisc { get; set; }
        public string InvoiceStatus { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime EditDate { get; set; }
        public DateTime? DeletedAt { get; set; }
        public List<InvoiceDetailViewModel> invoiceDetails { get; set; }
    }

    public class GetInvoicesViewModel
    {
        public List<InvoiceViewModel> DataInvoices { get; set; }
        public int CountData { get; set; }
    }
}
