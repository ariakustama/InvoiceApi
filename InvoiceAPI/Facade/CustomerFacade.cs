using InvoiceAPI.Helper;
using InvoiceAPI.Models.Db;
using InvoiceAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceAPI.Facade
{
    public class CustomerFacade
    {
        private InvoicedbDB _db;

        public CustomerFacade(
            InvoicedbDB db)
        {
            _db = db;
        }

        public GetCustomerViewModel GetCustomers(ParamSearchCustomerViewModel param)
        {
            try
            {
                GetCustomerViewModel objReturn = new GetCustomerViewModel();

                var predicate = PredicateBuilder.Create<Customer>(o => o.DeletedAt == null);

                if (!string.IsNullOrEmpty(param.CustomerName))
                    predicate = predicate.And(o => o.CustomerName.Contains(param.CustomerName));

                if (!string.IsNullOrEmpty(param.CustomerAddress))
                    predicate = predicate.And(o => o.CustomerAddress.Contains(param.CustomerAddress));

                objReturn.CountData = _db.Customers.Where(predicate).Count();

                List<Customer> dataCustomers = _db.Customers.Where(predicate)
                    .OrderBy(x => x.CustomerName).Skip((param.page - 1) * param.itemPerPage)
                    .Take(param.itemPerPage).ToList();

                objReturn.DataCustomers = new List<CustomerViewModel>();
                objReturn.DataCustomers = ViewModelToModel<List<CustomerViewModel>>.Convert(dataCustomers);

                return objReturn;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public List<string> GetCustomerForDropDown()
        {
            try
            {
                List<string> dataOptCustomers = _db.Customers.Where(x => x.DeletedAt == null)
                    .Select(x => "<option value=\"" + x.Id + "\">" + x.CustomerName + "</option>")
                    .ToList();
                return dataOptCustomers;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public CustomerViewModel GetCustomer(string id)
        {
            CustomerViewModel objCustomer = new CustomerViewModel();
            try
            {
                Customer dataCustomerExisting = _db.Customers.Where(x => x.Id == id).FirstOrDefault();

                if (dataCustomerExisting == null)
                    throw new ArgumentException("Data Invoice Not Found");

                return ViewModelToModel<CustomerViewModel>.Convert(dataCustomerExisting);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
