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
    public class DespesasOrcamentariasController : Controller
    {
        private readonly GestorContratosContext _context;

        public DespesasOrcamentariasController(GestorContratosContext context)
        {
            _context = context;
        }

        // GET: DespesasOrcamentarias
        public async Task<IActionResult> Index()
        {
            var gestorContratosContext = _context.DespesasOrcamentaria.Include(d => d.Contrato);
            return View(await gestorContratosContext.ToListAsync());
        }

        // GET: DespesasOrcamentarias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DespesasOrcamentaria == null)
            {
                return NotFound();
            }

            var despesaOrcamentaria = await _context.DespesasOrcamentaria
                .Include(d => d.Contrato)
                .FirstOrDefaultAsync(m => m.DespesaId == id);
            if (despesaOrcamentaria == null)
            {
                return NotFound();
            }

            return View(despesaOrcamentaria);
        }

        // GET: DespesasOrcamentarias/Create
        public IActionResult Create()
        {
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId");
            return View();
        }

        // POST: DespesasOrcamentarias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DespesaId,Programa,Acao,Fonte,Natureza,Elemento,ContratoId")] DespesaOrcamentaria despesaOrcamentaria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(despesaOrcamentaria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId", despesaOrcamentaria.ContratoId);
            return View(despesaOrcamentaria);
        }

        // GET: DespesasOrcamentarias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DespesasOrcamentaria == null)
            {
                return NotFound();
            }

            var despesaOrcamentaria = await _context.DespesasOrcamentaria.FindAsync(id);
            if (despesaOrcamentaria == null)
            {
                return NotFound();
            }
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId", despesaOrcamentaria.ContratoId);
            return View(despesaOrcamentaria);
        }

        // POST: DespesasOrcamentarias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DespesaId,Programa,Acao,Fonte,Natureza,Elemento,ContratoId")] DespesaOrcamentaria despesaOrcamentaria)
        {
            if (id != despesaOrcamentaria.DespesaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(despesaOrcamentaria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DespesaOrcamentariaExists(despesaOrcamentaria.DespesaId))
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
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId", despesaOrcamentaria.ContratoId);
            return View(despesaOrcamentaria);
        }

        // GET: DespesasOrcamentarias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DespesasOrcamentaria == null)
            {
                return NotFound();
            }

            var despesaOrcamentaria = await _context.DespesasOrcamentaria
                .Include(d => d.Contrato)
                .FirstOrDefaultAsync(m => m.DespesaId == id);
            if (despesaOrcamentaria == null)
            {
                return NotFound();
            }

            return View(despesaOrcamentaria);
        }

        // POST: DespesasOrcamentarias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DespesasOrcamentaria == null)
            {
                return Problem("Entity set 'GestorContratosContext.DespesasOrcamentaria'  is null.");
            }
            var despesaOrcamentaria = await _context.DespesasOrcamentaria.FindAsync(id);
            if (despesaOrcamentaria != null)
            {
                _context.DespesasOrcamentaria.Remove(despesaOrcamentaria);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DespesaOrcamentariaExists(int id)
        {
          return (_context.DespesasOrcamentaria?.Any(e => e.DespesaId == id)).GetValueOrDefault();
        }
    }
}
