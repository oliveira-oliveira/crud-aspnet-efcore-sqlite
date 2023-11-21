using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstApi.Data;
using MyFirstApi.Models;
using System.Reflection.Metadata.Ecma335;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyFirstApi.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class TodoController : ControllerBase
    {
        [HttpGet]
        [Route("todos")]
        public async Task<IActionResult> GetAsync([FromServices] AppDbContext context)
        {
            var todos = await context.Todos.ToListAsync();
            return Ok(todos);
        }

        [HttpGet("todos/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromServices] AppDbContext context, 
                                                      [FromRoute] int id)
        {
            var todos = await context
                        .Todos.AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id == id);

            return todos == null? NotFound() : Ok(todos);
        }

        [HttpPost("todos")]
        public async Task<IActionResult> PostAsync([FromServices] AppDbContext context, 
                                                   [FromBody] Todo todo)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var update = new Todo
            {
                UpdatedDate = DateTime.Now,
                Title = todo.Title,
                isDone = todo.isDone
            };

            try
            {
                await context.AddAsync(todo);
                await context.SaveChangesAsync();
                return Created($"api/todos/{todo.Id}", todo);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest();
            }
        }

        [HttpPut("todos/{id}")]
        public async Task<IActionResult> PutAsync([FromServices] AppDbContext context,
                             [FromBody] Todo todo,
                             [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var model = await context
                        .Todos
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (model == null)
                return NotFound();

            try
            {
                model.Title = todo.Title;

                if (todo.isDone == true)
                {
                    model.isDone = true;
                    model.UpdatedDate = DateTime.Now;
                }
                else
                    model.isDone = false;
                   // model.UpdatedDate = DateTime.Now;

                context.Todos.Update(model);
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest();
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromServices] AppDbContext context,
                                                     [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var todo = await context
                .Todos
                .FirstOrDefaultAsync(x => x.Id == id);

            if (todo == null)
                return NotFound();

            try
            {
                context.Todos.Remove(todo);
                await context.SaveChangesAsync();

                return Ok(todo);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest();
            }
        }
    }
}
