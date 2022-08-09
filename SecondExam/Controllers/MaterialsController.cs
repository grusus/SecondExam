namespace SecondExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //  [Authorize(AuthenticationSchemes = "Bearer")]
    public class MaterialsController : Controller
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;
        public MaterialsController(IUnitOfWork repository, IMapper mapper)
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
        [Route("Materials/{id}", Name = "GetMaterials")]
        public async Task<IActionResult> GetMaterials(int id)
        {
            var entity = await _repository.Materials.RetrieveAsyncWithDetails(id);
            if (entity == null) return NotFound();
            return Ok(_mapper.Map<MaterialsGetFullDTO>(entity));
        }

        /// <summary>
        /// Add new Material
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="location"></param>
        /// <param name="authorId"></param>
        /// <param name="typeId"></param>
        /// <param name="date"></param>
        /// <returns>Add new Material</returns>
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
        [Route("Materials/")]
        public async Task<IActionResult> CreateMaterial(string title, string description, string location, int authorId, int typeId, DateTime date)
        {
            var createdEntity = await _repository.Materials.CreateAsync(new Material()
            {
                MaterialTitle = title,
                MaterialDescription = description,
                MaterialLocation = location,
                AuthorId = authorId,
                MaterialTypeId = typeId,
                CreatedDate = date
            });

            if (createdEntity == null) return BadRequest();
            if (!TryValidateModel(createdEntity))
            {
                await _repository.Materials.DeleteAsync(createdEntity.MaterialId);
                return ValidationProblem(ModelState);
            }

            var entity = await _repository.Materials.RetrieveAsync(createdEntity.MaterialId);
            var readDto = _mapper.Map<MaterialsCreateDto>(entity);
            return CreatedAtRoute(nameof(GetMaterials), new { Id = readDto.MaterialId }, readDto);
        }
        /// <summary>
        /// Update Material
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Update Material </returns>
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
        [HttpPut("Materials/{id}")]
        public async Task<ActionResult> UpdateMaterial(int id, MaterialsCreateDto updateDto)
        {
            var modelFromRepo = await _repository.Materials.RetrieveAsync(id);
            if (modelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(updateDto, modelFromRepo);
            if (!TryValidateModel(modelFromRepo))
            {
                return ValidationProblem(ModelState);
            }
            await _repository.Materials.UpdateAsync(modelFromRepo);
            return NoContent();
        }

        /// <summary>
        /// Update Material
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDoc"></param>
        /// <returns>Update Material </returns>
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

        [HttpPatch("Materials/{id}")]
        public async Task<ActionResult> PartialEntityUpdateMaterial(int id, JsonPatchDocument<MaterialsCreateDto> patchDoc)
        {
            var modelFromRepo = await _repository.Materials.RetrieveAsync(id);
            if (modelFromRepo == null)
            {
                return NotFound();
            }
            var entityToPatch = _mapper.Map<MaterialsCreateDto>(modelFromRepo);
            patchDoc.ApplyTo(entityToPatch, ModelState);
            if (!TryValidateModel(entityToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(entityToPatch, modelFromRepo);
            await _repository.Materials.UpdateAsync(modelFromRepo);
            return NoContent();
        }

        /// <summary>
        /// Delete Material
        /// </summary>
        /// <returns>Delete Material</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     23
        ///
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [HttpDelete]
        [Route("Materials/{id}")]
        public async Task<IActionResult> RemoveMaterial(int id)
        {
            var result = await _repository.Materials.DeleteAsync(id);
            if (result == null) return NotFound();

            return NoContent();
        }
    }
}
