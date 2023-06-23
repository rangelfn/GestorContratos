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
    public class AditivosController : Controller
    {
        private readonly GestorContratosContext _context;

        public AditivosController(GestorContratosContext context)
        {
            _context = context;
        }

        // GET: Aditivoes
        public async Task<IActionResult> Index()
        {
            var gestorContratosContext = _context.Aditivos.Include(a => a.Contrato);
            return View(await gestorContratosContext.ToListAsync());
        }

        // GET: Aditivoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Aditivos == null)
            {
                return NotFound();
            }

            var aditivo = await _context.Aditivos
                .Include(a => a.Contrato)
                .FirstOrDefaultAsync(m => m.AditivoId == id);
            if (aditivo == null)
            {
                return NotFound();
            }

            return View(aditivo);
        }

        // GET: Aditivoes/Create
        public IActionResult Create()
        {
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId");
            return View();
        }

        // POST: Aditivoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AditivoId,Numero,Descricao,DataAditivos,Valor,ContratoId")] Aditivo aditivo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aditivo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId", aditivo.ContratoId);
            return View(aditivo);
        }

        // GET: Aditivoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Aditivos == null)
            {
                return NotFound();
            }

            var aditivo = await _context.Aditivos.FindAsync(id);
            if (aditivo == null)
            {
                return NotFound();
            }
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId", aditivo.ContratoId);
            return View(aditivo);
        }

        // POST: Aditivoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AditivoId,Numero,Descricao,DataAditivos,Valor,ContratoId")] Aditivo aditivo)
        {
            if (id != aditivo.AditivoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aditivo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AditivoExists(aditivo.AditivoId))
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
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId", aditivo.ContratoId);
            return View(aditivo);
        }

        // GET: Aditivoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Aditivos == null)
            {
                return NotFound();
            }

            var aditivo = await _context.Aditivos
                .Include(a => a.Contrato)
                .FirstOrDefaultAsync(m => m.AditivoId == id);
            if (aditivo == null)
            {
                return NotFound();
            }

            return View(aditivo);
        }

        // POST: Aditivoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Aditivos == null)
            {
                return Problem("Entity set 'GestorContratosContext.Aditivos'  is null.");
            }
            var aditivo = await _context.Aditivos.FindAsync(id);
            if (aditivo != null)
            {
                _context.Aditivos.Remove(aditivo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AditivoExists(int id)
        {
          return (_context.Aditivos?.Any(e => e.AditivoId == id)).GetValueOrDefault();
        }
    }
}
