using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class WorkRepository : IWorkRepository
    {
        private readonly DataContext _context;
        public WorkRepository(DataContext context)
        {
            _context = context;     
        }

        public void AddWork(Work work)
        {
            _context.Works.Add(work);
        }

        public void DeleteWork(Work work)
        {
            _context.Works.Remove(work);
        }

        public async Task<IEnumerable<Work>> GetWorks()
        {
           return await _context.Works.ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
             return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Work> GetWorkByIdAsync(int id)
        {
            return await _context.Works.FindAsync(id);
        }

    }
}
