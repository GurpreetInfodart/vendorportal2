using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorPortal.Models;
//using System.Web.Mail;
using System.Net.Mail;
using VendorPortal.Common;

namespace VendorPortal
{
    public class MailSend
    {
        public bool sendMail(MailModel _objModelMail)
        {
            try
            {

//                MailMessage mail = new MailMessage();
//                mail.To.Add(_objModelMail.To.TrimEnd(','));
//                mail.From = new MailAddress(_objModelMail.From);
//                mail.Subject = _objModelMail.Subject;
//                string Body = _objModelMail.Body;
//                mail.Body = Body;
//                mail.IsBodyHtml = false;
//                SmtpClient smtp = new SmtpClient();
//                smtp.Port = 587;
//                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
//                smtp.UseDefaultCredentials = false;
//                smtp.Host = "mail.bharatseats.net";
//                //smtp.Port = 25;
//                //smtp.UseDefaultCredentials = false;
//                smtp.Credentials = new System.Net.NetworkCredential("rahul.narula@bharatseats.net", "bsl@1234");

//                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
//System.Security.Cryptography.X509Certificates.X509Certificate certificate,
//System.Security.Cryptography.X509Certificates.X509Chain chain,
//System.Net.Security.SslPolicyErrors sslPolicyErrors)
//                {
//                    return true;
//                };
//                // ("virendra.pal@infodartmail.com", "9336100753");// Enter seders User name and password  
//                smtp.EnableSsl = false;
//                smtp.Send(mail);

                MailMessage mail = new MailMessage();
                mail.To.Add(_objModelMail.To.TrimEnd(','));
                mail.From = new MailAddress(_objModelMail.From);
                mail.Subject = _objModelMail.Subject;
                string Body = _objModelMail.Body;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Host = "smtp.rediffmailpro.com";
                //smtp.Port = 25;
                //smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("bslvendorportal@infodartmail.com", "Okyzs4d");
                // ("virendra.pal@infodartmail.com", "9336100753");// Enter seders User name and password  
                smtp.EnableSsl = false;
                smtp.Send(mail);

                return true;
        }
            catch(Exception ex)
            {
                return false;
            }
}
    }
}
