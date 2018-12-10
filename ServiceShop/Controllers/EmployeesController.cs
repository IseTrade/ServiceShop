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
        public ActionResult Index() //Shows logged in employee
        {

            var UserId = User.Identity.GetUserId();
            var emp = db.Employees.Where(c => c.ApplicationUserId == UserId).ToList();
            return View(emp);

        }

        [HttpGet]
        public ActionResult Index2() //Shows all employees
        {
            var empList = db.Employees.ToList();
            return View(empList);
        }

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
                //test employee.ApplicationUserId = User.Identity.GetUserId();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);

        }


        // GET: Employees/EditRating/5
        public ActionResult EditRating(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            var oldRate = employee.Rating; //testing
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRating([Bind(Include = "Id,Name,Email,Rating,RatingData,RateTemp,ApplicationUserId")] Employee employee)
        {
            //int rateCounter = 0;
            if (ModelState.IsValid)
            {
                //var empId = employee.ApplicationUserId;
                db.Entry(employee).State = EntityState.Modified;
                employee.RatingData += employee.RateTemp; //Concats new rating to RatingData string
                var rateCharArray = employee.RatingData.ToCharArray(); //Converts string to array of char
                int[] rateIntArray = Array.ConvertAll(rateCharArray, c => (int)Char.GetNumericValue(c)); //Converts CharArray to IntArray
                double rateAverage = rateIntArray.Average(); //Get Average from IntArray
                employee.Rating = Math.Round(rateAverage, 2); //Passing calculated rate average to employee.Rating
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
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

        }
    }
}
