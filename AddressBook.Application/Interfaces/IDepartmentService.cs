using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressBook.Application.DTOs.DepartmentDTOs;

namespace AddressBook.Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDTO>> GetAllAsync();
        Task<DepartmentDTO> GetByIdAsync(int id);
        Task<int> CreateAsync(DepartmentCreateDTO dto);
        Task UpdateAsync(int id, DepartmentUpdateDTO dto);
        Task DeleteAsync(int id);
    }
}
