using System.Net.Mail;
using System.Net;

namespace Copmany.MVC.PL.Helper
{
    public static class EmailSetting
    {
        public static bool SendEmail( Email email)
        {
            try
            {
                var client = new SmtpClient(host: "smtp.gmail.com", port: 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(userName: "amiraabdelazez26@gmail.com", password: "myzdmnfbyyxdbwwr");
                client.Send(from: "ahmedaminc41@gmail.com", email.To, email.Subject, email.Body);
                return true;
               
 }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
