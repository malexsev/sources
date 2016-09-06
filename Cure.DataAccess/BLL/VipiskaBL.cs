namespace Cure.DataAccess.BLL
{
    using System;
    using Cure.DataAccess.DAL;
    using System.Collections.Generic;

    public partial class DataAccessBL
    {
        public IEnumerable<Vipiska> GetMyVipiskas(string username)
        {
            return dataRepository.GetMyVipiskas(username);
        }

        public Vipiska GetVipiska(int visitId)
        {
            return dataRepository.GetVipiska(visitId);
        }

        public void InsertVipiska(Vipiska vipiska)
        {
            try
            {
                dataRepository.InsertVipiska(vipiska);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteVipiska(Vipiska vipiska)
        {
            try
            {
                dataRepository.DeleteVipiska(vipiska);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateVipiska(Vipiska vipiska)
        {
            try
            {
                dataRepository.UpdateVipiska(vipiska);
            } catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
