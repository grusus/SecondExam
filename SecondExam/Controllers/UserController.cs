using Microsoft.AspNetCore.JsonPatch;
using SecondExam.DTOs.ReviewsDTOs;
using SecondExam.DTOs.TypesDTOs;

namespace SecondExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  //  [Authorize(AuthenticationSchemes = "Bearer")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;
        public UserController(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all Materials
        /// </summary>
        /// <returns>Get all Materials</returns>
        /// <response code="200">OK</response>

        [HttpGet]
        [Route("Materials/")]
        public async Task<IActionResult> GetAllMaterials()
        {
            var entities = await _repository.Materials.RetrieveAllAsync();
            return Ok(_mapper.Map<IList<MaterialsGetDTO>>(entities));
        }

        /// <summary>
        /// Get material by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Get material by id</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     5
        ///
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>

        [HttpGet]
        [Route("Materials/{id}", Name = "GetMaterial")]
        public async Task<IActionResult> GetMaterial(int id)
        {
            var entity = await _repository.Materials.RetrieveAsyncWithDetails(id);
            if (entity == null) return NotFound();
            return Ok(_mapper.Map<MaterialsGetFullDTO>(entity));
        }

        /// <summary>
        /// Get all Types
        /// </summary>
        /// <returns>Get all Types</returns>
        /// <response code="200">OK</response>

        [HttpGet]
        [Route("Types/")]
        public async Task<IActionResult> GetAllTypes()
        {
            var entities = await _repository.MaterialsTypes.RetrieveAllAsync();
            return Ok(_mapper.Map<IList<TypesGetDTO>>(entities));
        }

        /// <summary>
        /// Get Types by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Get Types by id</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     5
        ///
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>

        [HttpGet]
        [Route("Types/{id}", Name = "GetTypes")]
        public async Task<IActionResult> GetTypes(int id)
        {
            var entity = await _repository.MaterialsTypes.RetrieveAsync(id);
            if (entity == null) return NotFound();
            return Ok(_mapper.Map<TypesGetFullDTO>(entity));
        }
        /// <summary>
        /// Get all Authors
        /// </summary>
        /// <returns>Get all Authors</returns>
        /// <response code="200">OK</response>

        [HttpGet]
        [Route("Authors/")]
        public async Task<IActionResult> GetAllAuthors()
        {
            var entities = await _repository.Authors.RetrieveAllAsync();
            return Ok(_mapper.Map<IList<AuthorGetDTOwithId>>(entities));
        }

        /// <summary>
        /// Get Authors by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Get Authors by id</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     5
        ///
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>



        [HttpGet]
        [Route("Authors/{id}", Name = "GetAuthors")]
        public async Task<IActionResult> GetAuthors(int id)
        {
            var entity = await _repository.Authors.RetrieveAsyncWithPublications(id);
            if (entity == null) return NotFound();
            return Ok(_mapper.Map<AuthorGetFullDto>(entity));
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
        [Route("Reviews/{id}", Name = "GetReview")]
        public async Task<IActionResult> GetReview(int id)
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
        [Route("Review/")]
        public async Task<IActionResult> Create(string textReview, int digitReview, int materialId)
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
            return CreatedAtRoute(nameof(GetReview), new { Id = readDto.ReviewId }, readDto);
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
        public async Task<ActionResult> PartialEntityUpdate(int id, JsonPatchDocument<ReviewsReadDTO> patchDoc)
        {
            var modelFromRepo = await _repository.Reviews.RetrieveAsync(id);
            if (modelFromRepo == null)
            {
                return NotFound();
            }
            var entityToPatch = _mapper.Map<ReviewsReadDTO>(modelFromRepo);
            patchDoc.ApplyTo(entityToPatch, ModelState);
            if (!TryValidateModel(entityToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(entityToPatch, modelFromRepo);
            await _repository.Reviews.UpdateAsync(modelFromRepo);
            return NoContent();
        }
    }
}
