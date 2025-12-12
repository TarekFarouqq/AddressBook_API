using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressBook.Application.DTOs.AddressBookDTOs;
using AddressBook.Application.Interfaces;
using AddressBook.Domain.Entities;

namespace AddressBook.Application.Services
{
    public class AddressBookService : IAddressBookService
    {

        private readonly IUnitOfWork _uow;

        public AddressBookService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> CreateAsync(AddressBookCreateDto dto)
        {
            var entity = MapToNewEntity(dto);
            await _uow.Entries.AddAsync(entity);
            await _uow.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var e = await _uow.Entries.GetByIdAsync(id);
            if (e is null) throw new KeyNotFoundException();

            _uow.Entries.Remove(e);
            await _uow.SaveChangesAsync();
        }

        public async Task<IEnumerable<AddressBookReadDto>> GetAllAsync()
        {
            var list = await _uow.Entries.GetAllAsync();
            return list.Select(MapToDto);
        }

        public async Task<AddressBookReadDto?> GetByIdAsync(int id)
        {
            var e = await _uow.Entries.GetByIdAsync(id);
            return e == null ? null : MapToDto(e);
        }

        public async Task UpdateAsync(int id, AddressBookReadDto dto)
        {
            var e = await _uow.Entries.GetByIdAsync(id);
            if (e is null) throw new KeyNotFoundException();

            MapToEntity(dto, e);

            _uow.Entries.Update(e);
            await _uow.SaveChangesAsync();
        }

        public async Task<byte[]> ExportToExcelAsync()
        {
            var entries = await _uow.Entries.GetAllAsync();

            using var wb = new ClosedXML.Excel.XLWorkbook();
            var ws = wb.Worksheets.Add("Entries");

           
            ws.Cell(1, 1).Value = "Full Name";
            ws.Cell(1, 2).Value = "Email";
            ws.Cell(1, 3).Value = "Mobile";
            ws.Cell(1, 4).Value = "Birth Date";

            int r = 2;
            foreach (var e in entries)
            {
                ws.Cell(r, 1).Value = e.FullName;
                ws.Cell(r, 2).Value = e.Email;
                ws.Cell(r, 3).Value = e.MobileNumber;
                ws.Cell(r, 4).Value = e.DateOfBirth.ToShortDateString();
                r++;
            }

            using var ms = new MemoryStream();
            wb.SaveAs(ms);
            return ms.ToArray();
        }

        public async Task<PagedResult<AddressBookReadDto>> SearchAsync( AddressBookSearchQuery query,CancellationToken ct = default)
        {
            return await _uow.Entries.SearchAsync(query, ct);
        }




        #region Mapping Helpers
        private AddressBookReadDto MapToDto(AddressBookEntry e)
        {
            return new AddressBookReadDto
            {
                Id = e.Id,
                FullName = e.FullName,
                JobId = e.JobId,
                JobName = e.Job?.Name,
                DepartmentId = e.DepartmentId,
                DepartmentName = e.Department?.Name,
                MobileNumber = e.MobileNumber,
                DateOfBirth = e.DateOfBirth,
                Address = e.Address,
                Email = e.Email,
                PhotoFileName = e.PhotoFileName,
                Age = e.Age
            };
        }

        private void MapToEntity(AddressBookReadDto dto, AddressBookEntry e)
        {
            e.FullName = dto.FullName;
            e.JobId = dto.JobId;
            e.DepartmentId = dto.DepartmentId;
            e.MobileNumber = dto.MobileNumber;
            e.DateOfBirth = dto.DateOfBirth;
            e.Address = dto.Address;
            e.Email = dto.Email;
            //e.PhotoFileName = dto.PhotoFileName;
        }

        private AddressBookEntry MapToNewEntity(AddressBookCreateDto dto)
        {
            return new AddressBookEntry
            {
                FullName = dto.FullName,
                JobId = dto.JobId,
                DepartmentId = dto.DepartmentId,
                MobileNumber = dto.MobileNumber,
                DateOfBirth = dto.DateOfBirth,
                Address = dto.Address,
                Email = dto.Email,
                //PhotoFileName = dto.PhotoFileName
            };
        }
        #endregion
    }

}

