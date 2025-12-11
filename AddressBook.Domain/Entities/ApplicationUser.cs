using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AddressBook.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string FirstName { get; set; } = null!;
        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string LastName { get; set; } = null!;
        public DateTime? LastModification { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 5)]
        public string Address { get; set; } = null!;
        public DateTime CreatedIn { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
      
}
}
