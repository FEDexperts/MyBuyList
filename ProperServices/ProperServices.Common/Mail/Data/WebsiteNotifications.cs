using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Configuration;

namespace ProperServices.Common.Mail.Data
{
    public class WebsiteNotifications : ConfigurationSection
    {
        [ConfigurationProperty("WebsiteStarted", IsRequired = true)]
        public Message WebsiteStarted { get { return (Message)base["WebsiteStarted"]; } }

        [ConfigurationProperty("WebsiteStopped", IsRequired = true)]
        public Message WebsiteStopped { get { return (Message)base["WebsiteStopped"]; } }

        [ConfigurationProperty("WebsiteErrors", IsRequired = true)]
        public Message WebsiteErrors { get { return (Message)base["WebsiteErrors"]; } }
        
        [ConfigurationProperty("From", IsRequired = true)]
        public AdminDetails From { get { return (AdminDetails)base["From"]; } }

        [ConfigurationProperty("Recipients", IsRequired = true)]
        public Admins Recipients { get { return (Admins)base["Recipients"]; } }
    }

    public class Message : ConfigurationElement
    {
        [ConfigurationProperty("Enabled", IsRequired = true)]
        public bool Enabled { get { return (bool)base["Enabled"]; } }

        [ConfigurationProperty("Subject", IsRequired = true)]
        public string Subject { get { return (string)base["Subject"]; } }

        [ConfigurationProperty("Body", IsRequired = true)]
        public Body Body { get { return (Body)base["Body"]; } }

        [ConfigurationProperty("SMSNotification", IsRequired = true)]
        public bool SMSNotification { get { return (bool)base["SMSNotification"]; } }
    }

    public class Body : ConfigurationElement
    {
        [ConfigurationProperty("IsHtml", DefaultValue = true, IsRequired = false)]
        public bool IsHtml { get { return (bool)base["IsHtml"]; } }

        [ConfigurationProperty("Text", IsRequired = true)]
        public string Text { get { return (string)base["Text"]; } }
    }

    [ConfigurationCollection(typeof(AdminDetails), AddItemName = "Recipient")]
    public class Admins : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new AdminDetails();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AdminDetails)element).Email;
        }

        public string[] GetReceipients()
        {
            List<string> list = new List<string>();
            foreach (AdminDetails details in this)
            {
                list.Add(details.Email);
            }

            return list.ToArray();
        }
    }

    public class AdminDetails : ConfigurationElement
    {
        [ConfigurationProperty("Name", IsRequired = true)]
        public string Name { get { return (string)base["Name"]; } }

        [ConfigurationProperty("Email", IsRequired = true)]
        public string Email { get { return (string)base["Email"]; } }

        [ConfigurationProperty("Phone", IsRequired = false)]
        public string Phone { get { return (string)base["Phone"]; } }
    }
}
