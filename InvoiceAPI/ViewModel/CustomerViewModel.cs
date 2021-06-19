using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceAPI.ViewModel
{
    public class CustomerViewModel
    {
        public string Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CUstomerLogo { get; set; }
        public DateTime? DeletedAt { get; set; }
    }

    public class GetCustomerViewModel
    {
        public List<CustomerViewModel> DataCustomers  { get; set; }
        public int CountData { get; set; }
    }
}
