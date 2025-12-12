using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Application.DTOs.AddressBookDTOs
{
    public class AddressBookSearchQuery
    {
        public string? FullName { get; set; }
        public int? JobId { get; set; }
        public int? DepartmentId { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public DateTime? BirthFrom { get; set; }
        public DateTime? BirthTo { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
