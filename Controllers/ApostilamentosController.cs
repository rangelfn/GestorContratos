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
    public class ApostilamentosController : Controller
    {
        private readonly GestorContratosContext _context;

        public ApostilamentosController(GestorContratosContext context)
        {
            _context = context;
        }

        // GET: Apostilamentos
        public async Task<IActionResult> Index()
        {
            var gestorContratosContext = _context.Apostilamentos.Include(a => a.Contrato);
            return View(await gestorContratosContext.ToListAsync());
        }

        // GET: Apostilamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Apostilamentos == null)
            {
                return NotFound();
            }

            var apostilamento = await _context.Apostilamentos
                .Include(a => a.Contrato)
                .FirstOrDefaultAsync(m => m.ApostilamentoId == id);
            if (apostilamento == null)
            {
                return NotFound();
            }

            return View(apostilamento);
        }

        // GET: Apostilamentos/Create
        public IActionResult Create()
        {
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId");
            return View();
        }

        // POST: Apostilamentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApostilamentoId,Numero,Descricao,DataInicio,DataFim,Valor,ContratoId")] Apostilamento apostilamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(apostilamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId", apostilamento.ContratoId);
            return View(apostilamento);
        }

        // GET: Apostilamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Apostilamentos == null)
            {
                return NotFound();
            }

            var apostilamento = await _context.Apostilamentos.FindAsync(id);
            if (apostilamento == null)
            {
                return NotFound();
            }
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId", apostilamento.ContratoId);
            return View(apostilamento);
        }

        // POST: Apostilamentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApostilamentoId,Numero,Descricao,DataInicio,DataFim,Valor,ContratoId")] Apostilamento apostilamento)
        {
            if (id != apostilamento.ApostilamentoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(apostilamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApostilamentoExists(apostilamento.ApostilamentoId))
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
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId", apostilamento.ContratoId);
            return View(apostilamento);
        }

        // GET: Apostilamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Apostilamentos == null)
            {
                return NotFound();
            }

            var apostilamento = await _context.Apostilamentos
                .Include(a => a.Contrato)
                .FirstOrDefaultAsync(m => m.ApostilamentoId == id);
            if (apostilamento == null)
            {
                return NotFound();
            }

            return View(apostilamento);
        }

        // POST: Apostilamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Apostilamentos == null)
            {
                return Problem("Entity set 'GestorContratosContext.Apostilamentos'  is null.");
            }
            var apostilamento = await _context.Apostilamentos.FindAsync(id);
            if (apostilamento != null)
            {
                _context.Apostilamentos.Remove(apostilamento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApostilamentoExists(int id)
        {
          return (_context.Apostilamentos?.Any(e => e.ApostilamentoId == id)).GetValueOrDefault();
        }
    }
}
