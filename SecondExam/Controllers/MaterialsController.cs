namespace SecondExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
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
        /// <response code="403">Forbidden, no permission</response>
        /// <response code="401">Not logged in</response>

        [Authorize(Roles = "user,admin")]
        [HttpGet]
        [Route("")]
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
        /// <response code="403">Forbidden, no permission</response>
        /// <response code="401">Not logged in</response>

        [Authorize(Roles = "user,admin")]
        [HttpGet]
        [Route("{id}", Name = "GetMaterials")]
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
        /// <response code="403">Forbidden, no permission</response>
        /// <response code="401">Not logged in</response>

        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateMaterial(string title, string description, string location, int authorId, int typeId, DateTime date)
        {
            if(authorId == 0) return BadRequest("Invalid Author Id");
            if (await _repository.Authors.RetrieveAsync(authorId)==null) return BadRequest("Invalid Author Id");
            if(typeId == 0) return BadRequest("Invalid Type Id");
            if (await _repository.MaterialsTypes.RetrieveAsync(typeId)==null) return BadRequest("Invalid Type Id");
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
        ///         "materialTitle": "string",
        ///         "materialDescription": "string",
        ///         "materialLocation": "string",
        ///         "authorId": 0,
        ///         "materialTypeId": 0,
        ///         "createdDate": "2022-08-09"
        ///     }
        ///
        /// </remarks>
        /// <response code="204">When object was added</response>
        /// <response code="404">If object doesn't exist</response>
        /// <response code="403">Forbidden, no permission</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Not logged in</response>

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMaterial(int id, MaterialsUpdateDTO updateDto)
        {
            var modelFromRepo = await _repository.Materials.RetrieveAsync(id);
            if (modelFromRepo == null)
            {
                return NotFound();
            }
            if (updateDto.AuthorId == 0) return BadRequest("Invalid Author Id");
            if (await _repository.Authors.RetrieveAsync(updateDto.AuthorId) == null) return BadRequest("Invalid Author Id");
            if (updateDto.MaterialTypeId == 0) return BadRequest("Invalid Type Id");
            if (await _repository.MaterialsTypes.RetrieveAsync(updateDto.MaterialTypeId) == null) return BadRequest("Invalid Type Id");
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
        ///        "path": "materialTitle",
        ///        "value": "Type new title here"
        ///     }
        ///
        /// </remarks>
        /// <response code="204">No content</response>
        /// <response code="200">OK</response>
        /// <response code="400">If the item is null</response>
        /// <response code="403">Forbidden, no permission</response>
        /// <response code="401">Not logged in</response>

        [Authorize(Roles = "admin")]
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialEntityUpdateMaterial(int id, JsonPatchDocument<MaterialsUpdateDTOForPatch> patchDoc)
        {
            var modelFromRepo = await _repository.Materials.RetrieveAsync(id);
            if (modelFromRepo == null)
            {
                return NotFound();
            }
            var entityToPatch = _mapper.Map<MaterialsUpdateDTOForPatch>(modelFromRepo);
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
        /// <response code="403">Forbidden, no permission</response>
        /// <response code="401">Not logged in</response>

        [Authorize(Roles = "admin")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> RemoveMaterial(int id)
        {
            var result = await _repository.Materials.DeleteAsync(id);
            if (result == null) return NotFound();

            return NoContent();
        }
    }
}
