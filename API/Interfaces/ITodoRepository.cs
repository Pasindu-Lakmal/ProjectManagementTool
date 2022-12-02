using API.Entities;

namespace API.Interfaces
{
    public interface ITodoRepository
    {
        void AddTodo (Todo todo);

        void DeleteTodo (Todo todo);

        Task<IEnumerable<Todo>> GetTodosAsync();
        
        Task<bool> SaveAllAsync();

        Task<Todo> GetTodoByIdAsync(int id);
    }
}

