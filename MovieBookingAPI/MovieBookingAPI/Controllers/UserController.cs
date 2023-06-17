using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieBooking.BL.Interfaces;
using MovieBooking.BL.Services;
using MovieBooking.Models.Models;
using MovieBooking.Models.Requests;
using MovieBooking.Models.Requests.MovieRequests;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieBookingAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly IConfiguration _configuration;

		public UserController(IUserService userService, IConfiguration configuration)
		{
			_userService = userService;
			_configuration = configuration;
		}

		[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(User))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[AllowAnonymous]
		[HttpPost("Add")]
		public async Task<IActionResult> Add([FromBody] AddUserRequest user)
		{
			if (user == null) return BadRequest(user);

			await _userService.Add(user);

			return Ok(user);
		}

		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[HttpGet("Login")]
		[AllowAnonymous]
		public async Task<IActionResult> Login(string email, string password)
		{
			if (string.IsNullOrEmpty(email) ||
				string.IsNullOrEmpty(password))
				return BadRequest("Wrong e-mail and/or password");

			var user = await _userService.GetUserInfo(email, password);

			if (user != null)
			{
				var claims = new List<Claim>
				{
					new Claim(JwtRegisteredClaimNames.Sub, _configuration.GetSection("Jwt:Subject").Value),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
					new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
					new Claim("UserId", user.Id.ToString()),
					new Claim("Email", user.Email ?? string.Empty)
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
				return BadRequest("Invalid credentials");
			}
		}
	}
}
