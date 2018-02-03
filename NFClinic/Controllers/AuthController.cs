using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using NFClinic.AppSettings;
using AutoMapper;
using NFClinic.Data.Models.AppUser;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using NFClinic.ErrorValidation;
using NFClinic.Core.DTOs;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace NFClinic.Controllers
{
    [Produces("application/json")]
    [Route("api/Auth")]
    public class AuthController : Controller
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;
		private readonly JwtSettings JWTSettings;
		private readonly IMapper mapper;

		public AuthController(
			UserManager<ApplicationUser> userManager,
			IMapper mapper,
			SignInManager<ApplicationUser> signInManager,
			IOptionsSnapshot<JwtSettings> JWTSettings)
		{
			this.userManager = userManager;
			this.mapper = mapper;
			this.signInManager = signInManager;
			this.JWTSettings = JWTSettings.Value;
		}

		[AllowAnonymous]
		[ValidateModelAttributes]
		[HttpPost("Register")]
		public async Task<IActionResult> RegisterUser([FromBody] RegisterDTO registerDTO)
		{
			//Create clinic user to save
			var user = mapper.Map<RegisterDTO, ApplicationUser>(registerDTO);

			//Set username to email
			user.UserName = registerDTO.Email;

			//Save new user to database
			var result = await userManager.CreateAsync(user, registerDTO.Password);
			if (result.Succeeded)
			{
				await userManager.AddToRolesAsync(user, new[] { "staff" });
				return Ok();
			}

			//Return errors, if it fails
			return BadRequest(result.Errors);
		}

		[AllowAnonymous]
		[ValidateModelAttributes]
		[HttpPost("Login")]
		public async Task<IActionResult> CreateToken([FromBody] LoginDTO loginDTO)
		{
			try
			{
				//Try to login user
				var result = await signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, false, false);
				if (result.Succeeded)
				{
					var user = await userManager.FindByEmailAsync(loginDTO.Email);
					var userRoles = await userManager.GetRolesAsync(user);

					//Create user claims
					var claims = new List<Claim>
					{
						new Claim(ClaimTypes.Sid, user.Id),
						new Claim(ClaimTypes.Name, loginDTO.Email)
					};

					//Add user roles
					foreach (var role in userRoles)
					{
						claims.Add(new Claim(ClaimTypes.Role, role));
					}

					//Create JWT Token
					var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSettings.Key));
					var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

					var jwtSecurityToken = new JwtSecurityToken(
						issuer: JWTSettings.Issuer,
						audience: JWTSettings.Audience,
						claims: claims,
						expires: DateTime.UtcNow.AddMinutes(30),
						signingCredentials: signingCredentials
						);

					//Return token
					return Ok(new
					{
						token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
						expiration = jwtSecurityToken.ValidTo
					});
				}
				else
				{
					return BadRequest();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}