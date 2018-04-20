using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Views
{
    public class PROJECTsController : Controller
    {
        private ProjectManagementSystemEntities db = new ProjectManagementSystemEntities();

        // GET: PROJECTs
        public ActionResult Index()
        {
            var pROJECTs = db.PROJECTs.Include(p => p.DEPARTMENT).Include(p => p.EMPLOYEE);
            return View(pROJECTs.ToList());
        }

        // GET: PROJECTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROJECT pROJECT = db.PROJECTs.Find(id);
            if (pROJECT == null)
            {
                return HttpNotFound();
            }
            return View(pROJECT);
        }

        // GET: PROJECTs/Create
        public ActionResult Create()
        {
            ViewBag.Department_ID = new SelectList(db.DEPARTMENTs, "Dept_ID", "Name");
            ViewBag.Manager_ID = new SelectList(db.EMPLOYEEs, "Employee_ID", "F_name");
            return View();
        }

        // POST: PROJECTs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Project_ID,USA_region,Deadline,Budget,Start_date,End_date,Progress_status,Last_update,Last_update_by,Department_ID,Manager_ID")] PROJECT pROJECT)
        {
            if (ModelState.IsValid)
            {
                db.PROJECTs.Add(pROJECT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Department_ID = new SelectList(db.DEPARTMENTs, "Dept_ID", "Name", pROJECT.Department_ID);
            ViewBag.Manager_ID = new SelectList(db.EMPLOYEEs, "Employee_ID", "F_name", pROJECT.Manager_ID);
            return View(pROJECT);
        }

        // GET: PROJECTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROJECT pROJECT = db.PROJECTs.Find(id);
            if (pROJECT == null)
            {
                return HttpNotFound();
            }
            ViewBag.Department_ID = new SelectList(db.DEPARTMENTs, "Dept_ID", "Name", pROJECT.Department_ID);
            ViewBag.Manager_ID = new SelectList(db.EMPLOYEEs, "Employee_ID", "F_name", pROJECT.Manager_ID);
            return View(pROJECT);
        }

        // POST: PROJECTs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name,Project_ID,USA_region,Deadline,Budget,Start_date,End_date,Progress_status,Last_update,Last_update_by,Department_ID,Manager_ID")] PROJECT pROJECT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pROJECT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Department_ID = new SelectList(db.DEPARTMENTs, "Dept_ID", "Name", pROJECT.Department_ID);
            ViewBag.Manager_ID = new SelectList(db.EMPLOYEEs, "Employee_ID", "F_name", pROJECT.Manager_ID);
            return View(pROJECT);
        }

        // GET: PROJECTs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROJECT pROJECT = db.PROJECTs.Find(id);
            if (pROJECT == null)
            {
                return HttpNotFound();
            }
            return View(pROJECT);
        }

        // POST: PROJECTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PROJECT pROJECT = db.PROJECTs.Find(id);
            db.PROJECTs.Remove(pROJECT);
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
