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
    public class TipoPersonaController : Controller
    {
        private SCCADEEntities db = new SCCADEEntities();

        // GET: TipoPersona
        public ActionResult Index()
        {
            return View(db.TipoPersona.ToList());
        }

        // GET: TipoPersona/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPersona tipoPersona = db.TipoPersona.Find(id);
            if (tipoPersona == null)
            {
                return HttpNotFound();
            }
            return View(tipoPersona);
        }

        // GET: TipoPersona/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoPersona/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodTipoPer,Detalle")] TipoPersona tipoPersona)
        {
            if (ModelState.IsValid)
            {
                db.TipoPersona.Add(tipoPersona);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoPersona);
        }

        // GET: TipoPersona/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPersona tipoPersona = db.TipoPersona.Find(id);
            if (tipoPersona == null)
            {
                return HttpNotFound();
            }
            return View(tipoPersona);
        }

        // POST: TipoPersona/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodTipoPer,Detalle")] TipoPersona tipoPersona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoPersona).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoPersona);
        }

        // GET: TipoPersona/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPersona tipoPersona = db.TipoPersona.Find(id);
            if (tipoPersona == null)
            {
                return HttpNotFound();
            }
            return View(tipoPersona);
        }

        // POST: TipoPersona/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoPersona tipoPersona = db.TipoPersona.Find(id);
            db.TipoPersona.Remove(tipoPersona);
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
