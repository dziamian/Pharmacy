using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Database.Entities
{
    public class Client
    {
        [Key]
        public string ClientId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
    }
}
