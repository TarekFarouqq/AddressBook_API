using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Application.DTOs.AddressBookDTOs
{
    public class AddressBookReadDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public int? JobId { get; set; }
        public string? JobName { get; set; }
        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public string MobileNumber { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhotoFileName { get; set; }
        public int Age { get; set; }
    }
}
