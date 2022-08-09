using SecondExam.Data.Utils;
using SecondExam.DTOs;

namespace SecondExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //  [Authorize(AuthenticationSchemes = "Bearer")]
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;
        public AdminController(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Add new admin
        /// </summary>
        /// <returns>Add new admin</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     username: new admin login
        ///     password: new admin password
        ///
        /// </remarks>
        /// <response code="201">Created</response>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        [HttpPost]
        [Route("AddNewAdmin")]
        public async Task<IActionResult> CreateAdmin(string username, string password)
        {
            var hashedLogin = Hashing.ComputeSha256Hash(username);
            var hashedPassword = Hashing.ComputeSha256Hash(password);
            var createdCredentials = await _repository.Users.CreateCredentialsAsync(new Credentials() { Login = hashedLogin, Password = hashedPassword});
            if (createdCredentials == null) return BadRequest();
            var createdUser = await _repository.Users.CreateAsync(new User() { CredentialsID = createdCredentials.CredentialsID, Role = Role.admin});
            if (createdUser == null) return BadRequest();

            var entity = await _repository.Users.RetrieveAsync(createdUser.UserId);
            var readDto = _mapper.Map<UserCreateDto>(entity);
            return CreatedAtRoute(nameof(GetUser), new { Id = readDto.UserId }, readDto);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var entity = await _repository.Users.RetrieveAsync(id);
            if (entity == null) return NotFound();
            return Ok(_mapper.Map<UserCreateDto>(entity));
        }

        /// <summary>
        /// Get author materials by id with rating above 5
        /// </summary>
        /// <returns>Get author materials by id with rating above 5</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     5
        ///
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [HttpGet]
        [Route("AuthorPublications{id}")]
        public async Task<IActionResult> GetAuthorMaterialsWithRatingAbove5(int id)
        {
            var entity = await _repository.Authors.RetrieveAsyncWithPublications(id);
            if (entity == null) return NotFound();
            var publications = await _repository.Materials.RetrieveAllWithRatingAbove5Async(entity);
            return Ok(_mapper.Map<IList<MaterialsGetDTO>>(publications));
        }

        /// <summary>
        /// Get publications for specific type
        /// </summary>
        /// <returns>Get publications for specific type</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     5
        ///
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [HttpGet]
        [Route("MaterialsByType{id}")]
        public async Task<IActionResult> GetPublicationsForSpecificType(int id)
        {
            var entity = await _repository.Materials.RetrieveAllWithinType(id);
            if (entity == null) return NotFound();
            return Ok(_mapper.Map<IList<MaterialsGetDTO>>(entity));
        }
        /// <summary>
        /// Get all Reviews
        /// </summary>
        /// <returns>Get all Reviews</returns>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("Reviews/")]
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

        [HttpGet]
        [Route("Reviews/{id}", Name = "GetReviews")]
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
        /// <param name="MaterialId"></param>
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
        [HttpPost]
        [Route("Reviews/")]
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
                return ValidationProblem(ModelState);
            }
            else
            {
                await _repository.Reviews.UpdateAsync(createdEntity);
                material.MaterialReviews.Add(createdEntity);
                await _repository.Materials.UpdateAsync(material);
            }
            var entity = await _repository.Reviews.RetrieveAsync(createdEntity.ReviewId);
            var readDto = _mapper.Map<ReviewsGetSimpleDTO>(entity);
            return CreatedAtRoute(nameof(GetReviews), new { Id = readDto.ReviewId }, readDto);
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
        /// <response code="204">When actor was added</response>
        /// <response code="404">If any object doesn't exist</response>
        [HttpPut("Reviews/{id}")]
        public async Task<ActionResult> UpdateReview(int id, ReviewsUpdateDTO updateDto)
        {
            var modelFromRepo = await _repository.Reviews.RetrieveAsync(id);
            if (modelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(updateDto, modelFromRepo);
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

        [HttpPatch("Reviews/{id}")]
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
        [HttpDelete]
        [Route("Reviews/{id}")]
        public async Task<IActionResult> RemoveReview(int id)
        {
            var result = await _repository.Reviews.DeleteAsync(id);
            if (result == null) return NotFound();

            return NoContent();
        }
    }
}
