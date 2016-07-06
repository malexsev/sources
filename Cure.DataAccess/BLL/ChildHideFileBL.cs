namespace Cure.DataAccess.BLL
{
    using System;
    using System.Collections.Generic;

    public partial class DataAccessBL
    {
        public IEnumerable<ChildHideFile> GetChildHideFiles(int childId)
        {
            return dataRepository.GetChildHideFiles(childId);
        }

        public bool CheckChildHideFile(int childId, string fileName)
        {
            return dataRepository.CheckChildHideFile(childId, fileName);
        }

        public void InsertChildHideFile(ChildHideFile childHideFile)
        {
            dataRepository.InsertChildHideFile(childHideFile);
        }

        public void DeleteChildHideFile(ChildHideFile childHideFile)
        {
            dataRepository.DeleteChildHideFile(childHideFile);
        }

    }
}
