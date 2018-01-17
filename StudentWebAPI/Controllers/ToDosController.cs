﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentWebAPI.Data;

namespace StudentWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class ToDosController : Controller
    {
        private readonly TodoContext _context;

        public ToDosController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/ToDos
        [HttpGet]
        public IEnumerable<ToDo> GetTodos()
        {
            return _context.Todos;
        }

        // GET: api/ToDos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetToDo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var toDo = await _context.Todos.SingleOrDefaultAsync(m => m.id == id);

            if (toDo == null)
            {
                return NotFound();
            }

            return Ok(toDo);
        }

        // PUT: api/ToDos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDo([FromRoute] int id, [FromBody] ToDo toDo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != toDo.id)
            {
                return BadRequest();
            }

            _context.Entry(toDo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ToDos
        [HttpPost]
        public async Task<IActionResult> PostToDo([FromBody] ToDo toDo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Todos.Add(toDo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetToDo", new { id = toDo.id }, toDo);
        }

        // DELETE: api/ToDos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var toDo = await _context.Todos.SingleOrDefaultAsync(m => m.id == id);
            if (toDo == null)
            {
                return NotFound();
            }

            _context.Todos.Remove(toDo);
            await _context.SaveChangesAsync();

            return Ok(toDo);
        }

        private bool ToDoExists(int id)
        {
            return _context.Todos.Any(e => e.id == id);
        }
    }
}