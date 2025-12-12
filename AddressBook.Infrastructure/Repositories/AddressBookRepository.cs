using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressBook.Application.DTOs.AddressBookDTOs;
using AddressBook.Application.Interfaces;
using AddressBook.Domain.Entities;
using AddressBook.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.Infrastructure.Repositories
{
    public class AddressBookRepository : GenericRepository<AddressBookEntry>, IAddressBookRepository
    {
        public AddressBookRepository(AddressBookDBContext context) : base(context) { }

        public override async Task<AddressBookEntry?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(x => x.Job)
                .Include(x => x.Department)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<IEnumerable<AddressBookEntry>> GetAllAsync()
        {
            return await _dbSet
                .Include(x => x.Job)
                .Include(x => x.Department)
                .ToListAsync();
        }

        public async Task<PagedResult<AddressBookReadDto>> SearchAsync(AddressBookSearchQuery query, CancellationToken ct = default)
        {
            var dbQuery = _dbSet
                .Include(x => x.Job)
                .Include(x => x.Department)
                .AsNoTracking();

           
            if (!string.IsNullOrWhiteSpace(query.FullName))
                dbQuery = dbQuery.Where(x => x.FullName.Contains(query.FullName));

            if (query.JobId.HasValue)
                dbQuery = dbQuery.Where(x => x.JobId == query.JobId);

            if (query.DepartmentId.HasValue)
                dbQuery = dbQuery.Where(x => x.DepartmentId == query.DepartmentId);

            if (!string.IsNullOrWhiteSpace(query.Mobile))
                dbQuery = dbQuery.Where(x => x.MobileNumber.Contains(query.Mobile));

            if (!string.IsNullOrWhiteSpace(query.Email))
                dbQuery = dbQuery.Where(x => x.Email.Contains(query.Email));

            if (query.BirthFrom.HasValue)
                dbQuery = dbQuery.Where(x => x.DateOfBirth >= query.BirthFrom);

            if (query.BirthTo.HasValue)
                dbQuery = dbQuery.Where(x => x.DateOfBirth <= query.BirthTo);

            var totalCount = await dbQuery.CountAsync(ct);

            var items = await dbQuery
                .OrderBy(x => x.FullName)
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(x => new AddressBookReadDto
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    JobId = x.JobId,
                    JobName = x.Job != null ? x.Job.Name : null,
                    DepartmentId = x.DepartmentId,
                    DepartmentName = x.Department != null ? x.Department.Name : null,
                    MobileNumber = x.MobileNumber,
                    Address = x.Address,
                    Email = x.Email,
                    DateOfBirth = x.DateOfBirth,
                    Age=x.Age
                })
                .ToListAsync(ct);

            return new PagedResult<AddressBookReadDto>
            {
                Items = items,
                TotalCount = totalCount,
                Page = query.Page,
                PageSize = query.PageSize
            };
        }

    }
}