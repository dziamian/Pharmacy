using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Controllers.BaseControllers;
using Pharmacy.Models.Converters;
using Pharmacy.Models.Data_Transfrom_Objects.Rating;
using Pharmacy.Models.Database.Entities;
using Pharmacy.Models.Database.Repositories.Interfaces;

namespace Pharmacy.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : AuthControllerBase
    {
        private readonly IRatingsRepo _ratingsRepo;

        public RatingsController(IRatingsRepo ratingsRepo)
        {
            _ratingsRepo = ratingsRepo;
        }

        [HttpPost]
        public async Task<ActionResult> CreateRating([FromBody] RatingDto dto)
        {
            var uid = GetUID();

            var createdRating = await _ratingsRepo.GetRating(uid, dto.ProductId);
            if (createdRating != null)
            {
                return Conflict();
            }

            Rating rating = new Rating {
                ClientId = uid,
                ProductId = dto.ProductId,
                Score = dto.Score
            };

            await _ratingsRepo.CreateRating(rating);
            await _ratingsRepo.SaveChanges();

            return CreatedAtRoute(nameof(GetRating), new { id = dto.ProductId }, null);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RatingDto>>> GetAllRatings()
        {
            return Ok(RatingConverter.ToRatingDtos(await _ratingsRepo.GetAllRatings(GetUID())));
        }

        [HttpGet("{id}", Name = nameof(GetRating))]
        public async Task<ActionResult<RatingDto>> GetRating(int id)
        {
            var uid = GetUID();

            var rating = await _ratingsRepo.GetRating(uid, id);

            if (rating == null)
            {
                return NotFound();
            }

            return Ok(RatingConverter.ToRatingDto(rating));
        }

        [AllowAnonymous]
        [HttpGet("{id}/all")]
        public async Task<ActionResult<IEnumerable<RatingDto>>> GetAllProductRatings(int id)
        {
            return Ok(RatingConverter.ToRatingDtos(await _ratingsRepo.GetAllProductRatings(id)));
        }

        [AllowAnonymous]
        [HttpGet("{id}/average")]
        public async Task<ActionResult<double>> GetAverageRating(int id)
        {
            var ratingAverage = await _ratingsRepo.GetAverageRating(id);

            if (ratingAverage == -1d)
            {
                return NotFound();
            }

            return Ok(ratingAverage);
        }

        [HttpDelete("{id}/remove")]
        public async Task<ActionResult> RemoveRating(int id)
        {
            var uid = GetUID();
            var result = await _ratingsRepo.RemoveRating(uid, id);
            
            if (!result)
            {
                return NotFound("Rating not found.");
            }

            await _ratingsRepo.SaveChanges();

            return Ok("Successfully removed rating.");
        }
    }
}
