using InvoiceAPI.Helper;
using InvoiceAPI.Models.Db;
using InvoiceAPI.ViewModel;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceAPI.Facade
{
    public class InvoiceFacade
    {
        private InvoicedbDB _db;

        public InvoiceFacade(
            InvoicedbDB db)
        {
            _db = db;
        }

        public InvoiceViewModel AddInvoice(InvoiceViewModel model) 
        {
            _db.BeginTransaction();
            try
            {
                model.Id = Guid.NewGuid().ToString();
                model.AddDate = DateTime.Now;
                model.EditDate = DateTime.Now;
                model.InvoiceNumber = $"INV-{String.Format("{0:D3}", _db.Invoices.Count() + 1)}";
                _db.Insert(ViewModelToModel<Invoice>.Convert(model));

                for (int i = 0; i < model.invoiceDetails.Count(); i++)
                {
                    model.invoiceDetails[i].Id = Guid.NewGuid().ToString();
                    model.invoiceDetails[i].InvoiceId = model.Id;
                    model.invoiceDetails[i].AddDate = DateTime.Now;
                    model.invoiceDetails[i].EditDate = DateTime.Now;

                    _db.Insert(ViewModelToModel<InvoiceDetail>.Convert(model.invoiceDetails[i]));
                }

                _db.CommitTransaction();
                return model;
            }
            catch (Exception ex)
            {
                _db.RollbackTransaction();
                throw new ArgumentException(ex.Message);
            }
        }

        public GetInvoicesViewModel GetInvoices(ParamSearchInvoiceViewModel param)
        {
            try
            {
                GetInvoicesViewModel objReturn = new GetInvoicesViewModel();

                var predicate = PredicateBuilder.Create<Invoice>(o => o.DeletedAt == null);

                if (param.InvoiceDate != null)
                    predicate = predicate.And(o => o.InvoiceDate == param.InvoiceDate.Value);

                if (!string.IsNullOrEmpty(param.InvoiceCode))
                    predicate = predicate.And(o => o.InvoiceNumber.Contains(param.InvoiceCode));

                if (!string.IsNullOrEmpty(param.PurchaseOrderCode))
                    predicate = predicate.And(o => o.PurchaseOrderNumber.Contains(param.PurchaseOrderCode));

                objReturn.CountData = _db.Invoices.Where(predicate).Count();

                List<Invoice> dataInvoices = _db.Invoices.Where(predicate)
                    .OrderBy(x => x.AddDate).Skip((param.page - 1) * param.itemPerPage)
                    .Take(param.itemPerPage).ToList();

                objReturn.DataInvoices = new List<InvoiceViewModel>();
                objReturn.DataInvoices = ViewModelToModel<List<InvoiceViewModel>>.Convert(dataInvoices);

                return objReturn;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public InvoiceViewModel GetInvoice(string id)
        {
            InvoiceViewModel objInvoice = new InvoiceViewModel();
            try
            {
                Invoice dataInvoiceExisting = _db.Invoices.Where(x => x.Id == id).FirstOrDefault();

                if (dataInvoiceExisting == null)
                    throw new ArgumentException("Data Invoice Not Found");

                objInvoice = ViewModelToModel<InvoiceViewModel>.Convert(dataInvoiceExisting);

                List<InvoiceDetail> invoiceDetails = _db.InvoiceDetails.Where(x => x.InvoiceId == id && x.DeletedAt == null).ToList();
                objInvoice.invoiceDetails = new List<InvoiceDetailViewModel>();
                objInvoice.invoiceDetails = ViewModelToModel<List<InvoiceDetailViewModel>>.Convert(invoiceDetails);

                return objInvoice;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public InvoiceViewModel UpdateInvoice(string id, InvoiceViewModel model)
        {
            _db.BeginTransaction();
            try
            {
                Invoice dataInvoiceExisting = _db.Invoices.Where(x => x.Id == id).FirstOrDefault();

                if (dataInvoiceExisting == null)
                    throw new ArgumentException("Data Invoice Not Found");

                Invoice invoiceToUpdate = ViewModelToModel<Invoice>.Convert(model);
                invoiceToUpdate.Id = dataInvoiceExisting.Id;
                invoiceToUpdate.InvoiceNumber = dataInvoiceExisting.InvoiceNumber;
                invoiceToUpdate.AddDate = dataInvoiceExisting.AddDate;
                invoiceToUpdate.EditDate = dataInvoiceExisting.EditDate;

                _db.Update(invoiceToUpdate);

                for (int i = 0; i < model.invoiceDetails.Count(); i++)
                {
                    if (string.IsNullOrEmpty(model.invoiceDetails[i].Id))
                    {
                        model.invoiceDetails[i].Id = Guid.NewGuid().ToString();
                        model.invoiceDetails[i].InvoiceId = dataInvoiceExisting.Id;
                        model.invoiceDetails[i].AddDate = DateTime.Now;
                        model.invoiceDetails[i].EditDate = DateTime.Now;

                        _db.Insert(ViewModelToModel<InvoiceDetail>.Convert(model.invoiceDetails[i]));
                    }
                    else
                    {
                        InvoiceDetail invoiceDetailExting = _db.InvoiceDetails.Where(x => x.Id == model.invoiceDetails[i].Id).FirstOrDefault();
                        if (invoiceDetailExting == null)
                            throw new ArgumentException("Data Detail Invoice Not Found");

                        InvoiceDetail invoiceDetailToUpdate = ViewModelToModel<InvoiceDetail>.Convert(model.invoiceDetails[i]);
                        invoiceDetailToUpdate.AddDate = invoiceDetailExting.AddDate;
                        invoiceDetailToUpdate.EditDate = DateTime.Now;

                        _db.Update(invoiceDetailToUpdate);
                    }
                }

                _db.CommitTransaction();
                return model;
            }
            catch (Exception ex)
            {
                _db.RollbackTransaction();
                throw new ArgumentException(ex.Message);
            }
        }

        public InvoiceViewModel DeleteInvoice(string id)
        {
            try
            {
                Invoice dataInvoiceExisting = _db.Invoices.Where(x => x.Id == id).FirstOrDefault();

                if (dataInvoiceExisting == null)
                    throw new ArgumentException("Data Invoice Not Found");

                dataInvoiceExisting.DeletedAt = DateTime.Now;

                _db.Update(dataInvoiceExisting);

                return ViewModelToModel<InvoiceViewModel>.Convert(dataInvoiceExisting);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
