using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using VetExpert.API.Helpers;
using VetExpert.Domain;
using VetExpert.Domain.Dto;
using VetExpert.Infrastructure;

namespace VetExpert.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		private readonly IRepository<ApplicationUser> _appUserRepository;
		private readonly IMapper _mapper;
		private readonly IConfiguration _configuration;

		public AuthenticationController(IRepository<ApplicationUser> appUserRepository,
			IMapper mapper, 
			IConfiguration configuration)
		{
			_appUserRepository = appUserRepository;
			_mapper = mapper;
			_configuration = configuration;
		}

		[HttpPost("login")]
		public async Task<ActionResult<string>> Login(UserLoginDto request)
		{
			var user = await _appUserRepository.FindEntity(x => x.UserName == request.Username);
			
			if (user == null)
			{
				return BadRequest("User not found.");
			}

			if (!AuthenticationExtension.VerifyPasswordHash(request.Password,
				    user.PasswordHash,
				    user.PasswordSalt))
			{
				return BadRequest("Wrong password");
			}

			var tokenKey = _configuration.GetSection("AppSettings:Token").Value;

			if (tokenKey == null)
			{
				return BadRequest("No token key was provided.");
			}

			string token = AuthenticationExtension.CreateToken(user, user.Role, tokenKey);

			return Ok(token);
		}

		[HttpPost("client/register")]
		public async Task<ActionResult<ApplicationUser>> RegisterClient(UserLoginDto request)
		{
			var existingUser = await _appUserRepository.FindEntity(x => x.UserName == request.Username);

			if (existingUser != null)
			{
				return BadRequest("Username is already being used.");
			}

			var user = new ApplicationUser();

			AuthenticationExtension.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

			user.UserName = request.Username;
			user.Role = UserRole.User;
			user.PasswordHash = passwordHash;
			user.PasswordSalt = passwordSalt;

			await _appUserRepository.Add(user);
			await _appUserRepository.SaveChangesAsync();

			return Ok(user);
		}

        [HttpPost("clinic/register")]
        public async Task<ActionResult<ApplicationUser>> RegisterClinic(UserLoginDto request)
        {
            var existingUser = await _appUserRepository.FindEntity(x => x.UserName == request.Username);

            if (existingUser != null)
            {
                return BadRequest("Username is already being used.");
            }

            var user = new ApplicationUser();

            AuthenticationExtension.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.UserName = request.Username;
            user.Role = UserRole.Clinic;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _appUserRepository.Add(user);
            await _appUserRepository.SaveChangesAsync();

            return Ok(user);
        }

    }
}
