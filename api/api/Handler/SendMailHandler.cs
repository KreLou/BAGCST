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

        public SendMailHandler(string receiver)
        {
            this.Receiver = receiver;
            this.Sender = "4002314@ba-glauchau.de";
        }

        public void sendMail(string subject, string body)
        {
            MailMessage message = new MailMessage(Sender, Receiver);
            message.Subject = subject;
            message.Body = body;

            SmtpClient client = new SmtpClient("smtp.ba-glauchau.de", 587);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("4002314@ba-glauchau.de", "<password>.");
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
