using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjektPPiJ.Models;

namespace ProjektPPiJ.Controllers
{
    public class OstvareniAchievemntiController : Controller
    {
        private ProjektBazaEntities db = new ProjektBazaEntities();

        // GET: OstvareniAchievemnti
        public ActionResult Index()
        {
            var ostvareniAchievemntis = db.OstvareniAchievemntis.Include(o => o.Achievement).Include(o => o.UserInfo);
            return View(ostvareniAchievemntis.ToList());
        }

        // GET: OstvareniAchievemnti/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OstvareniAchievemnti ostvareniAchievemnti = db.OstvareniAchievemntis.Find(id);
            if (ostvareniAchievemnti == null)
            {
                return HttpNotFound();
            }
            return View(ostvareniAchievemnti);
        }

        // GET: OstvareniAchievemnti/Create
        public ActionResult Create()
        {
            ViewBag.AchievementID = new SelectList(db.Achievements, "AchievementID", "Name");
            ViewBag.UserID = new SelectList(db.UserInfoes, "UserID", "Username");
            return View();
        }

        // POST: OstvareniAchievemnti/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,AchievementID,AchivementOstvaren")] OstvareniAchievemnti ostvareniAchievemnti)
        {
            if (ModelState.IsValid)
            {
                db.OstvareniAchievemntis.Add(ostvareniAchievemnti);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AchievementID = new SelectList(db.Achievements, "AchievementID", "Name", ostvareniAchievemnti.AchievementID);
            ViewBag.UserID = new SelectList(db.UserInfoes, "UserID", "Username", ostvareniAchievemnti.UserID);
            return View(ostvareniAchievemnti);
        }

        // GET: OstvareniAchievemnti/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OstvareniAchievemnti ostvareniAchievemnti = db.OstvareniAchievemntis.Find(id);
            if (ostvareniAchievemnti == null)
            {
                return HttpNotFound();
            }
            ViewBag.AchievementID = new SelectList(db.Achievements, "AchievementID", "Name", ostvareniAchievemnti.AchievementID);
            ViewBag.UserID = new SelectList(db.UserInfoes, "UserID", "Username", ostvareniAchievemnti.UserID);
            return View(ostvareniAchievemnti);
        }

        // POST: OstvareniAchievemnti/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,AchievementID,AchivementOstvaren")] OstvareniAchievemnti ostvareniAchievemnti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ostvareniAchievemnti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AchievementID = new SelectList(db.Achievements, "AchievementID", "Name", ostvareniAchievemnti.AchievementID);
            ViewBag.UserID = new SelectList(db.UserInfoes, "UserID", "Username", ostvareniAchievemnti.UserID);
            return View(ostvareniAchievemnti);
        }

        // GET: OstvareniAchievemnti/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OstvareniAchievemnti ostvareniAchievemnti = db.OstvareniAchievemntis.Find(id);
            if (ostvareniAchievemnti == null)
            {
                return HttpNotFound();
            }
            return View(ostvareniAchievemnti);
        }

        // POST: OstvareniAchievemnti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OstvareniAchievemnti ostvareniAchievemnti = db.OstvareniAchievemntis.Find(id);
            db.OstvareniAchievemntis.Remove(ostvareniAchievemnti);
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
