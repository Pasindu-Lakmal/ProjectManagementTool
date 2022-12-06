using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IEnumerable<Todo>> GetTodoByWorkIdAsync(int workId)
        {
            return await _context.Todos.Where(r => r.WorkId == workId).Include(e =>e.Works).ToListAsync();
        }


        public async Task<IEnumerable<Todo>> GetTodosAsync()
        {
            return await _context.Todos.ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

         public async Task<IEnumerable<Todo>> GetTodoByAssigneeIdAsync(int assigneeId)
        {
            return await _context.Todos.Where(r => r.AppUserId == assigneeId).Include(e =>e.Works).ToListAsync();
        }

        

        public async Task<IEnumerable<Todo>> GetTodoByAssigneeName(string assigneeName)
        {
            return await _context.Todos.Where(r => r.AssigneeName == assigneeName).Include(e =>e.Works).ToListAsync();

        }

        public void Update(Todo todo)
        {
             _context.Entry(todo).State = EntityState.Modified;
        }

        public async Task<IEnumerable<Todo>> GetTodoByCreaterName(string createrName)
        {
             return await _context.Todos.Where(r => r.CreaterName == createrName).Include(e =>e.Works).ToListAsync();
        }
    }
}