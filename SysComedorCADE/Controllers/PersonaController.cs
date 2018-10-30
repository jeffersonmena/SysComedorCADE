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
    public class PersonaController : Controller
    {
        private SCCADEEntities db = new SCCADEEntities();

        // GET: Persona
        public ActionResult Index()
        {
            var persona = db.Persona.Include(p => p.Entidad).Include(p => p.Genero).Include(p => p.TipoPersona);
            return View(persona.ToList());
        }

        // GET: Persona/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // GET: Persona/Create
        public ActionResult Create()
        {
            ViewBag.CodEntidad = new SelectList(db.Entidad, "CodEntidad", "NombreEntidad");
            ViewBag.CodGenero = new SelectList(db.Genero, "CodGenero", "Abrev");
            ViewBag.CodTipoPer = new SelectList(db.TipoPersona, "CodTipoPer", "Detalle");
            return View();
        }

        // POST: Persona/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodPersona,CodTipoPer,NombresCompletos,CiRuc,Telf,Cel,Dir,CodGenero,CodEntidad,Estado")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Persona.Add(persona);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodEntidad = new SelectList(db.Entidad, "CodEntidad", "NombreEntidad", persona.CodEntidad);
            ViewBag.CodGenero = new SelectList(db.Genero, "CodGenero", "Abrev", persona.CodGenero);
            ViewBag.CodTipoPer = new SelectList(db.TipoPersona, "CodTipoPer", "Detalle", persona.CodTipoPer);
            return View(persona);
        }

        // GET: Persona/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodEntidad = new SelectList(db.Entidad, "CodEntidad", "NombreEntidad", persona.CodEntidad);
            ViewBag.CodGenero = new SelectList(db.Genero, "CodGenero", "Abrev", persona.CodGenero);
            ViewBag.CodTipoPer = new SelectList(db.TipoPersona, "CodTipoPer", "Detalle", persona.CodTipoPer);
            return View(persona);
        }

        // POST: Persona/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodPersona,CodTipoPer,NombresCompletos,CiRuc,Telf,Cel,Dir,CodGenero,CodEntidad,Estado")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(persona).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodEntidad = new SelectList(db.Entidad, "CodEntidad", "NombreEntidad", persona.CodEntidad);
            ViewBag.CodGenero = new SelectList(db.Genero, "CodGenero", "Abrev", persona.CodGenero);
            ViewBag.CodTipoPer = new SelectList(db.TipoPersona, "CodTipoPer", "Detalle", persona.CodTipoPer);
            return View(persona);
        }

        // GET: Persona/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // POST: Persona/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Persona persona = db.Persona.Find(id);
            db.Persona.Remove(persona);
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
