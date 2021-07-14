using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using EsquemaDinamico.Data;

namespace EsquemaDinamico
{
    public class TablasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TablasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tablas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tablas.ToListAsync());
        }

        // GET: Tablas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tabla = await _context.Tablas
                .FirstOrDefaultAsync(m => m.Name == id);
            if (tabla == null)
            {
                return NotFound();
            }

            return View(tabla);
        }

        // GET: Tablas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tablas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Tabla tabla)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tabla);

                using (var connection = _context.Database.GetDbConnection())
                {

                    string NuevaTabla = string.Format("CREATE TABLE {0} ( \"ID\" INTEGER NOT NULL, PRIMARY KEY( \"ID\" AUTOINCREMENT))", tabla.Name);

                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = NuevaTabla;
                        var result = await command.ExecuteNonQueryAsync();
                    }

                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tabla);
        }

        // GET: Tablas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tabla = await _context.Tablas.FindAsync(id);
            if (tabla == null)
            {
                return NotFound();
            }
            return View(tabla);
        }

        // POST: Tablas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name")] Tabla tabla)
        {
            if (id != tabla.Name)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tabla);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TablaExists(tabla.Name))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tabla);
        }

        // GET: Tablas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tabla = await _context.Tablas
                .FirstOrDefaultAsync(m => m.Name == id);
            if (tabla == null)
            {
                return NotFound();
            }

            return View(tabla);
        }

        // POST: Tablas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tabla = await _context.Tablas.FindAsync(id);
            _context.Tablas.Remove(tabla);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TablaExists(string id)
        {
            return _context.Tablas.Any(e => e.Name == id);
        }
    }
}
