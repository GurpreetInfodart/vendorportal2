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
    public class CityController : Controller
    {
        public VendorPortalEntities db = new VendorPortalEntities();
        List<tblCity> objCityList = null;
        // GET: tblCities
        public ActionResult Index()
        {

            objCityList = db.tblCities.ToList();
            return View("Index",objCityList);
        }

        // GET: tblCities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCity tblCity = db.tblCities.Find(id);
            if (tblCity == null)
            {
                return HttpNotFound();
            }


            return View("Details",tblCity);
        }

        // GET: tblCities/Create
        public ActionResult Create()
        {
            List<SelectListItem> _stateList = (from s in db.STATE_MST.AsEnumerable()
                                               select new SelectListItem
                                               {
                                                   Text = s.STATE_CODE,
                                                   Value = s.StateId.ToString()
                                               }).ToList();
            //_stateList.Insert(0, new SelectListItem { Text = "--Select--", Value = "0" });
            ViewData["stateList"] = _stateList;
            return View("Create");
        }

        // POST: tblCities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CityName,Active,StateId")] tblCity tblCity)
        {
            if (ModelState.IsValid)
            {
                var stateNme = (from a in db.STATE_MST
                                    where a.StateId == tblCity.StateId
                                    select new { a.STATE_CODE }).ToList();
                tblCity.CreatedBy = 1;
                tblCity.CreatedOn = DateTime.Now;
                tblCity.StateName = stateNme[0].STATE_CODE;
                db.tblCities.Add(tblCity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index");
        }

        // GET: tblCities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCity tblCity = db.tblCities.Find(id);
            TempData["CityId"] = id;
            TempData.Keep();
            List<SelectListItem> _stateList = (from s in db.STATE_MST.AsEnumerable()
                                               select new SelectListItem
                                               {
                                                   Text = s.STATE_CODE,
                                                   Value = s.StateId.ToString()
                                               }).ToList();
            //_stateList.Insert(0, new SelectListItem { Text = "--Select--", Value = "0" });
            ViewData["stateList"] = _stateList;

            if (tblCity == null)
            {
                return HttpNotFound();
            }
            return View("Edit",tblCity);
        }

        // POST: tblCities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblCity tblCity)
        {
            if (ModelState.IsValid)
            {
                Int32 cityId = (int)TempData["CityId"];
                tblCity _dt = null;
                _dt = db.tblCities.Where(x => x.CityId == cityId).ToList().FirstOrDefault();

                var stateNme = (from a in db.STATE_MST
                          where a.StateId == tblCity.StateId
                          select new { a.STATE_CODE }).ToList();

                _dt.CityName = tblCity.CityName;
                _dt.UpdatedBy = 1;
                _dt.UpdatedOn = DateTime.Now;
                _dt.StateName = stateNme[0].STATE_CODE;                
                _dt.StateId = tblCity.StateId;
                

                db.Entry(_dt).State = EntityState.Modified;
               
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        // GET: tblCities/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tblCity tblCity = db.tblCities.Find(id);
        //    if (tblCity == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View("Index", tblCity);
        //}

        // POST: tblCities/Delete/5
        [HttpGet]        
        public ActionResult Delete(int? id)
        {
            tblCity tblCity = db.tblCities.Find(id);
            db.tblCities.Remove(tblCity);
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
