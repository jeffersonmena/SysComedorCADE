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
    public class EstadoCuentaPersonaController : Controller
    {
        private SCCADEEntities db = new SCCADEEntities();

        // GET: EstadoCuentaPersona
        public ActionResult Index()
        {
            var estadoCuentaPersona = db.EstadoCuentaPersona.Include(e => e.Persona).Include(e => e.Venta);
            return View(estadoCuentaPersona.ToList());
        }

        // GET: EstadoCuentaPersona/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoCuentaPersona estadoCuentaPersona = db.EstadoCuentaPersona.Find(id);
            if (estadoCuentaPersona == null)
            {
                return HttpNotFound();
            }
            return View(estadoCuentaPersona);
        }

        // GET: EstadoCuentaPersona/Create
        public ActionResult Create()
        {
            ViewBag.CodPersona = new SelectList(db.Persona, "CodPersona", "NombresCompletos");
            ViewBag.CodVenta = new SelectList(db.Venta, "CodVenta", "Detalle");
            return View();
        }

        // POST: EstadoCuentaPersona/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodEstadoCta,CodVenta,CodPersona,pagos,Valor,FRegistro,anio")] EstadoCuentaPersona estadoCuentaPersona)
        {
            if (ModelState.IsValid)
            {
                db.EstadoCuentaPersona.Add(estadoCuentaPersona);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodPersona = new SelectList(db.Persona, "CodPersona", "NombresCompletos", estadoCuentaPersona.CodPersona);
            ViewBag.CodVenta = new SelectList(db.Venta, "CodVenta", "Detalle", estadoCuentaPersona.CodVenta);
            return View(estadoCuentaPersona);
        }

        // GET: EstadoCuentaPersona/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoCuentaPersona estadoCuentaPersona = db.EstadoCuentaPersona.Find(id);
            if (estadoCuentaPersona == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodPersona = new SelectList(db.Persona, "CodPersona", "NombresCompletos", estadoCuentaPersona.CodPersona);
            ViewBag.CodVenta = new SelectList(db.Venta, "CodVenta", "Detalle", estadoCuentaPersona.CodVenta);
            return View(estadoCuentaPersona);
        }

        // POST: EstadoCuentaPersona/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodEstadoCta,CodVenta,CodPersona,pagos,Valor,FRegistro,anio")] EstadoCuentaPersona estadoCuentaPersona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estadoCuentaPersona).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodPersona = new SelectList(db.Persona, "CodPersona", "NombresCompletos", estadoCuentaPersona.CodPersona);
            ViewBag.CodVenta = new SelectList(db.Venta, "CodVenta", "Detalle", estadoCuentaPersona.CodVenta);
            return View(estadoCuentaPersona);
        }

        // GET: EstadoCuentaPersona/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoCuentaPersona estadoCuentaPersona = db.EstadoCuentaPersona.Find(id);
            if (estadoCuentaPersona == null)
            {
                return HttpNotFound();
            }
            return View(estadoCuentaPersona);
        }

        // POST: EstadoCuentaPersona/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EstadoCuentaPersona estadoCuentaPersona = db.EstadoCuentaPersona.Find(id);
            db.EstadoCuentaPersona.Remove(estadoCuentaPersona);
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
