using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IWorkRepository
    {
        void AddWork (Work Work);

        void DeleteWork (Work work);

        Task<IEnumerable<Work>> GetWorks();
        
        Task<bool> SaveAllAsync();

        Task<Work> GetWorkByIdAsync(int id);
        
    }
}