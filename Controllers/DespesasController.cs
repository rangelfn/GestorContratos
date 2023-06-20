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
    public class DespesasController : Controller
    {
        private readonly GestorContratosContext _context;

        public DespesasController(GestorContratosContext context)
        {
            _context = context;
        }

        // GET: Despesas
        public async Task<IActionResult> Index()
        {
            var gestorContratosContext = _context.Despesas.Include(d => d.Contrato);
            return View(await gestorContratosContext.ToListAsync());
        }

        // GET: Despesas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Despesas == null)
            {
                return NotFound();
            }

            var despesa = await _context.Despesas
                .Include(d => d.Contrato)
                .FirstOrDefaultAsync(m => m.DespesaId == id);
            if (despesa == null)
            {
                return NotFound();
            }

            return View(despesa);
        }

        // GET: Despesas/Create
        public IActionResult Create()
        {
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId");
            return View();
        }

        // POST: Despesas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DespesaId,Programa,Acao,Fonte,Natureza,Elemento,ContratoId")] Despesa despesa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(despesa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId", despesa.ContratoId);
            return View(despesa);
        }

        // GET: Despesas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Despesas == null)
            {
                return NotFound();
            }

            var despesa = await _context.Despesas.FindAsync(id);
            if (despesa == null)
            {
                return NotFound();
            }
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId", despesa.ContratoId);
            return View(despesa);
        }

        // POST: Despesas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DespesaId,Programa,Acao,Fonte,Natureza,Elemento,ContratoId")] Despesa despesa)
        {
            if (id != despesa.DespesaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(despesa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DespesaExists(despesa.DespesaId))
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
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId", despesa.ContratoId);
            return View(despesa);
        }

        // GET: Despesas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Despesas == null)
            {
                return NotFound();
            }

            var despesa = await _context.Despesas
                .Include(d => d.Contrato)
                .FirstOrDefaultAsync(m => m.DespesaId == id);
            if (despesa == null)
            {
                return NotFound();
            }

            return View(despesa);
        }

        // POST: Despesas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Despesas == null)
            {
                return Problem("Entity set 'GestorContratosContext.Despesas'  is null.");
            }
            var despesa = await _context.Despesas.FindAsync(id);
            if (despesa != null)
            {
                _context.Despesas.Remove(despesa);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DespesaExists(int id)
        {
          return (_context.Despesas?.Any(e => e.DespesaId == id)).GetValueOrDefault();
        }
    }
}
