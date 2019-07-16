using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;


namespace SoltaniWeb.Models.utility
{
    public static class Email
    {
        public static bool send(Emailparameter mail)
        {
            try
            {
                MailMessage mymessage = new MailMessage();
                mymessage.To.Add(mail.to);
                mymessage.From = new MailAddress(mail.from);
                mymessage.Subject = mail.subject;
                mymessage.Priority = MailPriority.High;
                mymessage.Body = mail.text;
                mymessage.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = mail.smtp;
                smtp.Port = 25;
                smtp.EnableSsl = true;

                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(mail.from, mail.password);
                smtp.Send(mymessage);
                return true;



            }
            catch (Exception e)
            {

                return false;

            }

        }
    }



    public class Emailparameter
    {
        public string to { get; set; }
        public string from { get; set; }
        public string smtp { get; set; }
        public string password { get; set; }
        public string subject { get; set; }
        public string text { get; set; }



    }
}