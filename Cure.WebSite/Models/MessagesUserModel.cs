using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cure.WebSite.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Configuration;
    using System.IO;
    using DataAccess;
    using Utils;

    public class MessagesUserModel
    {
        public Guid UserId { get; set; }
        public Guid ChildId { get; set; }
        public string Username { get; set; }
        public string ContactName { get; set; }
        public string UserDisplay { get; set; }
        public string OnlineModifier { get; set; }
        public string OnlineModifierRu { get; set; }
        public bool IsAdmin { get; set; }
        public string ChildAvaUrl { get; set; }
        public string UserpicUrl { get; set; }
        public string ChildName { get; set; }
        public string LastMessageText { get; set; }
        public DateTime LastMessageDate { get; set; }

        public MessagesUserModel(ViewRecipient view, bool isOnline, bool isAdmin, string lastMessageText, DateTime lastMessageDate)
        {
            this.UserId = view.UserId;
            this.ChildId = view.GuidId ?? Guid.NewGuid();
            this.Username = view.UserName;
            this.ContactName = view.ContactName;
            this.OnlineModifier = isOnline ? "is-online" : string.Empty;
            this.OnlineModifierRu = this.OnlineModifier.Replace("is-online", "в сети");
            this.ChildName = view.ChildName;
            this.IsAdmin = isAdmin;
            this.UserDisplay = this.IsAdmin 
                ? string.Format("{0} (Администратор)", this.ContactName) :
                (string.IsNullOrEmpty(this.ChildName) ? this.ContactName : string.Format("{0} ({1})", this.ContactName, this.ChildName));
            this.LastMessageText = string.IsNullOrEmpty(lastMessageText) ? "Пока нет сообщений" : lastMessageText;
            this.LastMessageDate = lastMessageDate.Year < 2016 ? DateTime.Now : lastMessageDate;
            this.UserpicUrl =
                SiteUtils.GetUserUserpic(
                    Path.Combine(ConfigurationManager.AppSettings["PhotoUrl"], view.GuidId.ToString())
                        .Replace("Upload", "Userpics"),
                    view.GuidId,
                    view.OwnerUserPic);

            string avaFile = string.IsNullOrEmpty(view.FileName)
                    ? string.Empty
                    : view.FileName;
            if (!string.IsNullOrEmpty(avaFile))
            {
                this.ChildAvaUrl = Path.Combine(Path.Combine(Path.Combine(ConfigurationManager.AppSettings["PhotoUrl"], view.GuidId.ToString()), "Thumb"), avaFile);
            }
            else
            {
                this.ChildAvaUrl = string.Format("/Content/img/userpics/no_photo_{0}.jpg",
                    SiteUtils.GetRandom(view.ChildId ?? 0, 3));
            }

        }
    }
}