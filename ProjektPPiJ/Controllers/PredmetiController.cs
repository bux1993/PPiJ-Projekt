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
    public class PredmetiController : Controller
    {
        private BazaEntities db = new BazaEntities();

        // GET: Predmeti
        public async Task<ActionResult> Index()
        {
            return View(await db.Predmeti.ToListAsync());
        }

        // GET: Predmeti/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Predmeti predmeti = await db.Predmeti.FindAsync(id);
            if (predmeti == null)
            {
                return HttpNotFound();
            }
            return View(predmeti);
        }

        // GET: Predmeti/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Predmeti/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PredmetID,PredmetName")] Predmeti predmeti)
        {
            if (ModelState.IsValid)
            {
                db.Predmeti.Add(predmeti);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(predmeti);
        }

        // GET: Predmeti/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Predmeti predmeti = await db.Predmeti.FindAsync(id);
            if (predmeti == null)
            {
                return HttpNotFound();
            }
            return View(predmeti);
        }

        // POST: Predmeti/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PredmetID,PredmetName")] Predmeti predmeti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(predmeti).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(predmeti);
        }

        // GET: Predmeti/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Predmeti predmeti = await db.Predmeti.FindAsync(id);
            if (predmeti == null)
            {
                return HttpNotFound();
            }
            return View(predmeti);
        }

        // POST: Predmeti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Predmeti predmeti = await db.Predmeti.FindAsync(id);
            db.Predmeti.Remove(predmeti);
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

        public async  Task<ActionResult> OdaberiPredmet()
        {
            return View( await db.Predmeti.ToListAsync());
        }
    }
}
