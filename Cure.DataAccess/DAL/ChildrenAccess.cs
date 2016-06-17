using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public ViewChild ViewChild(int id)
        {
            return context.ViewChilds.FirstOrDefault(x => x.Id == id);
        }

        public int CountChilds(int countryId, string regionName, int ageOption, int diagnozeId)
        {
            int startYears = 0;
            int endYears = 0;

            switch (ageOption)
            {
                case 1:
                    startYears = 0;
                    endYears = 3;
                    break;
                case 2:
                    startYears = 3;
                    endYears = 5;
                    break;
                case 3:
                    startYears = 5;
                    endYears = 8;
                    break;
                case 4:
                    startYears = 8;
                    endYears = 12;
                    break;
                case 5:
                    startYears = 12;
                    endYears = 200;
                    break;
            }

            var takeRecords = 1;
            DateTime startDate = DateTime.Today.AddYears(-endYears);
            DateTime endDate = DateTime.Today.AddYears(-startYears);

            return context.ViewChilds.Where(x => (countryId == 0 || x.CountryId == countryId)
                && (regionName == "0" || x.Region == regionName)
                && (ageOption == 0 || (x.Birthday > startDate && x.Birthday < endDate))
                && (diagnozeId == 0 || x.DiagnozId == diagnozeId)).OrderByDescending(x => x.Id).Count();
        }

        public IEnumerable<ViewChild> FilterChilds(int countryId, string regionName, int ageOption, int diagnozeId, int skipRecords)
        {
            int startYears = 0;
            int endYears = 0;

            switch (ageOption)
            {
                case 1:
                    startYears = 0;
                    endYears = 3;
                    break;
                case 2:
                    startYears = 3;
                    endYears = 5;
                    break;
                case 3:
                    startYears = 5;
                    endYears = 8;
                    break;
                case 4:
                    startYears = 8;
                    endYears = 12;
                    break;
                case 5:
                    startYears = 12;
                    endYears = 200;
                    break;
            }

            var takeRecords = 12;
            DateTime startDate = DateTime.Today.AddYears(-endYears);
            DateTime endDate = DateTime.Today.AddYears(-startYears);

            return context.ViewChilds.Where(x => (countryId == 0 || x.CountryId == countryId)
                && (regionName == "0" || x.Region == regionName)
                && (ageOption == 0 || (x.Birthday > startDate && x.Birthday < endDate))
                && (diagnozeId == 0 || x.DiagnozId == diagnozeId)).OrderByDescending(x => x.Id).Skip(skipRecords).Take(takeRecords).ToList();
        }

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
                origChild.FinCardName = child.FinCardName;
                origChild.FinCountryId = child.FinCountryId;
                origChild.FinKiwi = child.FinKiwi;
                origChild.FinOperatorId = child.FinOperatorId;
                origChild.FinPhoneNumber = child.FinPhoneNumber;
                origChild.FinWebmoney = child.FinWebmoney;
                origChild.FinWebmoney2 = child.FinWebmoney2;
                origChild.FinWebmoney3 = child.FinWebmoney3;
                origChild.FinYandexMoney = child.FinYandexMoney;
                origChild.IsActive = child.IsActive;
                origChild.Name = child.Name;
                origChild.OwnerUser = child.OwnerUser;
                origChild.Region = child.Region;
                origChild.SocialFb = child.SocialFb;
                origChild.SocialMm = child.SocialMm;
                origChild.SocialOk = child.SocialOk;
                origChild.SocialVk = child.SocialVk;
                origChild.SocialYoutube = child.SocialYoutube;
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
