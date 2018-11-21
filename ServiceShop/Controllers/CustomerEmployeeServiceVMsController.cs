using ServiceShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceShop.Controllers
{
    public class CustomerEmployeeServiceVMsController : Controller
    {

        public ApplicationDbContext db;
        public ApplicationUser user;
        public CustomerEmployeeServiceVMsController()
        {
            db = new ApplicationDbContext();
            user = new ApplicationUser();
        }

        // GET: CustomerEmployeeServiceVMs
        public ActionResult Index()
        {
            return View();
        }

        // GET: CustomerEmployeeServiceVMs/Details/5
        public ActionResult Details(int id)
        {
            Service serv = db.Services.Find(id);
            Employee empl = db.Employees.Find(id);
            Customer cust = db.Customers.Find(id);
            CustomerEmployeeServiceVM finalView = new CustomerEmployeeServiceVM() { employee = empl, service = serv, customer = cust };
            return View(finalView);
        }

        // GET: CustomerEmployeeServiceVMs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerEmployeeServiceVMs/Create
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

        // GET: CustomerEmployeeServiceVMs/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomerEmployeeServiceVMs/Edit/5
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

        // GET: CustomerEmployeeServiceVMs/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerEmployeeServiceVMs/Delete/5
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
