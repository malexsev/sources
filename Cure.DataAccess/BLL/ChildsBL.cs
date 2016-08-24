namespace Cure.DataAccess.BLL
{
    using System;
    using System.Collections.Generic;

    public partial class DataAccessBL
    {
        public ViewChild ViewChild(string ownerUser)
        {
            var view = dataRepository.ViewChild(ownerUser);
            var user = dataRepository.GetUserMembership(ownerUser);
            if (view == null && !string.IsNullOrEmpty(ownerUser))
            {
                var child = new Child()
                {
                    Birthday = new DateTime(2111,1,1),
                    GuidId = Guid.NewGuid(),
                    OwnerUser = ownerUser,
                    CountryId = 10,
                    IsActive = false,
                    Name = string.Empty,
                    ContactName = user.UserName,
                    ContactRodstvoId = 10,
                    ContactEmail = user.LoweredEmail,
                    DiagnozId = 1
                };
                dataRepository.InsertChild(child);
            }
            return view ?? dataRepository.ViewChild(ownerUser);
        }

        public ViewChild ViewChild(int id)
        {
            return dataRepository.ViewChild(id);
        }

        public IEnumerable<Child> GetChilds()
        {
            return dataRepository.GetChilds();
        }

        public Child GetChild(int Id)
        {
            return dataRepository.GetChild(Id);
        }

        public int CountChilds(int countryId, string regionName, int ageOption, int diagnozeId)
        {
            return dataRepository.CountChilds(countryId, regionName, ageOption, diagnozeId);
        }

        public IEnumerable<ViewChild> FilterChilds(int countryId, string regionName, int ageOption, int diagnozeId, int skipRecords)
        {
            return dataRepository.FilterChilds(countryId, regionName, ageOption, diagnozeId, skipRecords);
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
