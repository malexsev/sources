namespace Cure.DataAccess.BLL
{
    using System;
    using Cure.DataAccess.DAL;
    using System.Collections.Generic;

    public partial class DataAccessBL
    {
        public IEnumerable<Message> GetMyMessages(string username)
        {
            return dataRepository.GetMyMessages(username);
        }

        public IEnumerable<Message> GetMessages()
        {
            return dataRepository.GetMessages();
        }

        public Message GetMessage(int messageId)
        {
            return dataRepository.GetMessage(messageId);
        }

        public void InsertMessage(Message message)
        {
            try
            {
                dataRepository.InsertMessage(message);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteMessage(Message message)
        {
            try
            {
                dataRepository.DeleteMessage(message);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateMessage(Message message)
        {
            try
            {
                dataRepository.UpdateMessage(message);
            } catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
