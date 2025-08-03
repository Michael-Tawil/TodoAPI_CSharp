using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Threading.Tasks;
using TodoAPI.Data;

namespace TodoAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase

    {
        private readonly TodoDbContext _context;

        public TodosController(TodoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodos() {

            var todos = await _context.Todos.ToListAsync();
            return Ok(todos);
        }

         [HttpPost]
         public async Task<ActionResult<Todo>> CreateTodo([FromBody] CreateTodoRequest request) {

             if (string.IsNullOrWhiteSpace(request.Text))
             {
                 return BadRequest(new { error = "Text is required" });
             }

             var newTodo = new Todo
             {
                 Text = request.Text.Trim(),
                 Done = false
             };

             _context.Todos.Add(newTodo);
            await _context.SaveChangesAsync();
             return CreatedAtAction(nameof(GetTodos), new { id = newTodo.Id }, newTodo);
         }

        [HttpPut("{id}")]
         public async Task <ActionResult<Todo>> UpdateTodo(int id, [FromBody] UpdateTodoRequest request)
         {
             var todo = _context.Todos.Find(id);
             if (todo == null)
             {
                 return NotFound(new { error = "Todo Not Found" });
             }
             if (!string.IsNullOrWhiteSpace(request.Text))
             {
                 todo.Text = request.Text.Trim();
             }
             if (request.Done.HasValue)
             {
                 todo.Done = request.Done.Value;
             }
             await _context.SaveChangesAsync();
             return Ok(todo);
         }

         [HttpDelete("{id}")]
         public async Task <IActionResult> DeleteTodo(int id)
         {
             var todo = _context.Todos.Find(id);
             if (todo == null )
             {
                 return NotFound(new { error = "Todo Not Found" });
             }
             _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            return NoContent();
         }
    }
}
