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
    public class PrestamosController : Controller
    {
        private BaseDeDatos db = new BaseDeDatos();

        // GET: Prestamos
        public async Task<ActionResult> Index()
        {
            return View(await db.prestamos.ToListAsync());
        }

        // GET: Prestamos/Details/5
        public async Task<ActionResult> Details(int? id)
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

        // GET: Prestamos/Create
        public ActionResult Create(string cedula = null)
        {
            Prestamos prest = new Prestamos();
            prest.Cedula = cedula;
            return View(prest);
        }

        // POST: Prestamos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Cantidad,Pagos,Motivo,Cedula")] Prestamos prestamos)
        {
            if (ModelState.IsValid)
            {
                db.prestamos.Add(prestamos);
                await db.SaveChangesAsync();

                Amortizacion amor = new Amortizacion();
                double interesTaza = 0.13;
                double Cantidades = Convert.ToDouble(prestamos.Cantidad);
                amor.Cuota = (interesTaza * Cantidades) / (1 - (Math.Pow((1 + (interesTaza)), -prestamos.Pagos)));
                for (int x = 0; x < prestamos.Pagos; x++)
                {

                    amor.montoInicial = Cantidades;

                    amor.Pagos = x + 1;
                    double interes2 = (interesTaza) * Cantidades;
                    double amortizacion = amor.Cuota - interes2;
                    double montoFinal = Cantidades - amortizacion;
                    Cantidades = montoFinal;
                    amor.Interes = interes2;
                    amor.Amort = amortizacion;
                    amor.Cedula = prestamos.Cedula;
                    amor.Idref = prestamos.Id;

                    if (x == prestamos.Pagos - 1)
                    {
                        amor.montoFinal = 0;
                    }
                    else
                    {
                        amor.montoFinal = montoFinal;
                    }

                    db.amortizacion.Add(amor);
                    await db.SaveChangesAsync();
                }
                 return RedirectToAction("Index");
            }

            return View(prestamos);
        }

        // GET: Prestamos/Edit/5
        public async Task<ActionResult> Edit(int? id)
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

        // POST: Prestamos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Cantidad,Pagos,Motivo,Cedula")] Prestamos prestamos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prestamos).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(prestamos);
        }

        // GET: Prestamos/Delete/5
        public async Task<ActionResult> Delete(int? id)
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

        // POST: Prestamos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Prestamos prestamos = await db.prestamos.FindAsync(id);
            db.prestamos.Remove(prestamos);
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
    }
}
