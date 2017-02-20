using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public IEnumerable<Newsletter> GetNewsletters()
        {
            return context.Newsletters.OrderByDescending(o => o.EntryDate).ToList();
        }

        public Newsletter GetNewsletter(int id)
        {
            return context.Newsletters.FirstOrDefault(o => o.Id == id);
        }

        public void InsertNewsletter(Newsletter newsletter)
        {
            try
            {
                context.Newsletters.AddObject(newsletter);
                context.SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteNewsletter(Newsletter newsletter)
        {
            try
            {
                context.Newsletters.Attach(newsletter);
                context.Newsletters.DeleteObject(newsletter);
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateNewsletter(Newsletter newsletter)
        {
            try
            {
                var origNewsletter = GetNewsletter(newsletter.Id);
                origNewsletter.Email = newsletter.Email;
                origNewsletter.EntryDate = newsletter.EntryDate;
                origNewsletter.EntryType = newsletter.EntryType;
                origNewsletter.ErrorsCount = newsletter.ErrorsCount;
                origNewsletter.Settings = newsletter.Settings;
                origNewsletter.SuccessCount = newsletter.SuccessCount;
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public Newsletter GetNewsletter(string email)
        {
            return context.Newsletters.FirstOrDefault(o => o.Email == email);
        }

        public IEnumerable<ViewSubscriber> ViewSubscribers()
        {
            return context.ViewSubscribers;
        }
    }
}
