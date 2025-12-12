using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Domain.Entities
{
    public class AddressBookEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string FullName { get; set; } = null!;
        public int? JobId { get; set; }
        public int? DepartmentId { get; set; }

        [Required]
        [Phone(ErrorMessage = "Please enter a valid mobile number")]
        public string MobileNumber { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(500)]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; } = string.Empty;
        public string? PhotoFileName { get; set; }      // stored file name in wwwroot/uploads

        public int Age => CalculateAge(DateOfBirth);
        public bool IsDeleted { get; set; } = false;

        [ForeignKey("DepartmentId")]
        public virtual Department? Department { get; set; }

        [ForeignKey("JobId")]
        public virtual Job? Job { get; set; }



        #region Age Calculation Helper
        private static int CalculateAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
        #endregion
    }
}
