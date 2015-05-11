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
    public class VrstaPitanjaController : Controller
    {
        private BazaEntities db = new BazaEntities();

        // GET: VrstaPitanja
        public async Task<ActionResult> Index()
        {
            return View(await db.VrstaPitanja.ToListAsync());
        }

        // GET: VrstaPitanja/Details/5
        public async Task<ActionResult> Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VrstaPitanja vrstaPitanja = await db.VrstaPitanja.FindAsync(id);
            if (vrstaPitanja == null)
            {
                return HttpNotFound();
            }
            return View(vrstaPitanja);
        }

        // GET: VrstaPitanja/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VrstaPitanja/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "VrstaPitanjaID,VrstaPitanja1")] VrstaPitanja vrstaPitanja)
        {
            if (ModelState.IsValid)
            {
                db.VrstaPitanja.Add(vrstaPitanja);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(vrstaPitanja);
        }

        // GET: VrstaPitanja/Edit/5
        public async Task<ActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VrstaPitanja vrstaPitanja = await db.VrstaPitanja.FindAsync(id);
            if (vrstaPitanja == null)
            {
                return HttpNotFound();
            }
            return View(vrstaPitanja);
        }

        // POST: VrstaPitanja/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "VrstaPitanjaID,VrstaPitanja1")] VrstaPitanja vrstaPitanja)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vrstaPitanja).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(vrstaPitanja);
        }

        // GET: VrstaPitanja/Delete/5
        public async Task<ActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VrstaPitanja vrstaPitanja = await db.VrstaPitanja.FindAsync(id);
            if (vrstaPitanja == null)
            {
                return HttpNotFound();
            }
            return View(vrstaPitanja);
        }

        // POST: VrstaPitanja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(short id)
        {
            VrstaPitanja vrstaPitanja = await db.VrstaPitanja.FindAsync(id);
            db.VrstaPitanja.Remove(vrstaPitanja);
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
