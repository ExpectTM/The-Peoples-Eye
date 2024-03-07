using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ForekBase.Application.Common.Utility
{
    public static class SD
    {
        private static string _username = $"apprentice@forek.co.za";
        private static string _password = "P@55w0rd2022";

        static Random rand = new Random();
        public const string Alphabet =
       "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static string RandomStringGenerator(int size)
        {
            char[] chars = new char[size];

            for (int i = 0; i < size; i++)
            {
                chars[i] = Alphabet[rand.Next(Alphabet.Length)];
            }

            return new string(chars);

        }

        public static void OnSendMailNotification(string reciever, string subject, string message, string header)
        {
            var senderMail = new MailAddress(_username, $"The People's Eye");

            var recieverMail = new MailAddress(reciever, header);

            var password = _password;

            var sub = subject;

            var body = message;

            var smtp = new SmtpClient
            {
                Host = "smtp.forek.co.za",

                Port = 587,

                EnableSsl = true,

                DeliveryMethod = SmtpDeliveryMethod.Network,

                UseDefaultCredentials = false,

                Credentials = new NetworkCredential(senderMail.Address, password)
            };

            using (var mess = new MailMessage(senderMail, recieverMail)
            {
                Subject = subject,

                Body = body,

                IsBodyHtml = true,

            })

            {
                //mess.Attachments.Add(new Attachment("C:\\file.zip"));

                smtp.Send(mess);
            }
        }
        public static string GetDisplayName(this System.Enum enumValue)
        {
            var displayAttribute = enumValue.GetType()

                .GetMember(enumValue.ToString())

                .FirstOrDefault()

                ?.GetCustomAttribute<DisplayAttribute>();

            return displayAttribute?.Name ?? enumValue.ToString();
        }
    }
}
