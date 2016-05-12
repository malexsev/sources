using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cure.DataAccess.DAL;

namespace Cure.DataAccess.BLL
{
    public partial class DataAccessBL
    {

        private IDataRepository dataRepository;
        public DataAccessBL()
        {
            this.dataRepository = new DataRepository();
        }

        internal DataAccessBL(IDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        private bool disposedValue = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    dataRepository.Dispose();
                }
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
