using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressBook.Application.Interfaces;
using AddressBook.Domain.Entities;
using AddressBook.Infrastructure.DBContext;

namespace AddressBook.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AddressBookDBContext _context;
        public IAddressBookRepository Entries { get; }
        public IGenericRepository<Job> Jobs { get; }
        public IGenericRepository<Department> Departments { get; }
        public IGenericRepository<ApplicationUser> Users { get; }

        public UnitOfWork(AddressBookDBContext context)
        {
            _context = context;
            Entries = new Repositories.AddressBookRepository(context);
            Jobs = new Repositories.GenericRepository<Job>(context);
            Departments = new Repositories.GenericRepository<Department>(context);
            Users = new Repositories.GenericRepository<ApplicationUser>(context);
        }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
