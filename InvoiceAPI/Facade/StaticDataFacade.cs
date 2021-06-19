using InvoiceAPI.Helper;
using InvoiceAPI.Models.Db;
using InvoiceAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceAPI.Facade
{
    public class StaticDataFacade
    {
        private InvoicedbDB _db;

        public StaticDataFacade(
            InvoicedbDB db)
        {
            _db = db;
        }

        public List<string> GetStaticDataByGroup(string pGroup)
        {
            try
            {
                List<string> dataStaticDatas = _db.StaticDatas.Where(x => x.StaticGroup == pGroup)
                    .Select(x => "<option value=\"" + x.Id + "\" code=\"" + x.StaticCode + "\">" + x.StaticName + " - " + x.StaticCode + "</option>")
                    .ToList();
                return dataStaticDatas;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
