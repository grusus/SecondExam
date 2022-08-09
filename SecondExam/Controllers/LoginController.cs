namespace SecondExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly IUnitOfWork _repository;
        public LoginController(IConfiguration config, IUnitOfWork repository)
        {
            _configuration = config;
            _repository = repository;
        }
        /// <summary>
        /// Upon a successful login, retrieves a JWT token for you to authorize
        /// </summary>
        /// <returns>JWT Token</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "Id: "",
        ///        "Login": "",
        ///        "Password": "",
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If Login is successful</response>
        /// <response code="401">If the item is null</response>
        [HttpPost]

        public async Task<IActionResult> Post(int id, string login, string password)
        {
            if (login != null && password != null)
            {
                var user = await GetUser(id);
                var hashedLogin = Hashing.ComputeSha256Hash(login);
                var hashedPassword = Hashing.ComputeSha256Hash(password);
                if (user != null && user.Credentials.Login == hashedLogin && user.Credentials.Password == hashedPassword)
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.UserId.ToString()),
                        new Claim("CredentialsID", user.CredentialsID.ToString()),
                        new Claim("Login", user.Credentials.Login),
                        new Claim("Password", user.Credentials.Password),
                        new Claim(ClaimTypes.Role, user.Role.ToString())
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return Unauthorized("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<User> GetUser(int id)
        {
            return await _repository.Users.RetrieveAsync(id);
        }
    }
}
