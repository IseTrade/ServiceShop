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
        //GET: Customers/CreateOrder
        public ActionResult CreateOrder()
        {
            var user = User.Identity.GetUserId();
            Service service = new Service();
            return View(service);
        }

        // POST: Customers/CreateOrder
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrder([Bind(Include = "Id,WorkOrderDate,Description,PictureUpload")] Service service)
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

        //GET: Employees/EmployeeWorkOrder
        public ActionResult EmployeeWorkOrder(int? id)
        {
            var user = User.Identity.GetUserId();
            Service service = db.Services.Where(s => s.Id == id).Single();
            return View(service);
        }

        // POST: Employees/EmployeeWorkOrder
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployeeWorkOrder([Bind(Include = "Id,MotherBoard,MotherBoardPrice,VideoCard,VideoCardPrice,PowerSupply,PowerSupplyPrice,Cpu,CpuPrice,HardDrive,HardDrivePrice,Case,CasePrice,Memory,MemoryPrice,Fan,FanPrice,CpuCooler,CpuCoolerPrice,VirusRemoval,DataRecovery,InstallOs,Labor,Comment,PaymentStatus,WorkOrderStatus,CustomerId,EmployeeId,WorkOrderDate,Description,PictureUpload")] Service service)
        {

            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var currentEmp = db.Employees.Where(e => e.ApplicationUserId == userId).FirstOrDefault();
                var tempEmp = db.Employees.Where(e => e.Email == "ua@gmail.com").SingleOrDefault();
                //var currentCust = db.Customers.Where(c => c.Id == service.CustomerId).FirstOrDefault();
                //var currentServ = db.Services.Where(s => s.CustomerId == currentCust.Id).SingleOrDefault();
                //service.CustomerId = currentCust.Id;
                //service.EmployeeId = tempEmp.Id;
                service.EmployeeId = currentEmp.Id;
                //db.Services.Add(service);
                db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("Index", "Employees", new { id = 11 });

            }

            return View("Index");
        }


        //================================================


        // GET: Services
        public ActionResult Index()
        {
            return View(db.Services.ToList());
        }

        // GET: Services/Details/5
        public ActionResult Details(int? id)
        {
            Service service = db.Services.Find(id);
            //Service serviceTest = db.Services.Where(s => s.Id == id).Include(s => s.Employee).Include(s => s.Customer).FirstOrDefault();

            CustomerEmployeeServiceVM cesVM = new CustomerEmployeeServiceVM();
            cesVM.service = service;
            cesVM.employee = db.Employees.Where(e => e.Id == service.EmployeeId).First();
            cesVM.customer = db.Customers.Where(c => c.Id == service.CustomerId).First();
            var SubTotal = Convert.ToDouble(service.MotherBoardPrice) + Convert.ToDouble(service.VideoCardPrice) + Convert.ToDouble(service.PowerSupplyPrice) + Convert.ToDouble(service.CpuCoolerPrice) + Convert.ToDouble(service.HardDrivePrice)+ Convert.ToDouble(service.CasePrice) + Convert.ToDouble(service.MemoryPrice) + Convert.ToDouble(service.FanPrice) + Convert.ToDouble(service.CpuCoolerPrice) + Convert.ToDouble(service.VirusRemoval) + Convert.ToDouble(service.DataRecovery) + Convert.ToDouble(service.InstallOs) + Convert.ToDouble(service.Labor);

            var Diagnostics = 40.00;
            var Taxes = SubTotal * 0.056;
            var TotalBill = SubTotal + Diagnostics + Taxes;
            ViewBag.SubTotal = SubTotal;
            ViewBag.Diagnostics = Diagnostics;
            ViewBag.Taxes = Taxes;
            ViewBag.TotalBill = TotalBill;

            return View(cesVM);
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
        public ActionResult Delete(int? id)
        {
            //Service service = db.Services.Find(id);
            ////Service serviceTest = db.Services.Where(s => s.Id == id).Include(s => s.Employee).Include(s => s.Customer).FirstOrDefault();

            //CustomerEmployeeServiceVM cesVM = new CustomerEmployeeServiceVM();
            //cesVM.service = service;
            //cesVM.employee = db.Employees.Where(e => e.Id == service.EmployeeId).First();
            //cesVM.customer = db.Customers.Where(c => c.Id == service.CustomerId).First();

            //return View(cesVM);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost]
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
                Service service = db.Services.Find(id);
                db.Services.Remove(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

        }
    }
}
