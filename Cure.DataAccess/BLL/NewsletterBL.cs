namespace Cure.DataAccess.BLL
{
    using System;
    using Cure.DataAccess.DAL;
    using System.Collections.Generic;

    public partial class DataAccessBL
    {
        public IEnumerable<Newsletter> GetNewsletters()
        {
            return dataRepository.GetNewsletters();
        }

        public void InsertNewsletter(Newsletter newsletter)
        {
            try
            {
                dataRepository.InsertNewsletter(newsletter);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteNewsletter(Newsletter newsletter)
        {
            try
            {
                dataRepository.DeleteNewsletter(newsletter);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateNewsletter(Newsletter newsletter)
        {
            try
            {
                dataRepository.UpdateNewsletter(newsletter);
            } catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
