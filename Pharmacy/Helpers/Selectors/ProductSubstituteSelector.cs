using Pharmacy.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Helpers.Selectors
{
	public class ProductSubstituteSelector 
	{
		public float DosageInaccuracy { get; set; }
		public float Ratio { get; protected set; }
        public Product OriginalProduct { get; protected set; }
		public Product TestedProduct { get; protected set; }
        public bool SubstitutionPossible { get; protected set; }

		public ProductSubstituteSelector(float allowedInaccuracy = 0.0f)
		{
			this.DosageInaccuracy = allowedInaccuracy >= 0.0f ? allowedInaccuracy : 0.0f;
            this.OriginalProduct = null;
            this.TestedProduct = null;
            this.SubstitutionPossible = false;
		}

		public bool TestForSubstitution([AllowNull] Product original, [AllowNull] Product tested)
		{
            this.SubstitutionPossible = false;
            this.OriginalProduct = original;
            this.TestedProduct = tested;
            
            if (original == null || tested == null)
            {
                return false;
            }

            // Order collections by id so iterators can advance simultaneously and can be compared to each other instead of entire collections
            var actives1 = original.ActiveSubstances.OrderBy(p => p.ActiveSubstance.Id);
            var actives2 = tested.ActiveSubstances.OrderBy(p => p.ActiveSubstance.Id);
            /* 
             * Compute reference ratio which has to be met by all substance ratios
             * If P1 had substances A in dose 10 and B in dose 5, and P2 had substances A in dose 5 and B in dose 3,
             * such P2 cannot subsitute for A since consuming required dosage of both substances at a time could require
             * patient to consume much more of one or both of them than necessary
             */
            this.Ratio = ((float)actives1.First().Dose / (float)actives2.First().Dose);
            if (Ratio - MathF.Floor(Ratio) > this.DosageInaccuracy || Ratio < 1.0f)
            {
                return false;
            }
            // Make sure reference ratio is an integer in order to prevent inaccuracy creep
            Ratio = MathF.Floor(Ratio);

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
                if (MathF.Abs(ratio - Ratio) > this.DosageInaccuracy)
                {
                    return false;
                }
            }

            // All criteria met, substitution (perhaps) possible
            // Save last products in case user is interested in given pair
            return (this.SubstitutionPossible = true);
        }

	}
}
