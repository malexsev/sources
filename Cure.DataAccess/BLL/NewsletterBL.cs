namespace Cure.DataAccess.BLL
{
    using System;
    using Cure.DataAccess.DAL;
    using System.Collections.Generic;

    public partial class DataAccessBL
    {
        public IEnumerable<ViewSubscriber> ViewSubscribers()
        {
            return dataRepository.ViewSubscribers();
        }

        public bool Subscribe(string email)
        {
            if (!ValidateEmail(email))
                return false;

            var subscription = dataRepository.GetNewsletter(email);
            if (subscription == null)
            {
                subscription = new Newsletter()
                {
                    Email = email,
                    EntryDate = DateTime.Now,
                    ErrorsCount = 0,
                    SuccessCount = 0,
                    Settings = string.Empty,
                    EntryType = "Управление подпиской"
                };
                dataRepository.InsertNewsletter(subscription);
            }
            return true;
        }

        public bool UnSubscribe(string email)
        {
            if (!ValidateEmail(email))
                return false;

            var subscription = dataRepository.GetNewsletter(email);
            if (subscription == null)
                return false;

            dataRepository.DeleteNewsletter(subscription);
            return true;
        }

        public Newsletter GetNewsletter(string email)
        {
            return dataRepository.GetNewsletter(email);
        }

        public IEnumerable<Newsletter> GetNewsletters()
        {
            return dataRepository.GetNewsletters();
        }

        public void InsertNewsletter(Newsletter newsletter)
        {
            try
            {
                dataRepository.InsertNewsletter(newsletter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteNewsletter(Newsletter newsletter)
        {
            try
            {
                dataRepository.DeleteNewsletter(newsletter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateNewsletter(Newsletter newsletter)
        {
            try
            {
                dataRepository.UpdateNewsletter(newsletter);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private bool ValidateEmail(string email)
        {
            return email.Contains("@") && email.Contains(".");
        }

    }
}
