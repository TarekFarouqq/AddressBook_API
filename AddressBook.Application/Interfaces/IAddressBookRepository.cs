using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressBook.Application.DTOs.AddressBookDTOs;
using AddressBook.Domain.Entities;

namespace AddressBook.Application.Interfaces
{
    public interface IAddressBookRepository : IGenericRepository<AddressBookEntry>
    {
        Task<PagedResult<AddressBookReadDto>> SearchAsync( AddressBookSearchQuery query,CancellationToken ct = default);
    }
}