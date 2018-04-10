using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data;
using PagedList;

namespace MVC_CURD.Controllers
{
    public class EmployeesController : Controller
    {
        private IEmployee repository;
        private IUnitofWork unitOfWork;


        public EmployeesController(IEmployee context, IUnitofWork unitofWork)
        {

            repository = context;
            unitOfWork = unitofWork;
        }

        // GET: Employees
        public ActionResult Index(string Sorting_Order, int? Page_No)
        {
            ViewBag.CurrentSortOrder = Sorting_Order;
            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "Name_Description" : "";

            var employee = repository.GetAll();
            switch (Sorting_Order)
            {
                case "Name_Description":
                    employee = employee.OrderByDescending(stu => stu.FirstName);
                    break;               
                default:
                    employee = employee.OrderBy(stu => stu.FirstName);
                    break;
            }
            int Size_Of_Page = 1;
            int No_Of_Page = (Page_No ?? 1);
            return View(employee.ToList().ToPagedList(No_Of_Page, Size_Of_Page));  

           
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = repository.FindBy(x => x.EmployeeId == id).FirstOrDefault();
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeId,FirstName,LastName")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                repository.Add(employee);
                unitOfWork.CommiteChanges();
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
            Employee employee = repository.FindBy(x => x.EmployeeId == id).FirstOrDefault();
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,FirstName,LastName")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                repository.Edit(employee);
                unitOfWork.CommiteChanges();
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
            Employee employee = repository.FindBy(x => x.EmployeeId == id).FirstOrDefault();
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = repository.FindBy(x => x.EmployeeId == id).FirstOrDefault();
            repository.Delete(employee);
            unitOfWork.CommiteChanges();
            return RedirectToAction("Index");
        }


    }
}
