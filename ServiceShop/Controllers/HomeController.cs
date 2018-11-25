using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using ServiceShop.Models;
using System.Threading.Tasks;
using System.Configuration;
using Stripe;


namespace ServiceShop.Controllers
{
    public class HomeController : Controller
   {       

        public ActionResult Stripe()
        {
            var stripePublishKey = ConfigurationManager.AppSettings["stripePublishableKey"];
            ViewBag.StripePublishKey = stripePublishKey;
            return View();
        }

        public ActionResult Charge(string stripeEmail, string stripeToken)
        {
            string totalAmount = Request.Form["price"].Replace(".","");//Convert to cents for stripe by removing decimals
            int totalCents = Convert.ToInt32(totalAmount);

            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                SourceToken = stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Description = "Sample Charge",
                Currency = "USD",
                Amount = totalCents,
                CustomerId = customer.Id
            });

            ViewBag.TotalBill = Convert.ToDouble(totalAmount) / 100;//send the amount back to success page

            // further application specific code goes here

            return View();
        }

        //=================================================================
        public ActionResult Chat()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailFormModel model)
        {
            if (ModelState.IsValid)
            {
                var body = "";
                body += "<p>From: {0} ({1})</p>";
                body += "<p>Message:</p>";
                body += "<hr>";
                body += "<p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("isetrade@gmail.com")); //destination e-mail address
                message.Subject = "Your email subject";
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    try
                    {
                        await smtp.SendMailAsync(message);
                    }
                    catch (Exception e)
                    {
                        Response.Write("<p>" + e.Message + "</p>");
                    }
                    return RedirectToAction("Sent");
                }
            }
            return View(model);
        }

        public ActionResult Sent()
        {
            return View();
        }

    }
}