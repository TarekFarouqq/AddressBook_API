using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressBook.Application.DTOs.DepartmentDTOs;
using AddressBook.Application.DTOs.JobDTOs;

namespace AddressBook.Application.Interfaces
{
    public interface IJobService
    {
        Task<IEnumerable<JobDto>> GetAllAsync();
        Task<JobDto> GetByIdAsync(int id);

        Task<int> CreateAsync(JobCreateDTO dto);
        Task UpdateAsync(int id, JobUpdateDTO dto);
        Task DeleteAsync(int id);
    }
}
