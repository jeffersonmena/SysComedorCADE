using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SysComedorCADE.Models;
using System.Configuration;
using System.Data.SqlClient;

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
            var fechaventa = DateTime.Now;
            if ((datosp.CiRuc != null) && (datosp.CodPersona > 0) && (des != null) && (cant >0) && (vunit> 0 ) && (tpago > 0))
            {
            
            string CS = ConfigurationManager.ConnectionStrings["SCCADE"].ConnectionString;            
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "exec GrabaVenta @anio, @CodPersona, @CodTipoPago, @Cantidad, @Detalle, @Costo, @Total, @FVenta, @usuario";
                cmd.Parameters.AddWithValue("@anio", anio);
                cmd.Parameters.AddWithValue("@CodPersona", datosp.CodPersona);
                cmd.Parameters.AddWithValue("@CodTipoPago", tpago);
                cmd.Parameters.AddWithValue("@Cantidad", cant);
                cmd.Parameters.AddWithValue("@Detalle", des);
                cmd.Parameters.AddWithValue("@Costo", vunit);
                cmd.Parameters.AddWithValue("@Total", vt);
                cmd.Parameters.AddWithValue("@FVenta", fechaventa);
                cmd.Parameters.AddWithValue("@usuario", usuVenta);

                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sms = reader.GetInt32(0);
                    }
                }
                con.Close();

            }
            }
            return Json(sms, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Cobros()
        {
            return Json(JsonRequestBehavior.AllowGet);
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
