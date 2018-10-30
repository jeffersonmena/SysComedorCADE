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
    public class CostosAlimentacionController : Controller
    {
        private SCCADEEntities db = new SCCADEEntities();

        // GET: CostosAlimentacion
        public ActionResult Index()
        {
            return View(db.CostosAlimentacion.ToList());
        }

        // GET: CostosAlimentacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CostosAlimentacion costosAlimentacion = db.CostosAlimentacion.Find(id);
            if (costosAlimentacion == null)
            {
                return HttpNotFound();
            }
            return View(costosAlimentacion);
        }

        // GET: CostosAlimentacion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CostosAlimentacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodCosto,Detalle,Valor,FRegistro,Estado,anio")] CostosAlimentacion costosAlimentacion)
        {
            if (ModelState.IsValid)
            {
                db.CostosAlimentacion.Add(costosAlimentacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(costosAlimentacion);
        }

        // GET: CostosAlimentacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CostosAlimentacion costosAlimentacion = db.CostosAlimentacion.Find(id);
            if (costosAlimentacion == null)
            {
                return HttpNotFound();
            }
            return View(costosAlimentacion);
        }

        // POST: CostosAlimentacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodCosto,Detalle,Valor,FRegistro,Estado,anio")] CostosAlimentacion costosAlimentacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(costosAlimentacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(costosAlimentacion);
        }

        // GET: CostosAlimentacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CostosAlimentacion costosAlimentacion = db.CostosAlimentacion.Find(id);
            if (costosAlimentacion == null)
            {
                return HttpNotFound();
            }
            return View(costosAlimentacion);
        }

        // POST: CostosAlimentacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CostosAlimentacion costosAlimentacion = db.CostosAlimentacion.Find(id);
            db.CostosAlimentacion.Remove(costosAlimentacion);
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
