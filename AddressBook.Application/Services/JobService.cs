using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressBook.Application.DTOs.DepartmentDTOs;
using AddressBook.Application.DTOs.JobDTOs;
using AddressBook.Application.Interfaces;
using AddressBook.Domain.Entities;

namespace AddressBook.Application.Services
{
    public class JobService: IJobService
    {
        private readonly IUnitOfWork _uow;

        public JobService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> CreateAsync(JobCreateDTO dto)
        {
            var job = new Job { Name = dto.Name };
            await _uow.Jobs.AddAsync(job);
            await _uow.SaveChangesAsync();
            return job.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var job = await _uow.Jobs.GetByIdAsync(id);
            if (job == null) throw new KeyNotFoundException();
            _uow.Jobs.Remove(job);
            await _uow.SaveChangesAsync();
        }

        public async Task<IEnumerable<JobDto>> GetAllAsync()
        {
            var list = await _uow.Jobs.GetAllAsync();
            return list.Select(j => new JobDto { Id = j.Id, Name = j.Name });
        }

        public async Task<JobDto> GetByIdAsync(int id)
        {
            var job = await _uow.Jobs.GetByIdAsync(id);
            if (job == null) throw new KeyNotFoundException();

            return new JobDto
            {
                Id = job.Id,
                Name = job.Name
            };
        }

        public async Task UpdateAsync(int id, JobUpdateDTO dto)
        {
            var job = await _uow.Jobs.GetByIdAsync(id);
            if (job == null) throw new KeyNotFoundException();
            job.Name = dto.Name;
            _uow.Jobs.Update(job);
            await _uow.SaveChangesAsync();
        }
    }
}
