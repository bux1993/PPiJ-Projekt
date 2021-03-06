﻿using System;
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
    public class PitanjaController : Controller
    {
        private BazaEntities db = new BazaEntities();

        // GET: Pitanja
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Index()
        {
            var pitanja = db.Pitanja.Include(p => p.Kategorije).Include(p => p.VrstaPitanja1);
            return View(await pitanja.ToListAsync());
        }

        // GET: Pitanja/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pitanja pitanja = await db.Pitanja.FindAsync(id);
            if (pitanja == null)
            {
                return HttpNotFound();
            }
            return View(pitanja);
        }

        // GET: Pitanja/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.KategorijaID = new SelectList(db.Kategorije, "KategorijaID", "KategorijaName");
            ViewBag.VrstaPitanja = new SelectList(db.VrstaPitanja, "VrstaPitanjaID", "VrstaPitanja1");
            return View();
        }

        // POST: Pitanja/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PitanjeID,KategorijaID,VrstaPitanja,Pitanje,TocanOdgovor,KriviOdgovor,KriviOdgovor2,KriviOdgovor3,Slika")] Pitanja pitanja)
        {
            if (ModelState.IsValid)
            {
                db.Pitanja.Add(pitanja);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.KategorijaID = new SelectList(db.Kategorije, "KategorijaID", "KategorijaName", pitanja.KategorijaID);
            ViewBag.VrstaPitanja = new SelectList(db.VrstaPitanja, "VrstaPitanjaID", "VrstaPitanja1", pitanja.VrstaPitanja);
            return View(pitanja);
        }

        // GET: Pitanja/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pitanja pitanja = await db.Pitanja.FindAsync(id);
            if (pitanja == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategorijaID = new SelectList(db.Kategorije, "KategorijaID", "KategorijaName", pitanja.KategorijaID);
            ViewBag.VrstaPitanja = new SelectList(db.VrstaPitanja, "VrstaPitanjaID", "VrstaPitanja1", pitanja.VrstaPitanja);
            return View(pitanja);
        }

        // POST: Pitanja/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PitanjeID,KategorijaID,VrstaPitanja,Pitanje,TocanOdgovor,KriviOdgovor,KriviOdgovor2,KriviOdgovor3,Slika")] Pitanja pitanja)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pitanja).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.KategorijaID = new SelectList(db.Kategorije, "KategorijaID", "KategorijaName", pitanja.KategorijaID);
            ViewBag.VrstaPitanja = new SelectList(db.VrstaPitanja, "VrstaPitanjaID", "VrstaPitanja1", pitanja.VrstaPitanja);
            return View(pitanja);
        }

        // GET: Pitanja/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pitanja pitanja = await db.Pitanja.FindAsync(id);
            if (pitanja == null)
            {
                return HttpNotFound();
            }
            return View(pitanja);
        }

        // POST: Pitanja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Pitanja pitanja = await db.Pitanja.FindAsync(id);
            db.Pitanja.Remove(pitanja);
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

        // GET: Pitanja/Test       
        [Authorize()]
        public ActionResult Test(int kategorijaID = 1)
        {
            var pitanjaTest = db.Pitanja.Where(m => m.KategorijaID == kategorijaID).ToList();
            if (pitanjaTest.Count == 0)
            {
                return View("../Shared/Nedostupno");
            }

            List<Pitanja> listaPitanja = new List<Pitanja>();
            Random random = new Random();
            List<int> nadenaPitanja = new List<int>();
            int brojDozvoljenih = 
                pitanjaTest.Where(m => m.VrstaPitanja1.VrstaPitanja1.Equals("Više točnih")).ToList().Count;

            while (pitanjaTest.Count > 0)
            {
                int pitanje = random.Next() % pitanjaTest.Count;
                if (!nadenaPitanja.Contains(pitanje) && pitanjaTest.Count > pitanje &&
                    !(pitanjaTest.ElementAt(pitanje).VrstaPitanja1.VrstaPitanja1.Equals("Više točnih")))
                {
                    nadenaPitanja.Add(pitanje);
                    listaPitanja.Add(pitanjaTest.ElementAt(pitanje));
                }
                if (listaPitanja.Count == 10 || nadenaPitanja.Count == pitanjaTest.Count - brojDozvoljenih)
                {
                    break;
                }

            }

            TestViewModel tvm = new TestViewModel();
            tvm.pitanja = listaPitanja;
            tvm.KategorijaID = kategorijaID;
            tvm.generirajDummyOdgovore();

            return View(tvm);
        }

        // POST: Pitanja/Test
        [HttpPost]
        [ValidateAntiForgeryTokenAttribute]
        public ActionResult Test(TestViewModel model)
        {
            TempData["rezultati"] = model;
            return RedirectToAction("Evaluate");
        }

        [Authorize()]
        public ActionResult Talijan()
        {
            return View();
        }

        [Authorize()]
        public ActionResult Evaluate()
        {
            TestViewModel tvm = (TestViewModel) TempData["rezultati"];
            return View(tvm);
        }


        public ActionResult Provjereno()
        {
            return View();
        }
    }
}
