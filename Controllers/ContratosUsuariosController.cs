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
    public class ContratosUsuariosController : Controller
    {
        private readonly GestorContratosContext _context;

        public ContratosUsuariosController(GestorContratosContext context)
        {
            _context = context;
        }

        // GET: ContratosUsuarios
        public async Task<IActionResult> Index()
        {
            var gestorContratosContext = _context.ContratosUsuarios.Include(c => c.Contrato).Include(c => c.Usuario);
            return View(await gestorContratosContext.ToListAsync());
        }

        // GET: ContratosUsuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ContratosUsuarios == null)
            {
                return NotFound();
            }

            var contratosUsuario = await _context.ContratosUsuarios
                .Include(c => c.Contrato)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.ContratosUsuariosId == id);
            if (contratosUsuario == null)
            {
                return NotFound();
            }

            return View(contratosUsuario);
        }

        // GET: ContratosUsuarios/Create
        public IActionResult Create()
        {
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId");
            return View();
        }

        // POST: ContratosUsuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContratosUsuariosId,UsuarioId,ContratoId")] ContratosUsuario contratosUsuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contratosUsuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId", contratosUsuario.ContratoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId", contratosUsuario.UsuarioId);
            return View(contratosUsuario);
        }

        // GET: ContratosUsuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ContratosUsuarios == null)
            {
                return NotFound();
            }

            var contratosUsuario = await _context.ContratosUsuarios.FindAsync(id);
            if (contratosUsuario == null)
            {
                return NotFound();
            }
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId", contratosUsuario.ContratoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId", contratosUsuario.UsuarioId);
            return View(contratosUsuario);
        }

        // POST: ContratosUsuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContratosUsuariosId,UsuarioId,ContratoId")] ContratosUsuario contratosUsuario)
        {
            if (id != contratosUsuario.ContratosUsuariosId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contratosUsuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContratosUsuarioExists(contratosUsuario.ContratosUsuariosId))
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
            ViewData["ContratoId"] = new SelectList(_context.Contratos, "ContratoId", "ContratoId", contratosUsuario.ContratoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId", contratosUsuario.UsuarioId);
            return View(contratosUsuario);
        }

        // GET: ContratosUsuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ContratosUsuarios == null)
            {
                return NotFound();
            }

            var contratosUsuario = await _context.ContratosUsuarios
                .Include(c => c.Contrato)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.ContratosUsuariosId == id);
            if (contratosUsuario == null)
            {
                return NotFound();
            }

            return View(contratosUsuario);
        }

        // POST: ContratosUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ContratosUsuarios == null)
            {
                return Problem("Entity set 'GestorContratosContext.ContratosUsuarios'  is null.");
            }
            var contratosUsuario = await _context.ContratosUsuarios.FindAsync(id);
            if (contratosUsuario != null)
            {
                _context.ContratosUsuarios.Remove(contratosUsuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContratosUsuarioExists(int id)
        {
          return (_context.ContratosUsuarios?.Any(e => e.ContratosUsuariosId == id)).GetValueOrDefault();
        }
    }
}
