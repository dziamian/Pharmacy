using Pharmacy.Models.Data_Transfrom_Objects.Rating;
using Pharmacy.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Converters
{
    public static class RatingConverter
    {
        public static RatingDto ToRatingDto(Rating rating)
        {
            return new RatingDto {
                ProductId = rating.ProductId,
                Score = rating.Score
            };
        }

        public static IEnumerable<RatingDto> ToRatingDtos(IEnumerable<Rating> ratings)
        {
            if (ratings == null)
            {
                return null;
            }

            var collection = new List<RatingDto>();

            foreach (var it in ratings)
            {
                collection.Add(ToRatingDto(it));
            }

            return collection;
        }
    }
}
