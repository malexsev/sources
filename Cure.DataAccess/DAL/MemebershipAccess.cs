using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public int CheckDeleteMembership(string username)
        {
            var disallow = context.spUserDeleteCheck(username).FirstOrDefault();

            if (disallow == null)
            {
                return 0;
            }
            return disallow.Value;
        }

        public ViewUserMembership GetUserMembership(Guid userId)
        {
            return context.ViewUserMemberships.FirstOrDefault(x => x.Expr1 == userId);
        }

        public ViewUserMembership GetUserMembership(string username)
        {
            return context.ViewUserMemberships.FirstOrDefault(x => x.UserName.ToLower() == username.ToLower());
        }

        public IEnumerable<ViewUserMembership> ViewUserMembership()
        {
            return context.ViewUserMemberships.OrderByDescending(x => x.CreateDate);
        }

        public void UpdateUserMembership(ViewUserMembership userMembership)
        {
            try
            {
                aspnet_Users origUser = context.aspnet_Users.FirstOrDefault(x => x.UserName.ToLower() == userMembership.UserName.ToLower());
                aspnet_Membership origMemeber = context.aspnet_Membership.FirstOrDefault(x => x.UserId == origUser.UserId);

                if (origMemeber != null)
                {
                    origMemeber.LoweredEmail = userMembership.Email.ToLower();
                    origMemeber.Email = userMembership.Email;
                    origMemeber.IsLockedOut = userMembership.IsLockedOut;
                    origMemeber.Comment = userMembership.Comment;

                    SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
