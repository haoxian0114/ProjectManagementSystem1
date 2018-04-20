﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Controllers
{
    public class EMPLOYEEsController : Controller
    {
        private ProjectManagementSystemEntities db = new ProjectManagementSystemEntities();

        // GET: EMPLOYEEs
        [HttpGet]
        public ActionResult Index()
        {
            var eMPLOYEEs = db.EMPLOYEEs.Include(e => e.EMPLOYEE_DELIVERABLE);
            var getTypeList = db.EMPLOYEE_TYPES.ToList();
            SelectList list = new SelectList(getTypeList, "TypeID", "Employee_Type");
            ViewBag.employeetype = list;
            return View(eMPLOYEEs.ToList());
        }

        [HttpPost]
        public ActionResult Index(string FirstName, string EmpType, string LastName, string Email, EMPLOYEE emp)
        {
            var eMPLOYEEs = db.EMPLOYEEs.ToList().Where(p => p.F_name.StartsWith(FirstName) && p.Employee_type.StartsWith(EmpType)
            && p.L_name.StartsWith(LastName) && p.Email_address.Contains(Email));
            var getTypeList = db.EMPLOYEE_TYPES.ToList();
            SelectList list = new SelectList(getTypeList, "TypeID", "Employee_Type");
            ViewBag.employeetype = list;
            return View(eMPLOYEEs);
        }

        // GET: EMPLOYEEs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLOYEE eMPLOYEE = db.EMPLOYEEs.Find(id);
            if (eMPLOYEE == null)
            {
                return HttpNotFound();
            }
            return View(eMPLOYEE);
        }

        // GET: EMPLOYEEs/Create
        public ActionResult Create()
        {
            var getTypeList = db.EMPLOYEE_TYPES.ToList();
            SelectList list = new SelectList(getTypeList, "TypeID", "Employee_Type");
            ViewBag.employeetype = list;
            ViewBag.Employee_ID = new SelectList(db.EMPLOYEE_DELIVERABLE, "Employee_deliverable_ID", "Last_update_by");
            return View();
        }

        // POST: EMPLOYEEs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Employee_ID,F_name,Employee_type,Hourly_rate,M_name,L_name,Last_update,Last_update_by,Email_address")] EMPLOYEE eMPLOYEE)
        {
            if (ModelState.IsValid)
            {
                db.EMPLOYEEs.Add(eMPLOYEE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var getTypeList = db.EMPLOYEE_TYPES.ToList();
            SelectList list = new SelectList(getTypeList, "TypeID", "Employee_Type");
            ViewBag.employeetype = list;
            ViewBag.Employee_ID = new SelectList(db.EMPLOYEE_DELIVERABLE, "Employee_deliverable_ID", "Last_update_by", eMPLOYEE.Employee_ID);
            return View(eMPLOYEE);
        }

        // GET: EMPLOYEEs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLOYEE eMPLOYEE = db.EMPLOYEEs.Find(id);
            if (eMPLOYEE == null)
            {
                return HttpNotFound();
            }
            var getTypeList = db.EMPLOYEE_TYPES.ToList();
            SelectList list = new SelectList(getTypeList, "TypeID", "Employee_Type");
            ViewBag.employeetype = list;
            ViewBag.Employee_ID = new SelectList(db.EMPLOYEE_DELIVERABLE, "Employee_deliverable_ID", "Last_update_by", eMPLOYEE.Employee_ID);
            return View(eMPLOYEE);
        }

        // POST: EMPLOYEEs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Employee_ID,F_name,Employee_type,Hourly_rate,M_name,L_name,Last_update,Last_update_by,Email_address")] EMPLOYEE eMPLOYEE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eMPLOYEE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var getTypeList = db.EMPLOYEE_TYPES.ToList();
            SelectList list = new SelectList(getTypeList, "TypeID", "Employee_Type");
            ViewBag.employeetype = list;
            ViewBag.Employee_ID = new SelectList(db.EMPLOYEE_DELIVERABLE, "Employee_deliverable_ID", "Last_update_by", eMPLOYEE.Employee_ID);
            return View(eMPLOYEE);
        }

        // GET: EMPLOYEEs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLOYEE eMPLOYEE = db.EMPLOYEEs.Find(id);
            if (eMPLOYEE == null)
            {
                return HttpNotFound();
            }
            return View(eMPLOYEE);
        }

        // POST: EMPLOYEEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EMPLOYEE eMPLOYEE = db.EMPLOYEEs.Find(id);
            db.EMPLOYEEs.Remove(eMPLOYEE);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);

        }
       
    }
}
