namespace SecondExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Authorize(Roles = "admin")]
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
        /// <response code="403">Forbidden, no permission</response>
        /// <response code="401">Not logged in</response>

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
        /// <response code="403">Forbidden, no permission</response>
        /// <response code="401">Not logged in</response>

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
        /// <response code="403">Forbidden, no permission</response>
        /// <response code="401">Not logged in</response>

        [HttpGet]
        [Route("MaterialsByType{id}")]
        public async Task<IActionResult> GetPublicationsForSpecificType(int id)
        {
            var entity = await _repository.Materials.RetrieveAllWithinType(id);
            if (entity == null) return NotFound();
            return Ok(_mapper.Map<IList<MaterialsGetDTO>>(entity));
        }

        /// <summary>
        /// Get most productive author
        /// </summary>
        /// <returns>Get most productive author</returns>
        /// <response code="200">OK</response>
        /// <response code="403">Forbidden, no permission</response>
        /// <response code="401">Not logged in</response>

        [HttpGet]
        [Route("Authors/")]
        public async Task<IActionResult> GetMostProductiveAuthor()
        {
            var entities = await _repository.Authors.RetrieveMostProductiveAuthor();
            return Ok(_mapper.Map<AuthorGetFullDTO>(entities));
        }
    }
}
