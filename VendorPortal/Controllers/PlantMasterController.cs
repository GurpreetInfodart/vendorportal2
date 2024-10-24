using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VendorPortal.Models;

namespace VendorPortal.Controllers
{
    public class PlantMasterController : Controller
    {
        private VendorPortalEntities db = new VendorPortalEntities();

        // GET: PlantMaster
        public ActionResult Index()
        {
            return View(db.Plant_Master.ToList());
        }

        // GET: PlantMaster/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plant_Master plant_Master = db.Plant_Master.Find(id);
            if (plant_Master == null)
            {
                return HttpNotFound();
            }
            return View(plant_Master);
        }

        // GET: PlantMaster/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlantMaster/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlantId,Plant_Code,Plant_Name")] Plant_Master plant_Master)
        {
            if (ModelState.IsValid)
            {
                db.Plant_Master.Add(plant_Master);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(plant_Master);
        }

        // GET: PlantMaster/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plant_Master plant_Master = db.Plant_Master.Find(id);
            if (plant_Master == null)
            {
                return HttpNotFound();
            }
            return View(plant_Master);
        }

        // POST: PlantMaster/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlantId,Plant_Code,Plant_Name")] Plant_Master plant_Master)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plant_Master).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(plant_Master);
        }

        // GET: PlantMaster/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plant_Master plant_Master = db.Plant_Master.Find(id);
            if (plant_Master == null)
            {
                return HttpNotFound();
            }
            return View(plant_Master);
        }

        // POST: PlantMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Plant_Master plant_Master = db.Plant_Master.Find(id);
            db.Plant_Master.Remove(plant_Master);
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
