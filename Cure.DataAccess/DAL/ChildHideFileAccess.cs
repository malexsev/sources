using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public IEnumerable<ChildHideFile> GetChildHideFiles(int childId)
        {
            return context.ChildHideFiles.Where(x => x.ChildId == childId).ToList();
        }

        public bool CheckChildHideFile(int childId, string fileName)
        {
            return context.ChildHideFiles.Any(x => x.ChildId == childId && x.FileName == fileName);
        }

        public void InsertChildHideFile(ChildHideFile childHideFile)
        {
            try
            {
                context.ChildHideFiles.AddObject(childHideFile);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteChildHideFile(int childId, string fileName)
        {
            try
            {
                var hide = context.ChildHideFiles.FirstOrDefault(x => x.ChildId == childId && x.FileName == fileName);
                if (hide != null)
                {
                    context.ChildHideFiles.Attach(hide);
                    context.ChildHideFiles.DeleteObject(hide);
                    SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteChildHideFile(ChildHideFile childHideFile)
        {
            try
            {
                context.ChildHideFiles.Attach(childHideFile);
                context.ChildHideFiles.DeleteObject(childHideFile);
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
