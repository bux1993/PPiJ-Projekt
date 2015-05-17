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
    public class KategorijeController : Controller
    {
        private BazaEntities db = new BazaEntities();

        // GET: Kategorije
        public async Task<ActionResult> Index()
        {
            var kategorije = db.Kategorije.Include(k => k.Predmeti);
            return View(await kategorije.ToListAsync());
        }

        // GET: Kategorije/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategorije kategorije = await db.Kategorije.FindAsync(id);
            if (kategorije == null)
            {
                return HttpNotFound();
            }
            return View(kategorije);
        }

        // GET: Kategorije/Create
        public ActionResult Create()
        {
            ViewBag.PredmetID = new SelectList(db.Predmeti, "PredmetID", "PredmetName");
            return View();
        }

        // POST: Kategorije/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "KategorijaID,PredmetID,KategorijaName")] Kategorije kategorije)
        {
            if (ModelState.IsValid)
            {
                db.Kategorije.Add(kategorije);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PredmetID = new SelectList(db.Predmeti, "PredmetID", "PredmetName", kategorije.PredmetID);
            return View(kategorije);
        }

        // GET: Kategorije/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategorije kategorije = await db.Kategorije.FindAsync(id);
            if (kategorije == null)
            {
                return HttpNotFound();
            }
            ViewBag.PredmetID = new SelectList(db.Predmeti, "PredmetID", "PredmetName", kategorije.PredmetID);
            return View(kategorije);
        }

        // POST: Kategorije/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "KategorijaID,PredmetID,KategorijaName")] Kategorije kategorije)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kategorije).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PredmetID = new SelectList(db.Predmeti, "PredmetID", "PredmetName", kategorije.PredmetID);
            return View(kategorije);
        }

        // GET: Kategorije/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategorije kategorije = await db.Kategorije.FindAsync(id);
            if (kategorije == null)
            {
                return HttpNotFound();
            }
            return View(kategorije);
        }

        // POST: Kategorije/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Kategorije kategorije = await db.Kategorije.FindAsync(id);
            db.Kategorije.Remove(kategorije);
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

        public async Task<ActionResult> OdaberiKategoriju(int? predmetID)
        {
            var kategorijeIzbor =  await db.Kategorije.Where(m => m.PredmetID == predmetID).ToListAsync();
            if (kategorijeIzbor == null)
            {
                HttpNotFound();
            }
            return View(kategorijeIzbor);
        }
    }
}
