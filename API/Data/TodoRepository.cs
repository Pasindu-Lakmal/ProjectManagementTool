using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class TodoRepository : ITodoRepository
    {
       
        private readonly DataContext _context;

        public TodoRepository(DataContext context)
        {
            _context = context;  
        }




        public void AddTodo(Todo todo)
        {
            _context.Todos.Add(todo);
        }

        public void DeleteTodo(Todo todo)
        {
            _context.Todos.Remove(todo);
        }

        public async Task<Todo> GetTodoByIdAsync(int id)
        {
           return await _context.Todos.FindAsync(id);
        }

        public async Task<IEnumerable<Todo>> GetTodosAsync()
        {
            return await _context.Todos.ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}