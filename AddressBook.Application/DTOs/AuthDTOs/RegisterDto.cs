using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Application.DTOs.AuthDTOs
{
    public class RegisterDto
    {
        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string UserName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [Phone]
        [StringLength(20)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [StringLength(200, MinimumLength = 5)]
        public string Address { get; set; } = null!;

        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
