using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestorContratos.Models;

namespace GestorContratos.Controllers
{
    public class EditaisController : Controller
    {
        private readonly GestorContratosContext _context;

        public EditaisController(GestorContratosContext context)
        {
            _context = context;
        }

        // GET: Editais
        public async Task<IActionResult> Index()
        {
            var gestorContratosContext = _context.Editais.Include(e => e.Contrato);
            return View(await gestorContratosContext.ToListAsync());
        }

        // GET: Editais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Editais == null)
            {
                return NotFound();
            }

            var edital = await _context.Editais
                .Include(e => e.Contrato)
                .FirstOrDefaultAsync(m => m.EditalId == id);
            if (edital == null)
            {
                return NotFound();
            }

            return View(edital);
        }

        // GET: Editais/Create
        public IActionResult Create()
        {
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId");
            return View();
        }

        // POST: Editais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EditalId,Numero,EditalTipo,LinkPublico,DataEdital,ContratoId")] Edital edital)
        {
            if (ModelState.IsValid)
            {
                _context.Add(edital);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId", edital.ContratoId);
            return View(edital);
        }

        // GET: Editais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Editais == null)
            {
                return NotFound();
            }

            var edital = await _context.Editais.FindAsync(id);
            if (edital == null)
            {
                return NotFound();
            }
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId", edital.ContratoId);
            return View(edital);
        }

        // POST: Editais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EditalId,Numero,EditalTipo,LinkPublico,DataEdital,ContratoId")] Edital edital)
        {
            if (id != edital.EditalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(edital);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EditalExists(edital.EditalId))
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
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId", edital.ContratoId);
            return View(edital);
        }

        // GET: Editais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Editais == null)
            {
                return NotFound();
            }

            var edital = await _context.Editais
                .Include(e => e.Contrato)
                .FirstOrDefaultAsync(m => m.EditalId == id);
            if (edital == null)
            {
                return NotFound();
            }

            return View(edital);
        }

        // POST: Editais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Editais == null)
            {
                return Problem("Entity set 'GestorContratosContext.Editais'  is null.");
            }
            var edital = await _context.Editais.FindAsync(id);
            if (edital != null)
            {
                _context.Editais.Remove(edital);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EditalExists(int id)
        {
          return (_context.Editais?.Any(e => e.EditalId == id)).GetValueOrDefault();
        }
    }
}
