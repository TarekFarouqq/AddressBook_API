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
            var uploadsPath = Path.Combine(_env.WebRootPath, "photos");
            Directory.CreateDirectory(uploadsPath);

            var fullPath = Path.Combine(uploadsPath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await fileStream.CopyToAsync(stream);

            return fileName;
        }
    }
}
