using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Domain.Entities
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [StringLength(250)]
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<AddressBookEntry> Entries { get; set; } = new List<AddressBookEntry>();
    }
}
