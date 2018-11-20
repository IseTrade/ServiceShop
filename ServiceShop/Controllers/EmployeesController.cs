using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ServiceShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ServiceShop;

namespace ServiceShop.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees

        public ApplicationDbContext db;
        public ApplicationUser user;
        public EmployeesController()
        {
            db = new ApplicationDbContext();
            user = new ApplicationUser();
        }
        [HttpGet]
        public ActionResult Index()
        {
            //var empList = db.Employees.ToList();
            //return View(empList);
            var UserId = User.Identity.GetUserId();
            var emp = db.Employees.Where(c => c.ApplicationUserId == UserId).ToList();
            return View(emp);

        }

        //[HttpGet]
        //public ActionResult CustomerList(Employee employee)
        //{
        //    currentUserId= User.Identity.GetUserId();
        //    var employee = db.Employees.Where(e => e.ApplicationUserId == currentUserId).FirstOrDefault();
        //    var customerList = db.Customers.Where(c => c.Id == employee.Id).ToList();
          
        //    return View(customerList);
        //    //test
        //}
        // GET: Employees/Details/5
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        [HttpGet]
        public ActionResult Create()
        {
            //var userId = User.Identity.GetUserId();
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create(FormCollection collection)
        public ActionResult Create([Bind(Include = "Id,Name,Email")] Employee employee)
        {

            if (ModelState.IsValid)
            {
                employee.ApplicationUserId = User.Identity.GetUserId();
                //employee.Email = user.Email;
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
            //try
            //{
            //    // TODO: Add insert logic here

            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                employee.ApplicationUserId = User.Identity.GetUserId();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
            //try
            //{
            //    // TODO: Add update logic here

            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
            //try
            //{
            //    // TODO: Add delete logic here

            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }
    }
}
