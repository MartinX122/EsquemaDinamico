using EsquemaDinamico.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using EsquemaDinamico.Models;

namespace EsquemaDinamico.Controllers
{

    [Route("Tablas/Esquema/{table}/{action=Index}/{id?}")]
    public class EsquemaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EsquemaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EsquemaController
        public async Task<ActionResult> Index(string table)
        {

            ViewData["ALLColums"] = _context.Campos.Where(cp => cp.NombreTabla == table).ToList();

            List<string> ColumsNames = ((List<Campos>)ViewData["ALLColums"]).Select(CN => CN.Nombre).ToList();

            SqlRows Rows = new SqlRows();

            using (var connection = _context.Database.GetDbConnection())
            {

                ColumsNames.Add("ID");
                string consulta = string.Format("SELECT {0} FROM {1} ;", string.Join(",",ColumsNames), table);

                await connection.OpenAsync();

                var command = connection.CreateCommand();

                command.CommandText = consulta;
                command.CommandType = CommandType.Text;

                Rows = await SqlRows.GetColumnsAsync(command, (List<Campos>)ViewData["ALLColums"]);

            }

            ViewData["Rows"] = Rows;

            return View();
        }

        // GET: EsquemaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EsquemaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EsquemaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EsquemaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EsquemaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EsquemaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EsquemaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

         

    }
}
