using System;
using System.Web.UI;
using Cure.DataAccess.BLL;
using Cure.Utils;

namespace Cure.WebAdmin.Admin
{
    using System.Linq;
    using System.Web.Security;

    public partial class UserList : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void uxMainGrid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            var username = e.Keys[0].ToString();

            var dal = new DataAccessBL();

            try
            {
                switch (dal.CheckDeleteMembership(username))
                {
                    case 1:
                    {
                        throw new Exception("Пользователь имеет ссылки на страницу Наши Дети, удаление невозможно при наличии ссылки.");
                        break;
                    }
                    case 2:
                    {
                        throw new Exception("Пользователь имеет ссылки на записи в лентах впечатлений, удаление невозможно при наличии ссылок.");
                        break;
                    }
                    case 3:
                    {
                        throw new Exception("Пользователь имеет ссылки на сообщения, удаление невозможно при наличии ссылок.");
                        break;
                    }
                    case 4:
                    {
                        throw new Exception("Пользователь имеет ссылки на заявки, удаление невозможно при наличии заявок.");
                        break;
                    } 
                    case 5:
                    {
                        throw new Exception("Пользователь имеет посты в лентах впечатлений, удаление невозможно при наличии постов.");
                        break;
                    }
                    default:
                    {
                        Membership.DeleteUser(username);
                        break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                e.Cancel = true;
            }
        }
    }
}