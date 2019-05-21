using api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace api.Handler
{
    public class ServerConfigHandler
    {
        public static ServerConfig ServerConfig { get; private set; }

        private static string pathToConfigFile = Path.Combine(Environment.CurrentDirectory, "serverConfig.txt");


        private static ServerConfig getDefaultConfig()
        {
            return new ServerConfig
            {
                APIURl = "http://localhost:5900",
                SQLConnectionString = "<sql-connection-string>",
                SMTP_Host = "smtp.ba-glauchau.de",
                SMTP_Port = 587,
                SMTP_UseCurrentUser = false,
                SMTP_User = "smtp_user",
                SMTP_Password = "<password>",
                SMTP_SendAs = "noreply@app.ba-glauchau.de",
                Default_SessionUseTimeInMonth = 10,
                JWT_Audience = "APP-BA-Glauchau",
                JWT_Issuer = "APP-BA-Glauchau",
                JWT_SecurityKey = "<hier muss ein geheimer Code zur Verschlüsselung der JWT-Token stehen>"
            };
        }

        public static void loadConfig()
        {
            if (!File.Exists(pathToConfigFile))
            {
                createConfig();
            }


            ServerConfig defaultConfig = getDefaultConfig();
            ServerConfig loadConfig = getConfigFromFile();

            FieldInfo[] defaultFields = getFieldsOfConfig(defaultConfig);
            FieldInfo[] loadFields = getFieldsOfConfig(loadConfig);

            foreach(FieldInfo defaultFieldInfo in defaultFields)
            {
                object value = defaultFieldInfo.GetValue(defaultConfig);
                string name = defaultFieldInfo.Name;

                object foundedField = getFieldValueFromObject(name, loadFields, loadConfig);

                if (isNullOrEmpty(foundedField))
                {
                    saveDefaultFieldValue(name, getFieldValueFromObject(name, defaultFields, defaultConfig));

                    loadConfig = getConfigFromFile();
                    loadFields = getFieldsOfConfig(loadConfig);
                }
            }
            ServerConfig = loadConfig;
        }

        private static void saveDefaultFieldValue(string name, object foundedField)
        {
            name = extractFieldName(name);
            File.AppendAllLines(pathToConfigFile, new string[] { $"{name}={foundedField.ToString()}" });
        }

        private static bool isNullOrEmpty(object foundedField)
        {
            try
            {
                return Equals(foundedField, null) || ((int)foundedField == 0);
            }
            catch (InvalidCastException) { }
            return Equals(foundedField, null);
        }

        private static object getFieldValueFromObject(string name, FieldInfo[] loadFields, ServerConfig loadConfig)
        {
            foreach(FieldInfo info in loadFields)
            {
                if (info.Name == name)
                {
                    return info.GetValue(loadConfig);
                }
            }
            return null;
        }

        private static ServerConfig getConfigFromFile()
        {
            ServerConfig config = new ServerConfig();

            KeyValuePair<string, object>[] inputs = getKeyValueInputsFromFile();

            config = assignAllValuesToConfig(inputs, config);


            return config;

        }

        private static ServerConfig assignAllValuesToConfig(KeyValuePair<string, object>[] inputs, ServerConfig config)
        {
            FieldInfo[] fielInfos = getFieldsOfConfig(config);

            foreach(FieldInfo info in fielInfos)
            {
                string name = extractFieldName(info.Name);

                object input = getObjectFromInput(inputs, name);

                if (input != null)
                {
                    try
                    {
                        input = Convert.ChangeType(input, info.FieldType);
                    }catch(FormatException fe)
                    {
                        Console.WriteLine("Could not Convert input for Field: " + name + ": " + fe.Message);
                    }
                    info.SetValue(config, input);
                }
            }
            return config;
        }

        private static object getObjectFromInput(KeyValuePair<string, object>[] inputs, string name)
        {
            foreach(KeyValuePair<string, object> kvp in inputs)
            {
                if (kvp.Key.ToLower() == name.ToLower())
                {
                    return kvp.Value;
                }
            }
            return null;
        }

        private static string extractFieldName(string name)
        {
            name = name.Split('>')[0];
            name = name.Split('<')[1];
            return name;
        }

        private static FieldInfo[] getFieldsOfConfig(ServerConfig config)
        {
            var bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

            return config.GetType().GetFields(bindingFlags);
        }

        private static KeyValuePair<string, object>[] getKeyValueInputsFromFile()
        {
            List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>();

            using (StreamReader sr = new StreamReader(pathToConfigFile))
            {
                string currentLine;
                while((currentLine = sr.ReadLine()) != null)
                {
                    if (currentLine.Contains("="))
                    {
                        string key = currentLine.Split("=")[0];
                        string value = currentLine.Split("=")[1];
                        list.Add(new KeyValuePair<string, object>(key, value));
                    }
                }
            }
            return list.ToArray();
        }

        private static void createConfig()
        {
            using (StreamWriter sw = new StreamWriter(pathToConfigFile))
            {
                sw.WriteLine("Config File for APP-Backend by WI16");
            }
        }
    }
}
