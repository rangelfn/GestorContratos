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
    public class ResolucoesController : Controller
    {
        private readonly GestorContratosContext _context;

        public ResolucoesController(GestorContratosContext context)
        {
            _context = context;
        }

        // GET: Resolucoes
        public async Task<IActionResult> Index()
        {
            var gestorContratosContext = _context.Resolucoes.Include(r => r.Pessoa).Include(r => r.Portaria);
            return View(await gestorContratosContext.ToListAsync());
        }

        // GET: Resolucoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Resolucoes == null)
            {
                return NotFound();
            }

            var resoluco = await _context.Resolucoes
                .Include(r => r.Pessoa)
                .Include(r => r.Portaria)
                .FirstOrDefaultAsync(m => m.ResolucaoId == id);
            if (resoluco == null)
            {
                return NotFound();
            }

            return View(resoluco);
        }

        // GET: Resolucoes/Create
        public IActionResult Create()
        {
            ViewData["PessoaId"] = new SelectList(_context.Pessoas, "PessoaId", "PessoaId");
            ViewData["PortariaId"] = new SelectList(_context.Portarias, "PortariaId", "PortariaId");
            return View();
        }

        // POST: Resolucoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResolucaoId,Tipo,DataInicio,DataFim,PortariaId,PessoaId")] Resoluco resoluco)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resoluco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PessoaId"] = new SelectList(_context.Pessoas, "PessoaId", "PessoaId", resoluco.PessoaId);
            ViewData["PortariaId"] = new SelectList(_context.Portarias, "PortariaId", "PortariaId", resoluco.PortariaId);
            return View(resoluco);
        }

        // GET: Resolucoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Resolucoes == null)
            {
                return NotFound();
            }

            var resoluco = await _context.Resolucoes.FindAsync(id);
            if (resoluco == null)
            {
                return NotFound();
            }
            ViewData["PessoaId"] = new SelectList(_context.Pessoas, "PessoaId", "PessoaId", resoluco.PessoaId);
            ViewData["PortariaId"] = new SelectList(_context.Portarias, "PortariaId", "PortariaId", resoluco.PortariaId);
            return View(resoluco);
        }

        // POST: Resolucoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResolucaoId,Tipo,DataInicio,DataFim,PortariaId,PessoaId")] Resoluco resoluco)
        {
            if (id != resoluco.ResolucaoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resoluco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResolucoExists(resoluco.ResolucaoId))
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
            ViewData["PessoaId"] = new SelectList(_context.Pessoas, "PessoaId", "PessoaId", resoluco.PessoaId);
            ViewData["PortariaId"] = new SelectList(_context.Portarias, "PortariaId", "PortariaId", resoluco.PortariaId);
            return View(resoluco);
        }

        // GET: Resolucoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Resolucoes == null)
            {
                return NotFound();
            }

            var resoluco = await _context.Resolucoes
                .Include(r => r.Pessoa)
                .Include(r => r.Portaria)
                .FirstOrDefaultAsync(m => m.ResolucaoId == id);
            if (resoluco == null)
            {
                return NotFound();
            }

            return View(resoluco);
        }

        // POST: Resolucoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Resolucoes == null)
            {
                return Problem("Entity set 'GestorContratosContext.Resolucoes'  is null.");
            }
            var resoluco = await _context.Resolucoes.FindAsync(id);
            if (resoluco != null)
            {
                _context.Resolucoes.Remove(resoluco);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResolucoExists(int id)
        {
          return (_context.Resolucoes?.Any(e => e.ResolucaoId == id)).GetValueOrDefault();
        }
    }
}
