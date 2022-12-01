using API.DTOs;
using API.Entities;
using API.Interfaces;

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
            throw new NotImplementedException();
        }

        public Task<Work> GetWork(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Work> GetWorks()
        {
            throw new NotImplementedException();
        }
    }
}
