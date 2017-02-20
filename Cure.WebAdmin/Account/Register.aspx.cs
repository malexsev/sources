namespace Cure.WebAdmin
{
    using System;
    using System.Net.Mail;
    using System.Web;
    using Notification;
    using System.Web.Security;

    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCreateUser_Click(object sender, EventArgs e)
        {
            try
            {
                MembershipUser user = Membership.CreateUser(tbUserName.Text, tbPassword.Text, tbEmail.Text);
                var notification = new UserRegisteredEmailNotification(tbUserName.Text, new HttpServerUtilityWrapper(Server));
                notification.Send();
                var notificationToUser = new RegistrationToUserEmailNotification(tbUserName.Text, tbPassword.Text, new HttpServerUtilityWrapper(Server));
                notificationToUser.Send();
                Response.Redirect(Request.QueryString["ReturnUrl"] ?? "~/Account/RegisterSuccess.aspx");
            } catch (MembershipCreateUserException exc)
            {
                if (exc.StatusCode == MembershipCreateStatus.DuplicateEmail || exc.StatusCode == MembershipCreateStatus.InvalidEmail)
                {
                    tbEmail.ErrorText = "ƒанный адрес электронной почты уже используетс€ в системе";
                    tbEmail.IsValid = false;
                } else if (exc.StatusCode == MembershipCreateStatus.InvalidPassword)
                {
                    tbPassword.ErrorText = "Ќеверный пароль. ѕароль и подтверждение не соответствуют или пароль слишком простой";
                    tbPassword.IsValid = false;
                } else
                {
                    tbUserName.ErrorText = exc.Message == "The username is already in use." ? "ƒанный пользователь уже зарегистрирован" : exc.Message;
                    tbUserName.IsValid = false;
                }
            }
        }
    }
}