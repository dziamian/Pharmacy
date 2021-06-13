using Pharmacy.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Pharmacy.Models.Database.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public bool Supplement { get; set; }

        [Required]
        public int Cost { get; set; }

        [Required]
        public long Supply { get; set; }

        [Url]
        [Required]
        [MaxLength(1024)]
        public string Image { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Description { get; set; }

        [Required]
        [ForeignKey(nameof(Category))]
		public int CategoryId { get; set; }
        [Required]
		public Category Category { get; set; }

		[Required]
		public DateTime CreationDate { get; set; }

		public ICollection<ProductActiveSubstance> ActiveSubstances { get; set; }

        public ICollection<ProductPassiveSubstance> PassiveSubstances { get; set; }

		public ICollection<Rating> Ratings { get; set; }

		public bool IsSubstitutedBy([AllowNull] Product product, float dosageInaccuracy)
		{
            // Self-explanatory
            if (product == null)
			{
                return false;
			}

            // If dosage inaccuracy was not specified, default to perfect precision
            if (dosageInaccuracy < 0.0f)
			{
                dosageInaccuracy = 0.0f;
			}

            // Order collections by id so iterators can advance simultaneously and can be compared to each other instead of entire collections
			var actives1 = this.ActiveSubstances.OrderBy(p => p.ActiveSubstance.Id);
            var actives2 = product.ActiveSubstances.OrderBy(p => p.ActiveSubstance.Id);
            /* 
             * Compute reference ratio which has to be met by all substance ratios
             * If P1 had substances A in dose 10 and B in dose 5, and P2 had substances A in dose 5 and B in dose 3,
             * such P2 cannot subsitute for A since consuming required dosage of both substances at a time could require
             * patient to consume much more of one or both of them than necessary
             */
            var referenceRatio = ((float)actives1.First().Dose / (float)actives2.First().Dose);
            if (referenceRatio - MathF.Floor(referenceRatio) > dosageInaccuracy || referenceRatio < 1.0f)
			{
                return false;
			}
            // Make sure reference ratio is an integer in order to prevent inaccuracy creep
            referenceRatio = MathF.Floor(referenceRatio);
            
            for (
                IEnumerator<ProductActiveSubstance> e1 = actives1.GetEnumerator(), e2 = actives2.GetEnumerator();
                e1.MoveNext() && e2.MoveNext();)
			{
                // Different substances - cannot substitute
                if (!Equals(e1.Current.ActiveSubstance, e2.Current.ActiveSubstance))
				{
                    return false;
				}

                var ratio = (float)e1.Current.Dose / (float)e2.Current.Dose;
                // Ratio differences overstep inaccuracy - cannot substitute
                if (MathF.Abs(ratio - referenceRatio) > dosageInaccuracy)
				{
                    return false;
				}
			}

            // All criteria met, substitution (perhaps) possible
            return true;
		}
	}
}
