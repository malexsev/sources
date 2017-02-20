namespace Cure.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Net.Mime;
    using System.Text;

    public static class EmailUtils
    {
        public static bool SendEmail(string toEmails, string copyEmails, string subject, string body, string reason, 
            string attachmentPath = "", string attachmentName = "")
        {
            bool mailSent;
            var cred = new NetworkCredential("u436844", "22231e6b7zx");
            var client = new SmtpClient
            {
                UseDefaultCredentials = false,
                Credentials = cred,
                Port = 465,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = false,
                Host = "smtp-18.1gb.ru",
            };

            var message = new MailMessage("noreply@dcp-china.ru", toEmails, subject, body)
            {
                BodyEncoding = Encoding.UTF8,
                SubjectEncoding = Encoding.UTF8,
                IsBodyHtml = true,
                Priority = MailPriority.Normal,
            };

            if (!string.IsNullOrEmpty(attachmentPath) && !string.IsNullOrEmpty(attachmentName))
            {
                var contentType = new ContentType
                {
                    MediaType = System.Net.Mime.MediaTypeNames.Application.Octet,
                    Name = attachmentName
                };
                message.Attachments.Add(new Attachment(attachmentPath, contentType));
            }
            
            foreach (string copy in copyEmails.Split(Convert.ToChar(",")))
            {
                if (!string.IsNullOrEmpty(copy))
                {
                    message.CC.Add(new MailAddress(copy));
                }
            }

            //if (linkedResources != null && linkedResources.Any())
            //{
            //var images = new List<System.Net.Mail.LinkedResource>();
            //var htmlView = AlternateView.CreateAlternateViewFromString(body, Encoding.UTF8, MediaTypeNames.Text.Html);
            //images.ForEach(htmlView.LinkedResources.Add);
            //message.AlternateViews.Add(htmlView);
            //}

            try
            {
                client.Send(message);
                mailSent = true;
            }
            catch (Exception ex)
            {
                mailSent = false;
            }
            finally
            {
                message.Dispose();
            }

            return mailSent;
        }
    }
}
