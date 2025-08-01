using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace TodoAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {

        private static List<Todo> _todos = new List<Todo>
    {
        new Todo {Id = 1, Text = "Learn C#", Done = false},
        new Todo {Id = 2, Text = "Build Web API", Done=false}
    };
        private static int _nextId = 3;

        [HttpGet]
        public List<Todo> GetTodos() {

            return _todos;
        }

        [HttpPost]
        public ActionResult<Todo> CreateTodo([FromBody] CreateTodoRequest request) {

            if (string.IsNullOrWhiteSpace(request.Text))
            {
                return BadRequest(new { error = "Text is required" });
            }

            var newTodo = new Todo
            {
                Id = _nextId++,
                Text = request.Text.Trim(),
                Done = false
            };

            _todos.Add(newTodo);
            return CreatedAtAction(nameof(GetTodos), new { id = newTodo.Id }, newTodo);
        }

        [HttpPut("{id}")]
        public ActionResult<Todo> UpdateTodo(int id, [FromBody] UpdateTodoRequest request)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
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
            return Ok(todo);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTodo(int id)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo == null )
            {
                return NotFound(new { error = "Todo Not Found" });
            }
            _todos.Remove(todo);
            return NoContent();
        }
    }
}
