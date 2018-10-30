using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SysComedorCADE.Models;

using System.IO;

using System.Web.Routing;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;


namespace SysComedorCADE.Controllers
{
    public class UsuariosController : Controller
    {
        private SCCADEEntities db = new SCCADEEntities();

        // GET: Usuarios
        public ActionResult Index()
        {
            var usuarios = db.Usuarios.Include(u => u.Persona);
            return View(usuarios.ToList());
        }

        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login(Usuarios datos)
        {

            if (ModelState.IsValid)
            {
                if (valida(datos.Usuario,datos.Clave))
                {
                    string val = "1";
                    HttpCookie cookie = new HttpCookie("cookieSCCADE", val);
                    ControllerContext.HttpContext.Response.SetCookie(cookie);
                    cookie.Expires = DateTime.Now.AddMinutes(60);

                    FormsAuthentication.SetAuthCookie(datos.Usuario, false);

                    string usu = datos.Usuario;
                    string pss = datos.Clave;
                    var userlog = (from u in db.Usuarios 
                                    where u.Usuario==usu && u.Clave ==pss && u.Estado ==true
                                    select u).FirstOrDefault();
                    var anio = DateTime.Now;

                    Session["politica"] = userlog.politica;
                    

                    if (userlog.politica == 1)
                    {
                        Session["TipoUsuario"] = "Administrador";
                        Session["Gestion"] = anio;
                        Session["usuario"] = userlog.Usuario;
                        Session["IdpersonaUsuario"] = userlog.CodPersona;
                    }

                    if (userlog.politica == 2)
                    {
                        Session["TipoUsuario"] = "ayudante";
                        Session["Gestion"] = anio;
                        Session["usuario"] = userlog.Usuario;
                        Session["IdpersonaUsuario"] = userlog.CodPersona;

                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("1", "Usuario Incorrecto.");

                }
         

            }
            return View();
        }

        private bool valida(string x, string y)
        {
            bool valida = false;
            var user = (from us in db.Usuarios
                           where us.Usuario.Equals(x) && us.Clave.Equals(y)
                           select us).FirstOrDefault();

            if (user != null)
            {
                if (user.Clave == y) //crypto.Compute(password, usuario.PASSWORD))
                {
                    valida = true;

                }
            }
            return valida;
        }

        public ActionResult CerrarSesiones()
        {
            FormsAuthentication.SignOut();

            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            Session["TipoUsuario"] = null;
            Session["Gestion"] = null;
            Session["usuario"] = null;
            Session["IdpersonaUsuario"] = null;


            return RedirectToAction("Login", "Usuarios");
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.CodPersona = new SelectList(db.Persona, "CodPersona", "NombresCompletos");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodUsuario,CodPersona,Usuario,Clave,FRegistro,Estado")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodPersona = new SelectList(db.Persona, "CodPersona", "NombresCompletos", usuarios.CodPersona);
            return View(usuarios);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodPersona = new SelectList(db.Persona, "CodPersona", "NombresCompletos", usuarios.CodPersona);
            return View(usuarios);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodUsuario,CodPersona,Usuario,Clave,FRegistro,Estado")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodPersona = new SelectList(db.Persona, "CodPersona", "NombresCompletos", usuarios.CodPersona);
            return View(usuarios);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuarios usuarios = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuarios);
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
