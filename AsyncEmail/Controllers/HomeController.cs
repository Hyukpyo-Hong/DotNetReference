using AsyncEmail.Models;
using System;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AsyncEmail.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sent()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailFormModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                    var message = new MailMessage();
                    message.To.Add(new MailAddress("hhong@pharmacentra.com"));
                    message.Subject = "Your email subject";
                    message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                    message.IsBodyHtml = true;
                    message.Attachments.Add(new Attachment(HttpContext.Server.MapPath("~/Content/Site.css")));

                    foreach (var file in model.Upload)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var path = Path.Combine(Server.MapPath("~/App_Data/"), fileName);
                            file.SaveAs(path);
                            message.Attachments.Add(new Attachment(file.InputStream, Path.GetFileName(file.FileName)));
                        }
                    }

                    using (var smtp = new SmtpClient())
                    {
                        await smtp.SendMailAsync(message);
                        return RedirectToAction("Sent");
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }
            }
            return RedirectToAction("Index");
        }
    }
}