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
    public class OstvareniAchievementiController : Controller
    {
        private BazaEntities db = new BazaEntities();

        // GET: OstvareniAchievementi
        public async Task<ActionResult> Index()
        {
            var ostvareniAchievementi = 
                db.OstvareniAchievementi.Include(o => o.Achievements).Include(o => o.UserInfo);
            return View(await ostvareniAchievementi.ToListAsync());
        }

        // GET: OstvareniAchievementi/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OstvareniAchievementi ostvareniAchievementi = await db.OstvareniAchievementi.FindAsync(id);
            if (ostvareniAchievementi == null)
            {
                return HttpNotFound();
            }
            return View(ostvareniAchievementi);
        }

        // GET: OstvareniAchievementi/Create
        public ActionResult Create()
        {
            ViewBag.AchievementID = new SelectList(db.Achievements, "AchievementID", "Name");
            ViewBag.UserID = new SelectList(db.UserInfo, "UserID", "Username");
            return View();
        }

        // POST: OstvareniAchievementi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserID,AchievementID,AchivementOstvaren")] 
            OstvareniAchievementi ostvareniAchievementi)
        {
            if (ModelState.IsValid)
            {
                db.OstvareniAchievementi.Add(ostvareniAchievementi);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AchievementID = new SelectList(db.Achievements, "AchievementID", "Name", 
                ostvareniAchievementi.AchievementID);
            ViewBag.UserID = new SelectList(db.UserInfo, "UserID", "Username", ostvareniAchievementi.UserID);
            return View(ostvareniAchievementi);
        }

        // GET: OstvareniAchievementi/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OstvareniAchievementi ostvareniAchievementi = await db.OstvareniAchievementi.FindAsync(id);
            if (ostvareniAchievementi == null)
            {
                return HttpNotFound();
            }
            ViewBag.AchievementID = new SelectList(db.Achievements, "AchievementID", "Name", 
                ostvareniAchievementi.AchievementID);
            ViewBag.UserID = new SelectList(db.UserInfo, "UserID", "Username", ostvareniAchievementi.UserID);
            return View(ostvareniAchievementi);
        }

        // POST: OstvareniAchievementi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserID,AchievementID,AchivementOstvaren")] 
            OstvareniAchievementi ostvareniAchievementi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ostvareniAchievementi).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AchievementID = new SelectList(db.Achievements, "AchievementID", "Name", 
                ostvareniAchievementi.AchievementID);
            ViewBag.UserID = new SelectList(db.UserInfo, "UserID", "Username", ostvareniAchievementi.UserID);
            return View(ostvareniAchievementi);
        }

        // GET: OstvareniAchievementi/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OstvareniAchievementi ostvareniAchievementi = await db.OstvareniAchievementi.FindAsync(id);
            if (ostvareniAchievementi == null)
            {
                return HttpNotFound();
            }
            return View(ostvareniAchievementi);
        }

        // POST: OstvareniAchievementi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OstvareniAchievementi ostvareniAchievementi = await db.OstvareniAchievementi.FindAsync(id);
            db.OstvareniAchievementi.Remove(ostvareniAchievementi);
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

        public ActionResult GenerirajAchievemente()
        {
            var ostvareniAchievement = 
                db.OstvareniAchievementi.Include(o => o.Achievements).Include(o => o.UserInfo).ToList();
            var useri = db.UserInfo.ToList();
            var achievementi = db.Achievements.ToList();
            var helpLista = new List<OstvareniAchievementi>();
            foreach (var user in useri)
            {
                foreach (var achiev in achievementi)
                {
                    OstvareniAchievementi ostvaren =  new OstvareniAchievementi{
                        UserID = user.UserID,
                        AchievementID = achiev.AchievementID,

                    };
                    foreach (var ostAchiev in ostvareniAchievement)
                    {
                        if (!mojEquals(ostvaren, ostAchiev))
                        {
                            helpLista.Add(new OstvareniAchievementi{ UserID = user.UserID, 
                                AchievementID = achiev.AchievementID, AchivementOstvaren = false });
                        }
                    }
                    
                }
            }
            db.OstvareniAchievementi.AddRange(helpLista);
            db.SaveChanges();
            return RedirectToAction("Index", "Achievements");
        }

        private bool mojEquals(OstvareniAchievementi generiran, OstvareniAchievementi baza)
        {
            return generiran.AchievementID.Equals(baza.AchievementID) && generiran.UserID.Equals(baza.UserID);
        }
    }
}
