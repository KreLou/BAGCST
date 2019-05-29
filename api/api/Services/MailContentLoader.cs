using api.Models;
using BAGCST.api.User.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace api.Services
{
    public class MailContentLoader
    {
        private SessionItem session;
        private UserItem user;
        private string filepath = Path.Combine(Environment.CurrentDirectory, "email-templates", "registration.html");

        public MailContentLoader()
        {

        }

        public string getFormatedMessage(SessionItem session, UserItem user)
        {
            this.user = user;
            this.session = session;
            string message = loadMessage();

            message = formatMessage(message);

            return message;
        }

        private string formatMessage(string message)
        {
            Dictionary<string, string> replacements = loadDictionary();


            foreach(KeyValuePair<string, string> kvp in replacements)
            {
                message = replacePlaceholderByValue(message, kvp.Key, kvp.Value);
            }
            return message;
        }

        private Dictionary<string, string> loadDictionary()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            dict.Add("firstname", user.Firstname);
            dict.Add("lastname", user.Lastname);
            dict.Add("activationcode", session.ActivationCode);

            return dict;
        }

        private string replacePlaceholderByValue(string message, string placeholder, string value)
        {
            return message.Replace("{{" + placeholder + "}}", value);
        }

        private string loadMessage()
        {
            using (StreamReader sr = new StreamReader(filepath))
            {
                string message = "";

                string line;
                while((line = sr.ReadLine()) != null)
                {
                    message += line + "\n";
                }
                return message;
            }
        }
    }
}
