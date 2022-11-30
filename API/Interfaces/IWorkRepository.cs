namespace API.Interfaces
{
    public interface IWorkRepository
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
    }
}