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
using Microsoft.AspNet.Identity;

namespace ServiceShop.Controllers
{
    public class HomeController : Controller
    {
        public ApplicationDbContext db;
        public ApplicationUser user;
        //private static Random rnd;//Used for Live Chat Visitor#### randomname

        public ActionResult Stripe()
        {
            var stripePublishKey = ConfigurationManager.AppSettings["stripePublishableKey"];
            ViewBag.StripePublishKey = stripePublishKey;
            return View();
        }

        public ActionResult Charge(string stripeEmail, string stripeToken)
        {
            db = new ApplicationDbContext();
            user = new ApplicationUser();
            string totalAmount = Request.Form["price"].Replace(".", "");//Convert to cents for stripe by removing decimals
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

            var custId = User.Identity.GetUserId();
            var currentCust = db.Customers.Where(c => c.ApplicationUserId == custId).FirstOrDefault();
            var currentService = db.Services.Where(s => s.CustomerId == currentCust.Id).SingleOrDefault();
            currentService.PaymentStatus = "Paid";
            db.SaveChanges();  //Change payment status to paid once customer makes payment
            return View();
        }

        //=================================================================
        public ActionResult Chat()
        {
            //rnd = new Random();//Initialize seed value

            //if (User.Identity.IsAuthenticated)
            //{
            //    //Detect the current logged in user
            //    var userId = User.Identity.GetUserId();

            //    //Determine which one is logged in
            //    if (User.IsInRole("Customer"))
            //    {
            //        var currentCust = db.Customers.Where(c => c.ApplicationUserId == userId).FirstOrDefault();
            //        ViewBag.AutomaticDetectionOrGenerateName = currentCust;
            //    }
            //    else if (User.IsInRole("Employee"))
            //    {
            //        var currentEmpl = db.Employees.Where(e => e.ApplicationUserId == userId).FirstOrDefault();
            //        ViewBag.AutomaticDetectionOrGenerateName = currentEmpl;
            //    }
            //    else
            //    {
            //        //Set the name of the current customer as their live chat name
            //        ViewBag.AutomaticDetectionOrGenerateName = "Visitor" + rnd.Next(1, 50); //Depends on TYPE of user
            //    }
            //}
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