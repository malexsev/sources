namespace Cure.Notification
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Security;
    using DataAccess;
    using DataAccess.BLL;
    using DevExpress.XtraPrinting.Native;

    public class RemoveUnapprovedUsers10DaysLeft : BaseNotification
    {
        private IEnumerable<ViewUserMembership> bots;
        private int removeCount = 0;

        public RemoveUnapprovedUsers10DaysLeft(HttpServerUtilityBase server)
            : base(server)
        {
            var dal = new DataAccessBL();
            this.bots = dal.GetUnapprovedMemberships();
        }

        public override bool Send()
        {
            bool result = false;

            if (this.bots.Any())
            {
                string log = string.Empty;
                removeCount = this.bots.Count();
                foreach (var bot in this.bots.ToArray())
                {
                    if (bot != null && Membership.DeleteUser(bot.UserName))
                    {
                        log += bot.LoweredEmail + ",";
                    }
                    else
                    {
                        var userName = "<не удалось определить пользователя>";
                        if (bot != null)
                        {
                            userName = bot.LoweredUserName;
                        }
                        this.Log("Ошибка удаления: " + userName, "Ошибка удаления");
                    }
                }
                this.Log(log, "Удалено");
                result = true;
            }


            return result;
        }

        private void Log(string botsList, string result)
        {
            var notify = new NotificationLog()
            {
                ClientName = "Администрация",
                Description = "Удалено " + this.removeCount,
                Contacts = string.Empty,
                Details = "",
                ExecutionDate = DateTime.Now,
                Name = "Удаление ботов",
                Result = result,
                Type = "Delete",
                Text = null
            };

            SaveLog(notify);
        }
    }
}
