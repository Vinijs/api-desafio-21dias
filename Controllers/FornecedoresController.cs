using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using api.Models;
using api.Servicos;

namespace mvc_entity.Controllers
{
    [ApiController]
    public class FornecedoresController : ControllerBase
    {
        private readonly DbContexto _context;

        public Fornecedores
Controller(DbContexto context)
        {
            _context = context;
        }

        // GET: /Fornecedores

        [HttpGet]
        [Route("/Fornecedores
/")]
        public async Task<IActionResult> Index()
        {
              return StatusCode(200, await _context.Fornecedores
        .ToListAsync());
        }

        // GET: /Fornecedores
/5
        [HttpGet]
        [Route("/Fornecedores
/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Fornecedores
     == null)
            {
                return NotFound();
            }

            var aluno = await _context.Fornecedores
    
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return StatusCode(200, aluno);
        }

        // POST: /Fornecedores

        [HttpPost]
        [Route("/Fornecedores
")]
        public async Task<IActionResult> Create([Bind("Id,Nome,Matricula,Notas")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aluno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return StatusCode(201, aluno);
        }

        // PUT: /Fornecedores
/5
        [HttpPut]
        [Route("/Fornecedores
/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Matricula,Notas")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    aluno.Id = id;
                    _context.Update(aluno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlunoExists(aluno.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return StatusCode(200, aluno);
            }
            return StatusCode(200, aluno);
        }

        // DELETE: /Fornecedores
/5
        [HttpDelete]
        [Route("/Fornecedores
/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Fornecedores
     == null)
            {
                return Problem("Entity set 'DbContexto.Fornecedores
        '  is null.");
            }
            var aluno = await _context.Fornecedores
    .FindAsync(id);
            if (aluno != null)
            {
                _context.Fornecedores
        .Remove(aluno);
            }
            
            await _context.SaveChangesAsync();
            return StatusCode(204);
        }

        private bool AlunoExists(int id)
        {
          return _context.Fornecedores
    .Any(e => e.Id == id);
        }
    }
}
