using BCrypt.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using universityApiBackend.DTOs;
using universityApiBackend.Helpers;
using universityApiBackend.Models.DataModels;
using universityApiBackend.Repositories;

namespace universityApiBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsControllercs: ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private IUserRepository _userRepository;
        public AccountsControllercs ( JwtSettings jwtSettings, IUserRepository userRepository)
        {
            this._userRepository = userRepository;
            this._jwtSettings = jwtSettings;
        }
        [HttpPost]
        public IActionResult? CurrentUser()
        {
            var jwt = Request.Headers["Authorization"].FirstOrDefault()?.Split(' ').Last();
            if (jwt == null)
            {
                return Unauthorized("jwt is null");
            }
            // Decodificar el token JWT para obtener los valores de las reclamaciones
            var jwtHandler = new JwtSecurityTokenHandler();
            var token = jwtHandler.ReadJwtToken(jwt);

            if (token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email) is Claim emailClaim)
            {
                var userEmail = emailClaim.Value;
                
                var user = _userRepository.FindByEmail(userEmail);

                return Ok(user);

            }

            return null;
        }
        
        [HttpPost]
        public IActionResult GetToken(UserLoginsDTO userLoginsDTO)
        {
            try
            {
                var token = new UserTokens(_userRepository);
                var user = _userRepository.FindByEmail(userLoginsDTO.username);
                
                if (user != null && BCrypt.Net.BCrypt.Verify(userLoginsDTO.password, user.Password))
                {
                    
                    var userToken = new UserTokens(_userRepository)
                    {
                        UserName = user.Name,
                        EmailId= user.Email,
                        id = user.Id,
                        GuidId = Guid.NewGuid(),

                    };
                    token = JwtHelpers.GenTokenKey( userToken ,_jwtSettings );
                }
                else{
                    return BadRequest("User no valid");
                }

                return Ok(token);

            }
            catch (Exception ex)
            {
                throw new Exception("GetTokenError", ex);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Admin")]
        public IActionResult getUserList()
        {
            return Ok(_userRepository.GetAllUsers());
        }
    }
}
