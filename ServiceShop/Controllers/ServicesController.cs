﻿using Microsoft.AspNet.Identity;
using ServiceShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceShop.Controllers
{
    public class ServicesController : Controller
    {
        public ApplicationDbContext db;
        public ApplicationUser user;
        public ServicesController()
        {
            db = new ApplicationDbContext();
            user = new ApplicationUser();
        }

        //==========================================
        //GET: Customers/Create
        public ActionResult CreateOrder()
        {
            var user = User.Identity.GetUserId();
            Service service = new Service();
            return View(service);
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrder([Bind(Include = "Id,WorkOrderDate,Descriptions,PictureUpload")] Service service)
        {

            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var currentCust = db.Customers.Where(c => c.ApplicationUserId == userId).FirstOrDefault();
                var tempEmp = db.Employees.Where(e => e.Email == "ua@gmail.com").FirstOrDefault();
                service.CustomerId = currentCust.Id;
                service.EmployeeId = tempEmp.Id;
                db.Services.Add(service);
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("Index", "Customers", new { id = 11 });

            }

            return View("Index");
        }

        //=============================================



        // GET: Services
        public ActionResult Index()
        { 
            return View();
        }

        // GET: Services/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Services/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Services/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Services/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Services/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Services/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Services/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
