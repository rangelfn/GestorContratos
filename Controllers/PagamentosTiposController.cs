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
    public class PagamentosTiposController : Controller
    {
        private readonly GestorContratosContext _context;

        public PagamentosTiposController(GestorContratosContext context)
        {
            _context = context;
        }

        // GET: PagamentosTipoes
        public async Task<IActionResult> Index()
        {
            var gestorContratosContext = _context.PagamentosTipos.Include(p => p.Contrato).Include(p => p.Pagamento);
            return View(await gestorContratosContext.ToListAsync());
        }

        // GET: PagamentosTipoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PagamentosTipos == null)
            {
                return NotFound();
            }

            var pagamentosTipo = await _context.PagamentosTipos
                .Include(p => p.Contrato)
                .Include(p => p.Pagamento)
                .FirstOrDefaultAsync(m => m.PagamentosTipo1 == id);
            if (pagamentosTipo == null)
            {
                return NotFound();
            }

            return View(pagamentosTipo);
        }

        // GET: PagamentosTipoes/Create
        public IActionResult Create()
        {
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId");
            ViewData["PagamentoId"] = new SelectList(_context.Pagamentos, "PagamentoId", "PagamentoId");
            return View();
        }

        // POST: PagamentosTipoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PagamentosTipo1,NotaEmpenho,Tipo,DataCadastro,ContratoId,PagamentoId")] PagamentosTipo pagamentosTipo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pagamentosTipo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId", pagamentosTipo.ContratoId);
            ViewData["PagamentoId"] = new SelectList(_context.Pagamentos, "PagamentoId", "PagamentoId", pagamentosTipo.PagamentoId);
            return View(pagamentosTipo);
        }

        // GET: PagamentosTipoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PagamentosTipos == null)
            {
                return NotFound();
            }

            var pagamentosTipo = await _context.PagamentosTipos.FindAsync(id);
            if (pagamentosTipo == null)
            {
                return NotFound();
            }
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId", pagamentosTipo.ContratoId);
            ViewData["PagamentoId"] = new SelectList(_context.Pagamentos, "PagamentoId", "PagamentoId", pagamentosTipo.PagamentoId);
            return View(pagamentosTipo);
        }

        // POST: PagamentosTipoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PagamentosTipo1,NotaEmpenho,Tipo,DataCadastro,ContratoId,PagamentoId")] PagamentosTipo pagamentosTipo)
        {
            if (id != pagamentosTipo.PagamentosTipo1)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pagamentosTipo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PagamentosTipoExists(pagamentosTipo.PagamentosTipo1))
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
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId", pagamentosTipo.ContratoId);
            ViewData["PagamentoId"] = new SelectList(_context.Pagamentos, "PagamentoId", "PagamentoId", pagamentosTipo.PagamentoId);
            return View(pagamentosTipo);
        }

        // GET: PagamentosTipoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PagamentosTipos == null)
            {
                return NotFound();
            }

            var pagamentosTipo = await _context.PagamentosTipos
                .Include(p => p.Contrato)
                .Include(p => p.Pagamento)
                .FirstOrDefaultAsync(m => m.PagamentosTipo1 == id);
            if (pagamentosTipo == null)
            {
                return NotFound();
            }

            return View(pagamentosTipo);
        }

        // POST: PagamentosTipoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PagamentosTipos == null)
            {
                return Problem("Entity set 'GestorContratosContext.PagamentosTipos'  is null.");
            }
            var pagamentosTipo = await _context.PagamentosTipos.FindAsync(id);
            if (pagamentosTipo != null)
            {
                _context.PagamentosTipos.Remove(pagamentosTipo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PagamentosTipoExists(int id)
        {
          return (_context.PagamentosTipos?.Any(e => e.PagamentosTipo1 == id)).GetValueOrDefault();
        }
    }
}
