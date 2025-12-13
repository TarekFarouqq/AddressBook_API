using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AddressBook.Application.DTOs.AddressBookDTOs
{
    public class AddressBookCreateRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string FullName { get; set; } = null!;

        public int? JobId { get; set; }
        public int? DepartmentId { get; set; }

        [Required]
        [Phone]
        public string MobileNumber { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(500)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;

        
        public IFormFile? Photo { get; set; }
    }
}
