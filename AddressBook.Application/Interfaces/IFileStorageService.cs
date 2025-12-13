using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Application.Interfaces
{
    public interface IFileStorageService
    {
        Task<string?> SaveAsync(Stream fileStream, string fileName);
    }
}
