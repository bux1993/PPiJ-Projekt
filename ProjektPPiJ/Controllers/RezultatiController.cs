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
    public class RezultatiController : Controller
    {
        private BazaEntities db = new BazaEntities();

        // GET: Rezultati
        public async Task<ActionResult> Index()
        {
            var rezultati = db.Rezultati.Include(r => r.Kategorije).Include(r => r.UserInfo);
            return View(await rezultati.ToListAsync());
        }

        // GET: Rezultati/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rezultati rezultati = await db.Rezultati.FindAsync(id);
            if (rezultati == null)
            {
                return HttpNotFound();
            }
            return View(rezultati);
        }

        // GET: Rezultati/Create
        public ActionResult Create()
        {
            ViewBag.KategorijaID = new SelectList(db.Kategorije, "KategorijaID", "KategorijaName");
            ViewBag.UserID = new SelectList(db.UserInfo, "UserID", "Username");
            return View();
        }

        // POST: Rezultati/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RezultatID,UserID,KategorijaID,NajboljiRezultat,ZadnjiRezultat")] Rezultati rezultati)
        {
            if (ModelState.IsValid)
            {
                db.Rezultati.Add(rezultati);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.KategorijaID = new SelectList(db.Kategorije, "KategorijaID", "KategorijaName", rezultati.KategorijaID);
            ViewBag.UserID = new SelectList(db.UserInfo, "UserID", "Username", rezultati.UserID);
            return View(rezultati);
        }

        // GET: Rezultati/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rezultati rezultati = await db.Rezultati.FindAsync(id);
            if (rezultati == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategorijaID = new SelectList(db.Kategorije, "KategorijaID", "KategorijaName", rezultati.KategorijaID);
            ViewBag.UserID = new SelectList(db.UserInfo, "UserID", "Username", rezultati.UserID);
            return View(rezultati);
        }

        // POST: Rezultati/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RezultatID,UserID,KategorijaID,NajboljiRezultat,ZadnjiRezultat")] Rezultati rezultati)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rezultati).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.KategorijaID = new SelectList(db.Kategorije, "KategorijaID", "KategorijaName", rezultati.KategorijaID);
            ViewBag.UserID = new SelectList(db.UserInfo, "UserID", "Username", rezultati.UserID);
            return View(rezultati);
        }

        // GET: Rezultati/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rezultati rezultati = await db.Rezultati.FindAsync(id);
            if (rezultati == null)
            {
                return HttpNotFound();
            }
            return View(rezultati);
        }

        // POST: Rezultati/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Rezultati rezultati = await db.Rezultati.FindAsync(id);
            db.Rezultati.Remove(rezultati);
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

        public ActionResult UpdateRezultata(int kategorijaID, string username, 
            bool zasluzio, bool nula, double rezultatVrijednost)
        {
            var rezultati = db.Rezultati.ToList();
            var useri = db.UserInfo.ToList(); HttpNotFound();
            bool noviUnos = true;
            foreach (var rezultat in rezultati)
            {
                if (rezultat.KategorijaID == kategorijaID && rezultat.UserInfo.Username.Equals(username))
                {
                    noviUnos = false;
                    rezultat.ZadnjiRezultat = dajRezultat(rezultatVrijednost);
                    if (rezultat.ZadnjiRezultat > rezultat.NajboljiRezultat)
                    {
                        rezultat.NajboljiRezultat = rezultat.ZadnjiRezultat;
                    }
                    break;
                }
            }
            if (noviUnos)
            {
                var pomoc = useri.Where(m => m.Username.Equals(username)).ToList();
                Rezultati novi = new Rezultati()
                {
                    RezultatID = rezultati.Max(m => m.RezultatID) + 1,
                    NajboljiRezultat = dajRezultat(rezultatVrijednost),
                    ZadnjiRezultat = dajRezultat(rezultatVrijednost),
                    UserID = pomoc[0].UserID,
                    KategorijaID = kategorijaID
                };
                db.Rezultati.Add(novi);
            }
            db.SaveChanges();
            if (zasluzio)
            {
                return RedirectToAction("DajAchievement", "Achievements", 
                    new { username = username, kategorijaID = kategorijaID});
            }
            else if (nula)
            {
                return RedirectToAction("DajAchievement", "Achievements", 
                    new { username = username, kategorijaID = 16});
            }
            return RedirectToAction("Index", "Home");
        }

        private static int dajRezultat(double rezultatVrijednost)
        {
            return Convert.ToInt32(rezultatVrijednost);
        }


        public ActionResult RangLista()
        {
            var rezultati = db.Rezultati.ToList();
            var kategorije = db.Kategorije.ToList();
            List<RangListaViewModel> povratno = new List<RangListaViewModel>();
            foreach (var kategorija in kategorije)
            {
                RangListaViewModel rangLista = new RangListaViewModel()
                {
                    Poredak = new List<Uspjeh>(),
                    KategorijaName = kategorija.KategorijaName
                };
                List<Uspjeh> poredak = new List<Uspjeh>();
                foreach (var rezultat in rezultati)
                {
                    if (rezultat.KategorijaID.Equals(kategorija.KategorijaID))
                    {
                        Uspjeh novi = new Uspjeh()
                        {
                            Username = rezultat.UserInfo.Username,
                            NajboljiRezultat = rezultat.NajboljiRezultat
                        };
                        poredak.Add(novi);
                    }   
                }
                rangLista.Poredak = poredak.OrderByDescending(m => m.NajboljiRezultat).Take(10).ToList();
                povratno.Add(rangLista);
            }
            return View(povratno);
        }

    }
}
