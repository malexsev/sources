using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public IEnumerable<Message> GetMyMessages(string username)
        {
            return context.Messages.Where(x => x.FromUserName == username || x.ToUserName == username).OrderByDescending(x => x.SendTime);
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
            } catch (Exception ex)
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
            } catch (Exception ex)
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
                origMessage.ToDisplay = message.ToDisplay;
                origMessage.ToUserName = message.ToUserName;
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
