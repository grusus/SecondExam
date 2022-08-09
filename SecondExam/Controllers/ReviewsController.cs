namespace SecondExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class Reviews : Controller
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;
        public Reviews(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all Reviews
        /// </summary>
        /// <returns>Get all Reviews</returns>
        /// <response code="200">OK</response>
        /// <response code="403">Forbidden, no permission</response>
        /// <response code="401">Not logged in</response>

        [Authorize(Roles = "user,admin")]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllReviews()
        {
            var entities = await _repository.Reviews.RetrieveAllAsync();
            return Ok(_mapper.Map<IList<ReviewsGetSimpleDTO>>(entities));
        }

        /// <summary>
        /// Get Review by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Get Review by id</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     5
        ///
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        /// <response code="403">Forbidden, no permission</response>
        /// <response code="401">Not logged in</response>

        [Authorize(Roles = "user,admin")]
        [HttpGet]
        [Route("{id}", Name = "GetReviews")]
        public async Task<IActionResult> GetReviews(int id)
        {
            var entity = await _repository.Reviews.RetrieveAsync(id);
            if (entity == null) return NotFound();
            return Ok(_mapper.Map<ReviewsGetSimpleDTO>(entity));
        }

        /// <summary>
        /// Add new Review
        /// </summary>
        /// <param name="textReview"></param>
        /// <param name="digitReview"></param>
        /// <param name="materialId"></param>
        /// <returns>Add new Review</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "textReview": "Your review",
        ///        "digitReview": "from 1 to 10",
        ///        "materialId": "2"
        ///     }
        /// </remarks>
        /// <response code="201">Created</response>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="403">Forbidden, no permission</response>
        /// <response code="401">Not logged in</response>

        [Authorize(Roles = "user,admin")]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateReview(string textReview, int digitReview, int materialId)
        {
            var material = await _repository.Materials.RetrieveAsyncWithDetails(materialId);
            if (material == null) return NotFound();
            var createdEntity = await _repository.Reviews.CreateAsync(new Review()
            {
                TextReview = textReview,
                DigitReview = digitReview,
                MaterialId = material.MaterialId
            });

            if (createdEntity == null) return BadRequest();
            createdEntity.ReviewReference = "https://localhost:7173/api/User/Review/" + createdEntity.ReviewId;
            if (!TryValidateModel(createdEntity))
            {
                await _repository.Reviews.DeleteAsync(createdEntity.ReviewId);
                return ValidationProblem(ModelState);
            }
            else
            {
                await _repository.Reviews.UpdateAsync(createdEntity);
                material.MaterialReviews.Add(createdEntity);
                await _repository.Materials.UpdateAsync(material);

                var entity = await _repository.Reviews.RetrieveAsync(createdEntity.ReviewId);
                var readDto = _mapper.Map<ReviewsGetSimpleDTO>(entity);
                return CreatedAtRoute(nameof(GetReviews), new { Id = readDto.ReviewId }, readDto);
            }
        }

        /// <summary>
        /// Update review
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Update Review </returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT
        ///     {
        ///         "reviewReference": "string",
        ///         "textReview": "string",
        ///         "digitReview": 10
        ///     }
        ///
        /// </remarks>
        /// <response code="204">When object was added</response>
        /// <response code="404">If any object doesn't exist</response>
        /// <response code="403">Forbidden, no permission</response>
        /// <response code="401">Not logged in</response>

        [Authorize(Roles = "user,admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateReview(int id, ReviewsUpdateDTO updateDto)
        {
            var modelFromRepo = await _repository.Reviews.RetrieveAsync(id);
            if (modelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(updateDto, modelFromRepo);
            if (!TryValidateModel(modelFromRepo))
            {
                return ValidationProblem(ModelState);
            }
            await _repository.Reviews.UpdateAsync(modelFromRepo);
            return NoContent();
        }

        /// <summary>
        /// Update Review
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDoc"></param>
        /// <returns>Update Review </returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///        "op": "replace",
        ///        "path": "TextReview",
        ///        "value": "Type new review here"
        ///     }
        ///
        /// </remarks>
        /// <response code="204">No content</response>
        /// <response code="200">OK</response>
        /// <response code="400">If the item is null</response>
        /// <response code="403">Forbidden, no permission</response>
        /// <response code="401">Not logged in</response>

        [Authorize(Roles = "user,admin")]
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialEntityUpdateReview(int id, JsonPatchDocument<ReviewsUpdateDTO> patchDoc)
        {
            var modelFromRepo = await _repository.Reviews.RetrieveAsync(id);
            if (modelFromRepo == null)
            {
                return NotFound();
            }
            var entityToPatch = _mapper.Map<ReviewsUpdateDTO>(modelFromRepo);
            patchDoc.ApplyTo(entityToPatch, ModelState);
            if (!TryValidateModel(entityToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(entityToPatch, modelFromRepo);
            await _repository.Reviews.UpdateAsync(modelFromRepo);
            return NoContent();
        }

        /// <summary>
        /// Delete review
        /// </summary>
        /// <returns>Delete review</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     23
        ///
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        /// <response code="403">Forbidden, no permission</response>
        /// <response code="401">Not logged in</response>

        [Authorize(Roles = "admin")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> RemoveReview(int id)
        {
            var result = await _repository.Reviews.DeleteAsync(id);
            if (result == null) return NotFound();

            return NoContent();
        }
    }
}
