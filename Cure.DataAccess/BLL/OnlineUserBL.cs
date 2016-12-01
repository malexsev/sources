namespace Cure.DataAccess.BLL
{
    using System;
    using Cure.DataAccess.DAL;
    using System.Collections.Generic;

    public partial class DataAccessBL
    {
        public void BringOnlineUser(string username)
        {
            dataRepository.BringOnlineUser(username);
        }

        public void ClearOnlineUsers()
        {
            dataRepository.ClearOnlineUsers();
        }

        public IEnumerable<OnlineUser> GetOnlineUsers()
        {
            return dataRepository.GetOnlineUsers();
        }

        public void InsertOnlineUser(OnlineUser smsLog)
        {
            try
            {
                dataRepository.InsertOnlineUser(smsLog);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteOnlineUser(OnlineUser smsLog)
        {
            try
            {
                dataRepository.DeleteOnlineUser(smsLog);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateOnlineUser(OnlineUser smsLog)
        {
            try
            {
                dataRepository.UpdateOnlineUser(smsLog);
            } catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
