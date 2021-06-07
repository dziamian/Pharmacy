using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

		public ICollection<Product> FavouriteProducts { get; set; }

        public ICollection<Rating> Ratings { get; set; }

		public ICollection<Order> Orders { get; set; }
	}
}
