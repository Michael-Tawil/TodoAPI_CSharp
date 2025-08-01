namespace TodoAPI
{
    public class Todo
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;

        public bool Done { get; set; }
    }

    public class CreateTodoRequest
    {
        public string Text { get; set; } = string.Empty;
    }

    public class UpdateTodoRequest
    {
        public string? Text { get; set; }
        public bool? Done { get; set; }
    }
}