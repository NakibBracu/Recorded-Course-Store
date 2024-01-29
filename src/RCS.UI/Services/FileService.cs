using System;
using System.IO;

namespace RCS.UI.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHost;
        private readonly ILogger _logger;

        public FileService()
        {
            
        }
        public FileService(IWebHostEnvironment webHost, ILogger logger)
        {
            _webHost = webHost;
            _logger = logger;
        }

        public string SaveFile(IFormFile file)
        {
            var name = RandomName();
            var save_path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "wwwroot/CourseThumbnail"));
            if (!Directory.Exists(save_path))
            {
                Directory.CreateDirectory(save_path);
            }
            using (var fileStream = new FileStream(Path.Combine(save_path, name), FileMode.Create))
            {
                 file.CopyTo(fileStream);
            }
            return name;
        }

        private string RandomName(string prefix = "") =>
            $"img{prefix}_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss")}.png";

        public string SaveFile(IFormFile file, string path)
        {
            string fileName = string.Empty;
            var allowedImageExtensions = new string[] { ".jpg", ".png", ".jpeg" };
            var allowedVideoExtension = ".mp4";

            try
            {
                var rootPath = _webHost.WebRootPath;
                var imagePath = Path.Combine(rootPath, path);
                var videoPath = Path.Combine(rootPath, "mp4");

                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }

                if (!Directory.Exists(videoPath))
                {
                    Directory.CreateDirectory(videoPath);
                }

                // Check the allowed extensions
                var ext = Path.GetExtension(file.FileName);
                if (allowedVideoExtension.Contains(ext))
                {
                    string uniqueString = Guid.NewGuid().ToString();
                    var newFileName = uniqueString + ext;
                    var fileWithPath = Path.Combine(videoPath, newFileName);

                    using (var stream = new FileStream(fileWithPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    fileName = newFileName;
                }
                else if (allowedImageExtensions.Contains(ext) && file.Length <= 900 * 1000)
                {
                    string uniqueString = Guid.NewGuid().ToString();
                    var newFileName = uniqueString + ext;
                    var fileWithPath = Path.Combine(imagePath, newFileName);

                    using (var stream = new FileStream(fileWithPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    fileName = newFileName;
                }

                return fileName;
            }
            catch (Exception ex)
            {
                _logger.LogError(""+ex.Message);
                throw ex;
            }
        }

        public async Task<string> SaveFileAsync(IFormFile file, string path)
        {
            string fileName = string.Empty;
            var allowedImageExtensions = new string[] { ".jpg", ".png", ".jpeg" };
            var allowedVideoExtension = ".mp4";

            try
            {
                var rootPath = _webHost.WebRootPath;
                var imagePath = Path.Combine(rootPath, path);
                var videoPath = Path.Combine(rootPath, "mp4");

                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }

                if (!Directory.Exists(videoPath))
                {
                    Directory.CreateDirectory(videoPath);
                }

                // Check the allowed extensions

                var ext = Path.GetExtension(file.FileName);
                if (allowedVideoExtension.Contains(ext))
                {
                    string uniqueString = Guid.NewGuid().ToString();
                    // we are trying to create a unique filename here
                    var newFileName = uniqueString + ext;
                    var fileWithPath = Path.Combine(videoPath, newFileName);
                    var stream = new FileStream(fileWithPath, FileMode.Create);
                    await file.CopyToAsync(stream);
                    stream.Close();
                    fileName = newFileName;
                }
                else if (allowedImageExtensions.Contains(ext) && file.Length <= 300 * 1000)
                {
                    string uniqueString = Guid.NewGuid().ToString();
                    // we are trying to create a unique filename here
                    var newFileName = uniqueString + ext;
                    var fileWithPath = Path.Combine(imagePath, newFileName);
                    var stream = new FileStream(fileWithPath, FileMode.Create);
                    await file.CopyToAsync(stream);
                    stream.Close();
                    fileName = newFileName;
                }


                return fileName;
            }
            catch (Exception ex)
            {
                _logger.LogError("File upload Failed");
                throw ex;
            }
        }

        public bool DeleteFile(string filepath)
        {

            var rootPath = _webHost.WebRootPath;
            try
            {
                var path = Path.Combine(rootPath, filepath);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("File delete Failed");
                throw ex;
            }

        }
    }
}
