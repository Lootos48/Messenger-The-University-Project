using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MessengerServer.Util
{
    public static class FileService
    {
        public static async Task<byte[]> ConvertFileToByteArray(string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                byte[] imageBytes = await File.ReadAllBytesAsync(path);
                return imageBytes;
            }
        }

        public static async Task<string> SaveFileInUploadsFolder(IWebHostEnvironment _webHostEnvironment, byte[] file)
        {
            string uploads = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads");
            string filePath = Path.Combine(uploads, $"{Guid.NewGuid()}.jpg");

            string base64String = Convert.ToBase64String(file);
            File.WriteAllBytes(uploads, Convert.FromBase64String(base64String));

            return filePath;
        }

        public static async Task<string> SaveFileInAvatarsFolder(IWebHostEnvironment _webHostEnvironment, byte[] file)
        {
            string uploads = Path.Combine(_webHostEnvironment.WebRootPath, "Avatars");
            string filePath = Path.Combine(uploads, $"{Guid.NewGuid()}.jpg");

            string base64String = Convert.ToBase64String(file);
            File.WriteAllBytes(uploads, Convert.FromBase64String(base64String));

            return filePath;
        }
    }
}
