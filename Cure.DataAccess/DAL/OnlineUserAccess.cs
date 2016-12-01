using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    using System.Collections;
    using System.Xml.Linq;

    internal partial class DataRepository
    {
        public void BringOnlineUser(string username)
        {

            var onlineUser = context.OnlineUsers.FirstOrDefault(x => x.Username == username);
            if (onlineUser != null)
            {
                onlineUser.LastDate = DateTime.Now;
                onlineUser.Details = "автоматически";
                this.UpdateOnlineUser(onlineUser);
            }
            else
            {
                onlineUser = new OnlineUser() { LastDate = DateTime.Now, Username = username, Details = "автоматически" };
                this.InsertOnlineUser(onlineUser);
            }
        }

        public void ClearOnlineUsers()
        {
            context.sp_ClearOnlineUsers();
        }

        public IEnumerable<OnlineUser> GetOnlineUsers()
        {
            return context.OnlineUsers.OrderByDescending(o => o.LastDate).ToList();
        }

        public OnlineUser GetOnlineUser(int id)
        {
            return context.OnlineUsers.FirstOrDefault(o => o.Id == id);
        }

        public void InsertOnlineUser(OnlineUser onlineUser)
        {
            try
            {
                context.OnlineUsers.AddObject(onlineUser);
                context.SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteOnlineUser(OnlineUser onlineUser)
        {
            try
            {
                context.OnlineUsers.Attach(onlineUser);
                context.OnlineUsers.DeleteObject(onlineUser);
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateOnlineUser(OnlineUser onlineUser)
        {
            try
            {
                var origOnlineUser = GetOnlineUser(onlineUser.Id);
                origOnlineUser.Details = onlineUser.Details;
                origOnlineUser.LastDate = onlineUser.LastDate;
                origOnlineUser.Username = onlineUser.Username;
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
