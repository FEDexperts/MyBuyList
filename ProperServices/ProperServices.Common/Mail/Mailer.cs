using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using ProperServices.Common.Mail.Data;

namespace ProperServices.Common.Mail
{
    public class Mailer
    {
        #region SendMail static methods
        public static void SendMail(string to, string from, string subject, string body, bool isBodyHtml)
        {
            Mailer.SendMail(new string[] { to }, from, subject, body, isBodyHtml);
        }

        public static void SendMail(string[] to, string from, string subject, string body, bool isBodyHtml)
        {
            #region Argument validation
            if (to == null || to.Length == 0)
                throw new ArgumentNullException("to", "Must have at least one recipient");

            if (string.IsNullOrEmpty(from))
                throw new ArgumentNullException("from");

            if (string.IsNullOrEmpty(subject))
                throw new ArgumentNullException("subject");

            if (string.IsNullOrEmpty(body))
                throw new ArgumentNullException("body");
            #endregion

            //create and send the mail message
            using (MailMessage mail = new MailMessage())
            {
                //set the addresses
                mail.From = new MailAddress(from);
                foreach (string recipient in to)
                {
                    if (string.IsNullOrEmpty(recipient))
                        throw new ArgumentNullException("to", "one of the recipients is missing");

                    mail.To.Add(recipient);
                }

                //set the content
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = isBodyHtml;

                //send the message
                SmtpClient smtp = new SmtpClient();
                smtp.EnableSsl = true;
                
                //smtp.Send(mail);
            }
        } 
        #endregion
    }
}
