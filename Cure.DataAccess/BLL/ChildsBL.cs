namespace Cure.DataAccess.BLL
{
    using System;
    using System.Collections.Generic;

    public partial class DataAccessBL
    {
        public IEnumerable<Child> GetChilds()
        {
            return dataRepository.GetChilds();
        }

        public IEnumerable<ViewChild> ViewChilds()
        {
            return dataRepository.ViewChilds();
        }

        public IEnumerable<Child> GetChilds(int countryId)
        {
            return dataRepository.GetChilds(countryId);
        }

        public void InsertChild(Child child)
        {
            try
            {
                dataRepository.InsertChild(child);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteChild(Child child)
        {
            try
            {
                dataRepository.DeleteChild(child);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateChild(Child child)
        {
            try
            {
                dataRepository.UpdateChild(child);
            } catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
