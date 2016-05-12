using System;
using System.Data;
using System.Data.Objects;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository : IDataRepository
    {
        private DataEntities context = new DataEntities();

        public DataRepository()
        {
            //context.Enterprises.MergeOption = MergeOption.NoTracking;
            //context.Properties.MergeOption = MergeOption.NoTracking;
            //context.Rooms.MergeOption = MergeOption.NoTracking;
        }

        public void SaveChanges()
        {
            try
            {
                context.SaveChanges();
            } catch (OptimisticConcurrencyException ocex)
            {
                context.Refresh(RefreshMode.StoreWins, ocex.StateEntries[0].Entity);
                throw ocex;
            }
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();
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
