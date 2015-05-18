using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjektPPiJ.Models;

namespace ProjektPPiJ.Controllers
{
    public class AchievementsController : Controller
    {
        private BazaEntities db = new BazaEntities();

        // GET: Achievements
        public async Task<ActionResult> Index()
        {
            return View(await db.Achievements.ToListAsync());
        }

        // GET: Achievements/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Achievements achievements = await db.Achievements.FindAsync(id);
            if (achievements == null)
            {
                return HttpNotFound();
            }
            return View(achievements);
        }

        // GET: Achievements/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Achievements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AchievementID,Name,Poruka,Slika,PutanjaSlike")] Achievements achievements)
        {
            if (ModelState.IsValid)
            {
                db.Achievements.Add(achievements);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(achievements);
        }

        // GET: Achievements/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Achievements achievements = await db.Achievements.FindAsync(id);
            if (achievements == null)
            {
                return HttpNotFound();
            }
            return View(achievements);
        }

        // POST: Achievements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AchievementID,Name,Poruka,Slika,PutanjaSlike")] Achievements achievements)
        {
            if (ModelState.IsValid)
            {
                db.Entry(achievements).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(achievements);
        }

        // GET: Achievements/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Achievements achievements = await db.Achievements.FindAsync(id);
            if (achievements == null)
            {
                return HttpNotFound();
            }
            return View(achievements);
        }

        // POST: Achievements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Achievements achievements = await db.Achievements.FindAsync(id);
            db.Achievements.Remove(achievements);
            await db.SaveChangesAsync();
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
