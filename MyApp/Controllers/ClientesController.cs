using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyApp.Models;

namespace MyApp.Controllers
{
    public class ClientesController : Controller
    {
        private BaseDeDatos db = new BaseDeDatos();

        // GET: Clientes
        public async Task<ActionResult> Index()
        {
            return View(await db.clientes.ToListAsync());
        }

        // GET: Clientes/Prestamos/Cedula
        public async Task<ActionResult> Prestamos(String cedula)
        {
            IEnumerable<Prestamos> prestamos;
            if (cedula == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

             prestamos = await db.prestamos.Where(z => z.Cedula == cedula).ToListAsync();
            if (prestamos == null)
            {
                return HttpNotFound();
            }
            return View(prestamos);
        }

        public async Task<ActionResult> Informacion(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamos prestamos = await db.prestamos.FindAsync(id);
            if (prestamos == null)
            {
                return HttpNotFound();
            }
            return View(prestamos);
        }
        [HttpGet]
        public async Task<ActionResult> Amortizacion(int id,int Cantidad, int pagos, String cedula)
        {

            IEnumerable<Amortizacion> Amort;
            if (cedula == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Amort = await db.amortizacion.Where(z => z.Idref == id).ToListAsync();
            if (Amort == null)
            {
                return HttpNotFound();
            }
            return View(Amort);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nombre,Apellido,Nacimiento,Cedula")] Clientes clientes)
        {
            if (ModelState.IsValid)
            {
                db.clientes.Add(clientes);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(clientes);
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
