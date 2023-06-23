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
    public class UsuariosContratosController : Controller
    {
        private readonly GestorContratosContext _context;

        public UsuariosContratosController(GestorContratosContext context)
        {
            _context = context;
        }

        // GET: UsuariosContratoes
        public async Task<IActionResult> Index()
        {
            var gestorContratosContext = _context.UsuariosContratos.Include(u => u.Contrato).Include(u => u.Usuario);
            return View(await gestorContratosContext.ToListAsync());
        }

        // GET: UsuariosContratoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UsuariosContratos == null)
            {
                return NotFound();
            }

            var usuariosContrato = await _context.UsuariosContratos
                .Include(u => u.Contrato)
                .Include(u => u.Usuario)
                .FirstOrDefaultAsync(m => m.UsuariosContratosId == id);
            if (usuariosContrato == null)
            {
                return NotFound();
            }

            return View(usuariosContrato);
        }

        // GET: UsuariosContratoes/Create
        public IActionResult Create()
        {
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId");
            return View();
        }

        // POST: UsuariosContratoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsuariosContratosId,UsuarioId,ContratoId")] UsuariosContrato usuariosContrato)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuariosContrato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId", usuariosContrato.ContratoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId", usuariosContrato.UsuarioId);
            return View(usuariosContrato);
        }

        // GET: UsuariosContratoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UsuariosContratos == null)
            {
                return NotFound();
            }

            var usuariosContrato = await _context.UsuariosContratos.FindAsync(id);
            if (usuariosContrato == null)
            {
                return NotFound();
            }
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId", usuariosContrato.ContratoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId", usuariosContrato.UsuarioId);
            return View(usuariosContrato);
        }

        // POST: UsuariosContratoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsuariosContratosId,UsuarioId,ContratoId")] UsuariosContrato usuariosContrato)
        {
            if (id != usuariosContrato.UsuariosContratosId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuariosContrato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuariosContratoExists(usuariosContrato.UsuariosContratosId))
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
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId", usuariosContrato.ContratoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId", usuariosContrato.UsuarioId);
            return View(usuariosContrato);
        }

        // GET: UsuariosContratoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UsuariosContratos == null)
            {
                return NotFound();
            }

            var usuariosContrato = await _context.UsuariosContratos
                .Include(u => u.Contrato)
                .Include(u => u.Usuario)
                .FirstOrDefaultAsync(m => m.UsuariosContratosId == id);
            if (usuariosContrato == null)
            {
                return NotFound();
            }

            return View(usuariosContrato);
        }

        // POST: UsuariosContratoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UsuariosContratos == null)
            {
                return Problem("Entity set 'GestorContratosContext.UsuariosContratos'  is null.");
            }
            var usuariosContrato = await _context.UsuariosContratos.FindAsync(id);
            if (usuariosContrato != null)
            {
                _context.UsuariosContratos.Remove(usuariosContrato);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuariosContratoExists(int id)
        {
          return (_context.UsuariosContratos?.Any(e => e.UsuariosContratosId == id)).GetValueOrDefault();
        }
    }
}
