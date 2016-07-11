namespace Cure.DataAccess.BLL
{
    using System;
    using System.Collections.Generic;

    public partial class DataAccessBL
    {
        public ChildAvaFile GetChildAvaFile(int childId)
        {
            return dataRepository.GetChildAvaFile(childId);
        }

        public void InsertChildAvaFile(ChildAvaFile childAvaFile)
        {
            dataRepository.InsertChildAvaFile(childAvaFile);
        }

        public void DeleteChildAvaFile(ChildAvaFile childAvaFile)
        {
            dataRepository.DeleteChildAvaFile(childAvaFile);
        }

    }
}
