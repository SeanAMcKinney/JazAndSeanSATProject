using System.Web.Mvc;
using JazAndSeanSATProject.UI.MVC.Models;
using System.Configuration;
using System.Net.Mail;
using System.Net;


namespace JazAndSeanSATProject.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpPost]
        public ActionResult Contact(ContactViewModel cvm)
        {

            if (!ModelState.IsValid)
            {
                return View(cvm);
            }//end if

            string message = $"You have recieved an email from {cvm.Name}.<br/>" +
                $"Subject: {cvm.Subject}<br/>" +
                $"Message: {cvm.Message}<br/>" +
                $"Please reply to {cvm.Email} with your response at your earliest convenience.";

            //MailMessage - What sends the email
            //Overload for MailMessage - FORM, TO, SUBJECT, BOSY
            MailMessage mm = new MailMessage(ConfigurationManager.AppSettings["EmailUser"].ToString(), ConfigurationManager.AppSettings["EmailTo"].ToString(), cvm.Subject, message);

            //MailMessage Properties
            mm.IsBodyHtml = true;
            mm.Priority = MailPriority.High;
            //Reply to the sender and not our website/webmail
            mm.ReplyToList.Add(cvm.Email);

            //SmtpClient - Info from the host that allows emails to be sent
            SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["EmailClient"].ToString());

            //Client Credentials
            client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailUser"].ToString(), ConfigurationManager.AppSettings["EmailPass"].ToString());

            //Port options
            //Test with both to make sure your internet provider isn't blocking one or the other
            //client.Port = 25;
            client.Port = 8889;

            //Try to send the email
            try
            {
                //Attempt to send the email
                client.Send(mm);
            }
            catch (System.Exception ex)
            {

                ViewBag.CustomerMessage = $"We're sorry but your request could not be completed at this time. " +
                    $"Please try again later. If the issue persists, please contact your site administrator " +
                    $"and provide the following info:<br/>{ex.Message}";
                return View(cvm);
            }// end try/catch

            //If you make it here you have succeeded
            return View("EmailConfirmation", cvm);

        }
    }
}
