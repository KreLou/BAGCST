using api.Models;
using api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace api.Handler
{
    //TODO Setup as global Scoped
    public class SendMailService
    {
        public string Receiver { get; private set; }
        public string Sender { get; private set; }

        private UserItem user;
        private ServerConfig configData;

        private readonly MailContentLoader _mailContentLoader;

        public SendMailService(MailContentLoader contentLoader)
        {
            this._mailContentLoader = contentLoader;
            this.configData = ServerConfigHandler.ServerConfig;
            this.Sender = configData.SMTP_SendAs;
        }

        public void sendMail(string subject, string body)
        {
            MailMessage message = new MailMessage(Sender, Receiver);
            message.IsBodyHtml = true;
            message.Subject = subject;
            message.Body = body;

            SmtpClient client = new SmtpClient(configData.SMTP_Host, configData.SMTP_Port);
            if (configData.SMTP_UseCurrentUser)
            {
                client.UseDefaultCredentials = true;
            }
            else
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(configData.SMTP_User, configData.SMTP_Password);
            }
            try
            {
                client.Send(message);
            }catch(System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void sendRegistrationMail(SessionItem session)
        {
            string message = _mailContentLoader.getFormatedMessage(session, user);
            string subject = "Registrierung BA-Glauchau-APP";
            sendMail(subject, message);
        }

        internal void setUser(UserItem user)
        {
            this.user = user;
            this.user = user;
            this.Receiver = user.Email;
        }
    }
}
