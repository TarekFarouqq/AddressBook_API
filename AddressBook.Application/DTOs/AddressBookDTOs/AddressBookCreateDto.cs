using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace AddressBook.Application.DTOs.AddressBookDTOs
{
    public class AddressBookCreateDto
    {
        

            [Required(ErrorMessage = "Full name is required")]
            [StringLength(100, MinimumLength = 2, ErrorMessage = "Full name must be between 2 and 100 characters")]
            public string FullName { get; set; } = null!;
            public int? JobId { get; set; }
            public int? DepartmentId { get; set; }

            [Required(ErrorMessage = "Mobile number is required")]
            [Phone(ErrorMessage = "Please enter a valid mobile number")]
            public string MobileNumber { get; set; } = null!;

            [Required(ErrorMessage = "Date of birth is required")]
            [DataType(DataType.Date)]
            [Range(typeof(DateTime), "1900-01-01", "2010-12-31",
                ErrorMessage = "Date of birth must be between 1900 and 2007")]
            public DateTime DateOfBirth { get; set; }

            [Required(ErrorMessage = "Address is required")]
            [StringLength(500, ErrorMessage = "Address cannot exceed 500 characters")]
            public string Address { get; set; } = string.Empty;

            [Required(ErrorMessage = "Email is required")]
            [EmailAddress(ErrorMessage = "Please enter a valid email address")]
            [StringLength(255)]
            public string Email { get; set; } = string.Empty;


            //[StringLength(255, ErrorMessage = "Photo filename too long")]
            //[Display(Name = "Photo")]
            //public string? PhotoFileName { get; set; }


        }
}
