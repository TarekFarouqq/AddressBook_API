using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressBook.Application.DTOs.DepartmentDTOs;
using AddressBook.Application.Interfaces;
using AddressBook.Domain.Entities;

namespace AddressBook.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _uow;

        public DepartmentService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> CreateAsync(DepartmentCreateDTO dto)
        {
            var dept = new Department { Name = dto.Name };
            await _uow.Departments.AddAsync(dept);
            await _uow.SaveChangesAsync();
            return dept.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var dept = await _uow.Departments.GetByIdAsync(id);
            if (dept == null) throw new KeyNotFoundException();

            var hasAddressBooks = await _uow.Entries.AnyAsync(a => a.DepartmentId == id && !a.IsDeleted);

            if (hasAddressBooks)
                throw new InvalidOperationException(
                    "Cannot delete department because it contains address book entries."
                );

            _uow.Departments.Remove(dept);
            await _uow.SaveChangesAsync();
        }

        public async Task<IEnumerable<DepartmentDTO>> GetAllAsync()
        {
            var list = await _uow.Departments.GetAllAsync();
            return list.Select(d => new DepartmentDTO
            {
                Id = d.Id,
                Name = d.Name
            });
        }

        public async Task<DepartmentDTO> GetByIdAsync(int id)
        {
            var dept = await _uow.Departments.GetByIdAsync(id);
            if (dept == null) throw new KeyNotFoundException();
            return new DepartmentDTO
            {
                Id = dept.Id,
                Name = dept.Name
            };
        }

        public async Task UpdateAsync(int id, DepartmentUpdateDTO dto)
        {
            var dept = await _uow.Departments.GetByIdAsync(id);
            if (dept == null) throw new KeyNotFoundException();

            dept.Name = dto.Name;

            _uow.Departments.Update(dept);
            await _uow.SaveChangesAsync();
        }


    }
}