using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EsquemaDinamico.Data;

namespace EsquemaDinamico
{
    public class CamposController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CamposController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Campos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Campos.Include(c => c.Tabla);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Campos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campos = await _context.Campos
                .Include(c => c.Tabla)
                .FirstOrDefaultAsync(m => m.id == id);
            if (campos == null)
            {
                return NotFound();
            }

            return View(campos);
        }

        // GET: Campos/Create
        public IActionResult Create()
        {
            ViewData["NombreTabla"] = new SelectList(_context.Tablas, "Name", "Name");





            return View();
        }

        // POST: Campos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,NombreTabla,Nombre,Desc,TipoID")] Campos campos)
        {
            if (ModelState.IsValid)
            {

                using (var connection = _context.Database.GetDbConnection())
                {

                    string NuevaTabla = string.Format("ALTER TABLE {0} ADD COLUMN {1} {2} ;", campos.NombreTabla,campos.Nombre,Campos.GetTipoKey(campos.Tipo));

                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = NuevaTabla;
                        var result = await command.ExecuteNonQueryAsync();
                    }

                }


                _context.Add(campos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NombreTabla"] = new SelectList(_context.Tablas, "Name", "Name", campos.NombreTabla);
            return View(campos);
        }

        // GET: Campos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campos = await _context.Campos.FindAsync(id);
            if (campos == null)
            {
                return NotFound();
            }
            ViewData["NombreTabla"] = new SelectList(_context.Tablas, "Name", "Name", campos.NombreTabla);
            return View(campos);
        }

        // POST: Campos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Campos campos)
        {
            if (id != campos.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    _context.Update(campos);

                    using (var connection = _context.Database.GetDbConnection())
                    {

                        string NuevaTabla = string.Format("ALTER TABLE {0} RENAME COLUMN {1} TO {2} ;", campos.NombreTabla, campos.NombreAnterior, campos.Nombre);



                        await connection.OpenAsync();
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = NuevaTabla;
                            var result = await command.ExecuteNonQueryAsync();
                        }

                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CamposExists(campos.id))
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
            ViewData["NombreTabla"] = new SelectList(_context.Tablas, "Name", "Name", campos.NombreTabla);
            return View(campos);
        }

        // GET: Campos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campos = await _context.Campos
                .Include(c => c.Tabla)
                .FirstOrDefaultAsync(m => m.id == id);
            if (campos == null)
            {
                return NotFound();
            }

            return View(campos);
        }

        // POST: Campos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var campos = await _context.Campos.FindAsync(id);

            _context.Campos.Remove(campos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CamposExists(int id)
        {
            return _context.Campos.Any(e => e.id == id);
        }
    }
}
