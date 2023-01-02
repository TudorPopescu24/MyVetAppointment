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

			//string token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiVG9ueSBTdGFyayIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Iklyb24gTWFuIiwiZXhwIjozMTY4NTQwMDAwfQ.IbVQa1lNYYOzwso69xYfsMOHnQfO3VLvVqV2SOXS7sTtyyZ8DEf5jmmwz2FGLJJvZnQKZuieHnmHkg7CGkDbvA";

			var tokenKey = _configuration.GetSection("AppSettings:Token").Value;

			if (tokenKey == null)
			{
				return BadRequest("No token key was provided.");
			}

			string token = AuthenticationExtension.CreateToken(user, tokenKey);

			return Ok(token);
		}

		[HttpPost("register")]
		public async Task<ActionResult<ApplicationUser>> Register(UserLoginDto request)
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
		
	}
}
