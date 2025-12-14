using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressBook.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace AddressBook.Application.Services
{
    public class LocalFileStorageService : IFileStorageService  
    {
        private readonly IWebHostEnvironment _env;

        public LocalFileStorageService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string?> SaveAsync(Stream fileStream, string fileName)
        {
            if (fileStream == null) throw new ArgumentNullException(nameof(fileStream));
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException(nameof(fileName));

            fileName = Path.GetFileName(fileName);

            var webRoot = _env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot");
            var folderName = "photos";

            var uploadsPath = Path.Combine(webRoot, folderName);
            Directory.CreateDirectory(uploadsPath);

            var fullPath = Path.Combine(uploadsPath, fileName);

            if (fileStream.CanSeek) fileStream.Position = 0;

            await using var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None);
            await fileStream.CopyToAsync(stream);

            
            return $"{folderName}/{fileName}";
        }
    }
    }
