using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using api.Models;
using api.Servicos;
using EntityFrameworkPaginateCore;
using web_renderizacao_server_side.Helpers;

namespace mvc_entity.Controllers
{
    [ApiController]
    [Logado]
    public class AlunosController : ControllerBase
    {
        private readonly DbContexto _context;
        private const int QUANTIDADE_POR_PAGINA = 3;

        public AlunosController(DbContexto context)
        {
            _context = context;
        }

        // GET: /alunos
        [HttpGet]
        [Route("/alunos/")]
        public async Task<IActionResult> Index(int page = 1)
        {
            //   var alunosPaginados = await _context.Alunos.OrderBy(a => a.Id)
            //         .Skip((page - 1) * QUANTIDADE_POR_PAGINA)
            //         .Take(QUANTIDADE_POR_PAGINA)
            //         .ToListAsync();

            //   return StatusCode(200, alunosPaginados);
                 return StatusCode(200, await _context.Alunos.OrderBy(a => a.Id).PaginateAsync(page, QUANTIDADE_POR_PAGINA));
            //   return StatusCode(200, await _context.Alunos.OrderByDescending(a => a.Id).PaginateAsync(page, QUANTIDADE_POR_PAGINA));
        }

        // GET: /alunos/5
        [HttpGet]
        [Route("/alunos/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Alunos == null)
            {
                return NotFound();
            }

            var aluno = await _context.Alunos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return StatusCode(200, aluno);
        }

        // POST: /alunos
        [HttpPost]
        [Route("/alunos")]
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

        // PUT: /alunos/5
        [HttpPut]
        [Route("/alunos/{id}")]
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

        // DELETE: /alunos/5
        [HttpDelete]
        [Route("/alunos/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Alunos == null)
            {
                return Problem("Entity set 'DbContexto.Alunos'  is null.");
            }
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno != null)
            {
                _context.Alunos.Remove(aluno);
            }
            
            await _context.SaveChangesAsync();
            return StatusCode(204);
        }

        private bool AlunoExists(int id)
        {
          return _context.Alunos.Any(e => e.Id == id);
        }
    }
}
