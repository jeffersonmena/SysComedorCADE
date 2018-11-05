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
    public class VentaController : Controller
    {
        private SCCADEEntities db = new SCCADEEntities();

        // GET: Venta
        public ActionResult Index()
        {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }

            var venta = db.Venta.Include(v => v.Persona).Include(v => v.TipoPago);
            return View(venta.ToList());
        }

        // GET: Venta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Venta.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }

        // GET: Venta/Create
        public ActionResult Create()
        {
            ViewBag.CodPersona = new SelectList(db.Persona, "CodPersona", "NombresCompletos");
            ViewBag.CodTipoPago = new SelectList(db.TipoPago, "CodTipoPago", "DetTipoPago");
            return View();
        }

        public JsonResult GuardaVenta(int cant, string des, decimal vunit, decimal vt, int tpago, PersonaModel datosp)
        {
            var sms = 0;

            var anio = Convert.ToInt32(Session["Gestion"]);
            var usuVenta = Convert.ToString(Session["usuario"]);
            //fecha ya vá por default e la bd
            Venta vm = new Venta();
            vm.anio = anio;
            vm.CodPersona = datosp.CodPersona;
            vm.CodTipoPago = tpago;
            vm.Cantidad = cant;
            vm.Detalle = des;
            vm.Costo = vunit;
            vm.Total = vt;
            vm.FVenta = DateTime.Now;
            vm.usuario = usuVenta;
            db.Venta.Add(vm);
            db.SaveChanges();

            /*TIPO PAGO ::  CUENTAS*/
            /*Solo cuando sea una venta que pase a la CUENTA y no al Contado */
            if (vm.CodTipoPago == 1)
            {
                var venta = (from v in db.Venta
                             where v.CodPersona == vm.CodPersona
                             && v.CodTipoPago == vm.CodTipoPago
                             && v.anio == vm.anio
                             && v.usuario == vm.usuario
                             select v).FirstOrDefault();

                EstadoCuentaPersona ecp = new EstadoCuentaPersona();
                ecp.anio = venta.anio;
                ecp.CodVenta = venta.CodVenta;
                ecp.CodPersona = venta.CodPersona;
                //guarda 1 en pagos cuando es deuda Y el valor con positivo
                ecp.pagos = 1;
                ecp.Valor = venta.Total;
                ecp.FRegistro = venta.FVenta;
                ecp.usuario = venta.usuario;
                db.EstadoCuentaPersona.Add(ecp);
                db.SaveChanges();
                sms = 1;
            }
            else {

                /*Solo cuando sea una venta al Contado */
                sms = 2;
            }
            return Json(sms, JsonRequestBehavior.AllowGet);
        }



        // POST: Venta/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodVenta,CodPersona,CodTipoPago,Cantidad,Detalle,Costo,Total,FVenta,anio")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                db.Venta.Add(venta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodPersona = new SelectList(db.Persona, "CodPersona", "NombresCompletos", venta.CodPersona);
            ViewBag.CodTipoPago = new SelectList(db.TipoPago, "CodTipoPago", "DetTipoPago", venta.CodTipoPago);
            return View(venta);
        }

        // GET: Venta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Venta.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodPersona = new SelectList(db.Persona, "CodPersona", "NombresCompletos", venta.CodPersona);
            ViewBag.CodTipoPago = new SelectList(db.TipoPago, "CodTipoPago", "DetTipoPago", venta.CodTipoPago);
            return View(venta);
        }

        // POST: Venta/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodVenta,CodPersona,CodTipoPago,Cantidad,Detalle,Costo,Total,FVenta,anio")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodPersona = new SelectList(db.Persona, "CodPersona", "NombresCompletos", venta.CodPersona);
            ViewBag.CodTipoPago = new SelectList(db.TipoPago, "CodTipoPago", "DetTipoPago", venta.CodTipoPago);
            return View(venta);
        }

        // GET: Venta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Venta.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }

        // POST: Venta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Venta venta = db.Venta.Find(id);
            db.Venta.Remove(venta);
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
