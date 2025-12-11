using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AddressBook.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.Infrastructure.DBContext
{
    public class AddressBookDBContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<AddressBookEntry> Entries { get; set; } = null!;
        public DbSet<Job> Jobs { get; set; } = null!;
        public DbSet<Department> Departments { get; set; } = null!;



        public AddressBookDBContext() : base()
        {

        }

        public AddressBookDBContext(DbContextOptions<AddressBookDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
