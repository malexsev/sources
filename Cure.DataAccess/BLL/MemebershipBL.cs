namespace Cure.DataAccess.BLL
{
    using System.Collections.Generic;

    public partial class DataAccessBL
    {
        public ViewUserMembership GetUserMembership(string username)
        {
            return dataRepository.GetUserMembership(username);
        }

        public IEnumerable<ViewUserMembership> ViewUserMembership()
        {
            return dataRepository.ViewUserMembership();
        }

        public void UpdateUserMembership(ViewUserMembership userMembership)
        {
            dataRepository.UpdateUserMembership(userMembership);
        }
    }
}
