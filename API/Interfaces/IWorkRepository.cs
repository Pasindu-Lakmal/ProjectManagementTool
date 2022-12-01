using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IWorkRepository
    {
        void AddWork (Work work);

        void DeleteWork (Work work);

        Task<Work> GetWork(int id);
        Task<Work> GetWorks();
    }
}