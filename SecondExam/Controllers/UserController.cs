namespace SecondExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Authorize(Roles = "user")]
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
            return Ok(_mapper.Map<AuthorGetFullDTO>(entity));
        }
    }
}
