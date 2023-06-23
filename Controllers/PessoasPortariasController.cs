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
    public class PessoasPortariasController : Controller
    {
        private readonly GestorContratosContext _context;

        public PessoasPortariasController(GestorContratosContext context)
        {
            _context = context;
        }

        // GET: PessoasPortarias
        public async Task<IActionResult> Index()
        {
            var gestorContratosContext = _context.PessoasPortarias.Include(p => p.Pessoa).Include(p => p.Portaria);
            return View(await gestorContratosContext.ToListAsync());
        }

        // GET: PessoasPortarias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PessoasPortarias == null)
            {
                return NotFound();
            }

            var pessoasPortaria = await _context.PessoasPortarias
                .Include(p => p.Pessoa)
                .Include(p => p.Portaria)
                .FirstOrDefaultAsync(m => m.ResolucaoId == id);
            if (pessoasPortaria == null)
            {
                return NotFound();
            }

            return View(pessoasPortaria);
        }

        // GET: PessoasPortarias/Create
        public IActionResult Create()
        {
            ViewData["PessoaId"] = new SelectList(_context.Pessoas, "PessoaId", "PessoaId");
            ViewData["PortariaId"] = new SelectList(_context.Portarias, "PortariaId", "PortariaId");
            return View();
        }

        // POST: PessoasPortarias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResolucaoId,TipoPortaria,FuncaoPessoa,PessoaId,PortariaId")] PessoasPortaria pessoasPortaria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pessoasPortaria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PessoaId"] = new SelectList(_context.Pessoas, "PessoaId", "PessoaId", pessoasPortaria.PessoaId);
            ViewData["PortariaId"] = new SelectList(_context.Portarias, "PortariaId", "PortariaId", pessoasPortaria.PortariaId);
            return View(pessoasPortaria);
        }

        // GET: PessoasPortarias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PessoasPortarias == null)
            {
                return NotFound();
            }

            var pessoasPortaria = await _context.PessoasPortarias.FindAsync(id);
            if (pessoasPortaria == null)
            {
                return NotFound();
            }
            ViewData["PessoaId"] = new SelectList(_context.Pessoas, "PessoaId", "PessoaId", pessoasPortaria.PessoaId);
            ViewData["PortariaId"] = new SelectList(_context.Portarias, "PortariaId", "PortariaId", pessoasPortaria.PortariaId);
            return View(pessoasPortaria);
        }

        // POST: PessoasPortarias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResolucaoId,TipoPortaria,FuncaoPessoa,PessoaId,PortariaId")] PessoasPortaria pessoasPortaria)
        {
            if (id != pessoasPortaria.ResolucaoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pessoasPortaria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoasPortariaExists(pessoasPortaria.ResolucaoId))
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
            ViewData["PessoaId"] = new SelectList(_context.Pessoas, "PessoaId", "PessoaId", pessoasPortaria.PessoaId);
            ViewData["PortariaId"] = new SelectList(_context.Portarias, "PortariaId", "PortariaId", pessoasPortaria.PortariaId);
            return View(pessoasPortaria);
        }

        // GET: PessoasPortarias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PessoasPortarias == null)
            {
                return NotFound();
            }

            var pessoasPortaria = await _context.PessoasPortarias
                .Include(p => p.Pessoa)
                .Include(p => p.Portaria)
                .FirstOrDefaultAsync(m => m.ResolucaoId == id);
            if (pessoasPortaria == null)
            {
                return NotFound();
            }

            return View(pessoasPortaria);
        }

        // POST: PessoasPortarias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PessoasPortarias == null)
            {
                return Problem("Entity set 'GestorContratosContext.PessoasPortarias'  is null.");
            }
            var pessoasPortaria = await _context.PessoasPortarias.FindAsync(id);
            if (pessoasPortaria != null)
            {
                _context.PessoasPortarias.Remove(pessoasPortaria);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PessoasPortariaExists(int id)
        {
          return (_context.PessoasPortarias?.Any(e => e.ResolucaoId == id)).GetValueOrDefault();
        }
    }
}
