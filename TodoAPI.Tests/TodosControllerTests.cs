using Microsoft.EntityFrameworkCore;
using TodoAPI.Controllers;
using TodoAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace TodoAPI.Tests;

public class TodosControllerTests
{
    private readonly DbContextOptions<TodoDbContext> _dbOptions;

    public TodosControllerTests()
    {
        _dbOptions = new DbContextOptionsBuilder<TodoDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }
    [Fact]
    public async Task GetTodos_ReturnAListOfTodos()
    {
        await using (var context = new TodoDbContext(_dbOptions)) {

            context.Todos.AddRange(

               new Todo { Id = 1, Text = "Test Todo 1", Done = false },
               new Todo { Id = 2, Text = "Test Todo 2", Done = true }

            );
            await context.SaveChangesAsync();
        }

        await using (var context = new TodoDbContext(_dbOptions))
        {
            var controller = new TodosController(context);
            var result = await controller.GetTodos();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedTodos = Assert.IsAssignableFrom<IEnumerable<Todo>>(okResult.Value);
            Assert.Equal(2, returnedTodos.Count());
        }
    }
    [Fact]
    public async Task CreateTodo_WithValidText_CreatesAndReturnsNewTodo()
    {
        var request = new CreateTodoRequest { Text = "hello from test" };

        await using (var context = new TodoDbContext( _dbOptions))
        {
            var controller = new TodosController(context);
            var result = await controller.CreateTodo(request);

            var CreateResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var CreatedTodo = Assert.IsType<Todo>(CreateResult.Value);

            Assert.Equal(request.Text, CreatedTodo.Text);
            Assert.False(CreatedTodo.Done);
        }

        await using (var context = new TodoDbContext(_dbOptions))
        {
            Assert.Equal(1, await context.Todos.CountAsync());
            var todoInDb = await context.Todos.SingleAsync();
            Assert.Equal(request.Text, todoInDb.Text);
        }
    }

    [Fact]
    public async Task UpdateTodo_ReturnUpdatedTodo()
    {
        await using (var context = new TodoDbContext(_dbOptions))
        {
            context.Todos.Add(new Todo { Id = 1, Text = "Initial Text", Done = false });
            await context.SaveChangesAsync();
        }

        var request = new UpdateTodoRequest { Text = "Hello from test put", Done = true };

        await using (var context = new TodoDbContext(_dbOptions))
        {
            var controller = new TodosController(context);
            var result = await controller.UpdateTodo(1,request);

            var updatedresult = Assert.IsType<OkObjectResult>(result.Result);
            var updatedtodo = Assert.IsType<Todo>(updatedresult.Value);

            Assert.Equal(request.Text, updatedtodo.Text);
            Assert.True(updatedtodo.Done);
        }

        await using (var context = new TodoDbContext(_dbOptions))
        {
            var todoInDb = await context.Todos.FindAsync(1);
            Assert.NotNull(todoInDb);
            Assert.Equal(request.Text, todoInDb.Text);
            Assert.True(todoInDb.Done);
        }
    }

    [Fact]
    public async Task DeleteTodo_ReturnNothing()
    {
        await using (var context = new TodoDbContext(_dbOptions))
        {
            context.Todos.Add(new Todo { Id = 1, Text = "Initial Text", Done = false });
            await context.SaveChangesAsync();
        }
        await using (var context = new TodoDbContext(_dbOptions))
        {
            var controller = new TodosController(context);
            var result = await controller.DeleteTodo(1);

            Assert.IsType<NoContentResult>(result);
        }
        await using (var context = new TodoDbContext(_dbOptions))
        {
            var todoInDb = await context.Todos.FindAsync(1);
            Assert.Null(todoInDb);
        }

    }
}