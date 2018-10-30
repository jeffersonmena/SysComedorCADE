using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SysComedorCADE.Models;

namespace SysComedorCADE.Controllers
{
    public class TipoPagoController : Controller
    {
        private SCCADEEntities db = new SCCADEEntities();

        // GET: TipoPago
        public ActionResult Index()
        {
            return View(db.TipoPago.ToList());
        }

        // GET: TipoPago/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPago tipoPago = db.TipoPago.Find(id);
            if (tipoPago == null)
            {
                return HttpNotFound();
            }
            return View(tipoPago);
        }

        // GET: TipoPago/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoPago/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodTipoPago,DetTipoPago")] TipoPago tipoPago)
        {
            if (ModelState.IsValid)
            {
                db.TipoPago.Add(tipoPago);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoPago);
        }

        // GET: TipoPago/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPago tipoPago = db.TipoPago.Find(id);
            if (tipoPago == null)
            {
                return HttpNotFound();
            }
            return View(tipoPago);
        }

        // POST: TipoPago/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodTipoPago,DetTipoPago")] TipoPago tipoPago)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoPago).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoPago);
        }

        // GET: TipoPago/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPago tipoPago = db.TipoPago.Find(id);
            if (tipoPago == null)
            {
                return HttpNotFound();
            }
            return View(tipoPago);
        }

        // POST: TipoPago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoPago tipoPago = db.TipoPago.Find(id);
            db.TipoPago.Remove(tipoPago);
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
