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

        // GET: All Customers for particular employee
        [HttpGet]
        public ActionResult CustomerList2()  // This brings up all customers for particular employee //TO DO
        {
            var empId = User.Identity.GetUserId();
            var currentEmp = db.Employees.Where(e => e.ApplicationUserId == empId).SingleOrDefault();
            var serv = db.Services.Where(s => s.EmployeeId == currentEmp.Id).ToList(); //List of orders for logged in employee
            //var custList = db.Customers.Where(c => c.Id == serv.
            return View(custList);
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
            //try
            //{
            //    // TODO: Add insert logic here

            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
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
            //try
            //{
            //    // TODO: Add update logic here

            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
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
            //try
            //{
            //    // TODO: Add delete logic here

            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
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
            //var UserId = User.Identity.GetUserId();
            //var cust = db.Customers.Where(c => c.ApplicationUserId == UserId).First();

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
