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
    public class PortariasController : Controller
    {
        private readonly GestorContratosContext _context;

        public PortariasController(GestorContratosContext context)
        {
            _context = context;
        }

        // GET: Portarias
        public async Task<IActionResult> Index()
        {
            var gestorContratosContext = _context.Portarias.Include(p => p.Contrato);
            return View(await gestorContratosContext.ToListAsync());
        }

        // GET: Portarias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Portarias == null)
            {
                return NotFound();
            }

            var portaria = await _context.Portarias
                .Include(p => p.Contrato)
                .FirstOrDefaultAsync(m => m.PortariaId == id);
            if (portaria == null)
            {
                return NotFound();
            }

            return View(portaria);
        }

        // GET: Portarias/Create
        public IActionResult Create()
        {
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId");
            return View();
        }

        // POST: Portarias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PortariaId,Numero,ProtocoloDiof,UnidadeGestora,Tipo,DataPublicacao,ContratoId")] Portaria portaria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(portaria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId", portaria.ContratoId);
            return View(portaria);
        }

        // GET: Portarias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Portarias == null)
            {
                return NotFound();
            }

            var portaria = await _context.Portarias.FindAsync(id);
            if (portaria == null)
            {
                return NotFound();
            }
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId", portaria.ContratoId);
            return View(portaria);
        }

        // POST: Portarias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PortariaId,Numero,ProtocoloDiof,UnidadeGestora,Tipo,DataPublicacao,ContratoId")] Portaria portaria)
        {
            if (id != portaria.PortariaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(portaria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortariaExists(portaria.PortariaId))
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
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId", portaria.ContratoId);
            return View(portaria);
        }

        // GET: Portarias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Portarias == null)
            {
                return NotFound();
            }

            var portaria = await _context.Portarias
                .Include(p => p.Contrato)
                .FirstOrDefaultAsync(m => m.PortariaId == id);
            if (portaria == null)
            {
                return NotFound();
            }

            return View(portaria);
        }

        // POST: Portarias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Portarias == null)
            {
                return Problem("Entity set 'GestorContratosContext.Portarias'  is null.");
            }
            var portaria = await _context.Portarias.FindAsync(id);
            if (portaria != null)
            {
                _context.Portarias.Remove(portaria);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PortariaExists(int id)
        {
          return (_context.Portarias?.Any(e => e.PortariaId == id)).GetValueOrDefault();
        }
    }
}
