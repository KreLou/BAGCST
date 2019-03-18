using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace api.Handler
{
    public class SendMailHandler
    {
        public string Receiver { get; private set; }
        public string Sender { get; private set; }

        private ServerConfig configData;

        public SendMailHandler(string receiver)
        {
            this.Receiver = receiver;
            this.configData = ServerConfigHandler.ServerConfig;
            this.Sender = configData.SMTP_SendAs;
        }

        public void sendMail(string subject, string body)
        {
            MailMessage message = new MailMessage(Sender, Receiver);
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
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void sendRegistrationMail(SessionItem session)
        {
            string message = $@"
                Herzlich Willkommen zur APP der BA-Glauchau\n

Sie versuchen gerade ein neues Gerät anzumelden.<br>
Um die Registrierung abzuschließen klicken Sie bitte auf den folgenden Links:<br>
<a href=""http://app.ba-glauchau.de/register/{session.ActivationCode}"">Hier</a><br><br>

                ";

            string subject = "Registrierung BA-Glauchau-APP";
            sendMail(subject, message);
        }
    }
}
