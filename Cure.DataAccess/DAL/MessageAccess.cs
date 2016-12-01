using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public IEnumerable<ViewRecipient> GetContacts(string username, string newContact)
        {
            return context.sp_GetMyContacts(username, newContact);
        }

        public int GetUnreadCount(string username)
        {
            int count = context.Messages.Count(x => x.ToUserName == username && x.Unread == true);
            return count;
        }

        public IEnumerable<Message> GetMyMessages(string username, string contact)
        {
            var messages = context.Messages.Where(x => (x.FromUserName == username && x.ToUserName == contact) || (x.ToUserName == username && x.FromUserName == contact)).OrderBy(x => x.SendTime);
            for (int i = messages.Count(x => x.Unread == true && x.ToUserName == username) - 1; i > -1; i--)
            {
                var msg = messages.Where(x => x.Unread == true && x.ToUserName == username).ToList()[i];
                msg.Unread = false;
                UpdateMessage(msg);
            }
            return messages;
        }

        public IEnumerable<Message> GetMessages()
        {
            return context.Messages.OrderByDescending(x => x.SendTime);
        }

        public Message GetMessage(int messageId)
        {
            return context.Messages.FirstOrDefault(o => o.Id == messageId);
        }

        public void InsertMessage(Message message)
        {
            try
            {
                context.Messages.AddObject(message);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteMessage(Message message)
        {
            try
            {
                context.Messages.Attach(message);
                context.Messages.DeleteObject(message);
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateMessage(Message message)
        {
            try
            {
                var origMessage = GetMessage(message.Id);
                origMessage.FromDisplay = message.FromDisplay;
                origMessage.FromUserName = message.FromUserName;
                origMessage.SendTime = message.SendTime;
                origMessage.Subject = message.Subject;
                origMessage.Text = message.Text;
                origMessage.Unread = message.Unread;
                origMessage.ToDisplay = message.ToDisplay;
                origMessage.ToUserName = message.ToUserName;
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
