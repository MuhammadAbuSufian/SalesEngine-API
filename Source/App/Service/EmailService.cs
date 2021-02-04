using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using Project.RequestModel;

namespace Project.Service
{
    public class EmailService
    {
        public bool Send(EmailRequestModel request)
        {

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("iiabd.dev@gmail.com", "ODI-HR-App");
            msg.To.Add(request.To);
            msg.Subject = request.Subject;
            msg.Body = request.Body;
            msg.IsBodyHtml = true;
            using (SmtpClient client = new SmtpClient())
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("iiabd.dev@gmail.com", "Index@iia");
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                client.Send(msg);
            }

            return true;
        }
    }
}