﻿using System.Net.Mail;
using System.Net;

namespace MovieTicketBooking
{
    public class SendMailSMTP
    {
        public static void SendMail(string to, string sub, string msg)
        {
            var fromMail = "hieundhe170151@fpt.edu.vn";
            var fromPassword = "hnqm fezm jtkl qrmp";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = sub;
            message.To.Add(new MailAddress(to));
            message.Body = msg;
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };
            smtpClient.Send(message);
        }
    }
}
