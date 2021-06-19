using InvoiceAPI.Helper;
using InvoiceAPI.Models.Db;
using InvoiceAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceAPI.Facade
{
    public class PurchaseOrderFacade
    {
        private InvoicedbDB _db;

        public PurchaseOrderFacade(
            InvoicedbDB db)
        {
            _db = db;
        }

        public GetPurchaseOrdersViewModel GetPurchaseOrders(ParamSearchPurchaseOrderViewModel param) 
        {
            try
            {
                GetPurchaseOrdersViewModel objReturn = new GetPurchaseOrdersViewModel();

                var predicate = PredicateBuilder.Create<PurchaseOrder>(o => o.DeletedAt == null);

                if (!string.IsNullOrEmpty(param.PurchaseOrderCode))
                    predicate = predicate.And(o => o.PurchaseOrderNumber == param.PurchaseOrderCode);

                if (!string.IsNullOrEmpty(param.PurchaseOrderPic))
                    predicate = predicate.And(o => o.PIC == param.PurchaseOrderPic);

                objReturn.CountData = _db.PurchaseOrders.Where(predicate).Count();

                List<PurchaseOrder> dataPurchaseOrder = _db.PurchaseOrders.Where(predicate)
                    .OrderBy(x => x.PurchaseOrderNumber).Skip((param.page - 1) * param.itemPerPage)
                    .Take(param.itemPerPage).ToList();

                objReturn.DataPurchaseOrders = new List<PurchaseOrderViewModel>();
                objReturn.DataPurchaseOrders = ViewModelToModel<List<PurchaseOrderViewModel>>.Convert(dataPurchaseOrder);

                return objReturn;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public PurchaseOrderViewModel GetPurchaseOrder(string id)
        {
            PurchaseOrderViewModel objPurchaseOrder = new PurchaseOrderViewModel();
            try
            {
                PurchaseOrder dataPurchaseOrderExisting = _db.PurchaseOrders.Where(x => x.Id == id).FirstOrDefault();

                if (dataPurchaseOrderExisting == null)
                    throw new ArgumentException("Data Invoice Not Found");

                return ViewModelToModel<PurchaseOrderViewModel>.Convert(dataPurchaseOrderExisting);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
