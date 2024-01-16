namespace RCS.UI.Services
{
    public interface IFileService
    {
        string SaveFile(IFormFile file, string path);
        Task<string> SaveFileAsync(IFormFile file, string path);
        bool DeleteFile(string filepath);
        string SaveFile(IFormFile file);
    }
}
