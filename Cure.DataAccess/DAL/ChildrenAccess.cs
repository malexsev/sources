using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public IEnumerable<ViewChild> ViewChilds()
        {
            return context.ViewChilds.OrderByDescending(o => o.Id).ToList();
        }

        public IEnumerable<Child> GetChilds()
        {
            return context.Children.OrderByDescending(o => o.Id).ToList();
        }

        public IEnumerable<Child> GetChilds(int countryId)
        {
            return context.Children.Where(o => o.CountryId == countryId).OrderByDescending(o => o.Name).ToList();
        }

        public Child GetChild(int id)
        {
            return context.Children.FirstOrDefault(o => o.Id == id);
        }

        public void InsertChild(Child child)
        {
            try
            {
                child.GuidId = Guid.NewGuid();
                context.Children.AddObject(child);
                context.SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteChild(Child child)
        {
            try
            {
                context.Children.Attach(child);
                context.Children.DeleteObject(child);
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateChild(Child child)
        {
            try
            {
                var origChild = GetChild(child.Id);
                origChild.Birthday = child.Birthday;
                origChild.ContactEmail = child.ContactEmail;
                origChild.ContactName = child.ContactName;
                origChild.ContactPhone = child.ContactPhone;
                origChild.ContactRodstvoId = child.ContactRodstvoId;
                origChild.CountryId = child.CountryId;
                origChild.Diagnoz = child.Diagnoz;
                origChild.DiagnozId = child.DiagnozId;
                origChild.FinBankId = child.FinBankId;
                origChild.FinBankOther = child.FinBankOther;
                origChild.FinCardNumber = child.FinCardNumber;
                origChild.FinCountryId = child.FinCountryId;
                origChild.FinKiwi = child.FinKiwi;
                origChild.FinOperatorId = child.FinOperatorId;
                origChild.FinPhoneNumber = child.FinPhoneNumber;
                origChild.FinWebmoney = child.FinWebmoney;
                origChild.FinYandexMoney = child.FinYandexMoney;
                origChild.IsActive = child.IsActive;
                origChild.Name = child.Name;
                origChild.OwnerUser = child.OwnerUser;
                origChild.Region = child.Region;
                origChild.SocialFb = child.SocialFb;
                origChild.SocialMm = child.SocialMm;
                origChild.SocialOk = child.SocialOk;
                origChild.SocialVk = child.SocialVk;
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
