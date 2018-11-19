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
        public ActionResult Index()
        {
            //var custList = db.Customers.ToList();
            //return View(custList);


            //customer.ApplicationUserId = User.Identity.GetUserId();
            //var cust = db.Customers.Where(c => c.Id == customer.Id).ToList();
            //return View(cust);

            var UserId = User.Identity.GetUserId();
            var cust = db.Customers.Where(c => c.ApplicationUserId == UserId).ToList();
            return View(cust);


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
    }
}
