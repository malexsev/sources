using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public ChildAvaFile GetChildAvaFile(int childId)
        {
            var ava = context.ChildAvaFiles.FirstOrDefault(x => x.ChildId == childId);
            return ava ?? new ChildAvaFile();
        }

        public bool CheckChildAvaFile(int childId, string fileName)
        {
            return context.ChildAvaFiles.Any(x => x.ChildId == childId && x.FileName == fileName);
        }

        public void InsertChildAvaFile(ChildAvaFile childAvaFile)
        {
            try
            {
                context.ChildAvaFiles.AddObject(childAvaFile);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteChildAvaFile(ChildAvaFile childAvaFile)
        {
            try
            {
                context.ChildAvaFiles.Attach(childAvaFile);
                context.ChildAvaFiles.DeleteObject(childAvaFile);
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
