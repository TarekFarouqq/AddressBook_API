using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IAddressBookRepository Entries { get; }
        IGenericRepository<AddressBook.Domain.Entities.Job> Jobs { get; }
        IGenericRepository<AddressBook.Domain.Entities.Department> Departments { get; }
        IGenericRepository<AddressBook.Domain.Entities.ApplicationUser> Users { get; }
        Task<int> SaveChangesAsync();
    }
}
