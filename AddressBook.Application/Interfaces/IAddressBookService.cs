using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressBook.Application.DTOs.AddressBookDTOs;

namespace AddressBook.Application.Interfaces
{
    public interface IAddressBookService
    {
        Task<IEnumerable<AddressBookReadDto>> GetAllAsync();
        Task<AddressBookReadDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(AddressBookCreateDto dto);
        Task UpdateAsync(int id, AddressBookReadDto dto);
        Task DeleteAsync(int id);
        Task<byte[]> ExportToExcelAsync();
        Task<PagedResult<AddressBookReadDto>> SearchAsync( AddressBookSearchQuery query, CancellationToken ct = default);
    }
}
