using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using Email_Gonderme.Models;
using System.Net;

namespace Email_Gonderme.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Email model,HttpPostedFileBase myFiles)
        {
            MailMessage mailim = new MailMessage(); 
            mailim.To.Add("maliniz@gmail.com"); //alıcı
            mailim.From = new MailAddress("mailiniz@gmail.com"); // gönderen
            mailim.Subject = "Bize ulaşın sayfasından mesajınız var" + model.Baslik;
            mailim.Body = "Sayın yetkili, " + model.AdSoyad +" kişisinden gelen mesajın içeriği aşağıdaki gibidir <br>" + model.Icerik;
            mailim.IsBodyHtml = true;

            if(myFiles!= null)
            {
                mailim.Attachments.Add(new Attachment(myFiles.InputStream, myFiles.FileName));
            }

            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential("mailiniz@gmail.com", "şifreniz");
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(mailim);
                TempData["Message"] = "Mesajınız iletilmiştir.";

            }
            catch (Exception ex)
            {
                TempData["Message"] = "Mail gönderilemedi.Hata Nedeni" + ex.Message;
            }


            return View();
        }
    }
}