using Microsoft.AspNet.Identity;
using ServiceShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ServiceShop.Controllers
{
    public class CustomersController : Controller
    {
        public ApplicationDbContext db;
        public ApplicationUser user;
        public CustomersController()
        {
            db = new ApplicationDbContext();
            user = new ApplicationUser();

        }

        // GET: Customers
        [HttpGet]
        public ActionResult Index() //This brings up only customer who's currently logged in.
        {
            //var custList = db.Customers.ToList();
            //return View(custList);

            //customer.ApplicationUserId = User.Identity.GetUserId();
            //var cust = db.Customers.Where(c => c.Id == customer.Id).ToList();
            //return View(cust);

            var UserId = User.Identity.GetUserId();
            var cust = db.Customers.Where(c => c.ApplicationUserId == UserId).ToList();

            //var address = cust.Select(a => a.Address).FirstOrDefault();
            //var city = cust.Select(c => c.City).FirstOrDefault();
            //var state = cust.Select(s => s.State).FirstOrDefault();

            //ViewBag.StartAddress = address + ", " + city + ", " + state;//Valid

            return View(cust);
        }

        // GET: All Customers
        [HttpGet]
        public ActionResult CustomerList()  // This brings up all customers
        {
            var custList = db.Customers.ToList();
            return View(custList);
        }

        // GET: All Customers for the current logged in employee i mean from intention piont of view
        [HttpGet]
        public ActionResult CustomerList2()
        {
            //1. Get current employeeid
            var employeeId = User.Identity.GetUserId(); //Passed

            //2. Lookup in services table for all service.employeeId = currentemployee.id -> list (1, 2, 3)
            var currentEmployee = db.Employees.Where(e => e.ApplicationUserId == employeeId).FirstOrDefault(); //Passed
            var currentEmployeeServices = db.Services.Where(s => s.EmployeeId == currentEmployee.Id).Include(s => s.Customer).Select(s => s.Customer).ToList(); //Passed

            //3. Lookup in customers table for all id's IN step 2.
            //dynamic arrList = currentEmployeeServices.Select(cES => cES.Customer).ToList();
            //var currentEmployeeCustomersList = db.Customers.Where(c => c.In(arrList)).ToList();

            //4. List -> View
            //return View(currentEmployeeServices);
            return View("~/Views/Customers/CustomerList.cshtml", currentEmployeeServices);
        }

        [HttpGet]
        public ActionResult CustomerList3() //Customers with Completed Orders
        {

            var completedCustomers = db.Services.Where(s => s.WorkOrderStatus == "completed").Include(s => s.Customer).Select(s => s.Customer).ToList();

            return View("~/Views/Customers/CustomerList.cshtml", completedCustomers);
        }

        [HttpGet]
        public ActionResult CustomerList4() //Paid Customers
        {

            var paidCustomers = db.Services.Where(s => s.PaymentStatus == "paid").Include(s => s.Customer).Select(s => s.Customer).ToList();

            return View("~/Views/Customers/CustomerList.cshtml",paidCustomers);
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        //{
        //    return View();
        //}
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            var user = User.Identity.GetUserId();
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address,City,State,Zipcode,Phone,Email")] Customer customer)
        {

            //    return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                customer.ApplicationUserId = User.Identity.GetUserId();
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address,City,State,Zipcode,Phone,Email")] Customer customer)
        {

            //    return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                customer.ApplicationUserId = User.Identity.GetUserId();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {

            //    return RedirectToAction("Index");

            {
                Customer customer = db.Customers.Find(id);
                db.Customers.Remove(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        // GET: Show map
        [HttpGet]
        public ActionResult Map()  //Showing google map and directions
        {

            var UserId = User.Identity.GetUserId(); //grabs id of person logged in if /Customers/Map is accessed

            if (User.Identity.IsAuthenticated)
            {
                var cust = db.Customers.Where(c => c.ApplicationUserId == UserId).ToList();

                var address = cust.Select(a => a.Address).FirstOrDefault();
                var city = cust.Select(c => c.City).FirstOrDefault();
                var state = cust.Select(s => s.State).FirstOrDefault();

                ViewBag.StartAddress = address + ", " + city + ", " + state;//Valid
                return View();
            }
            return Index();
        }
    }
}
