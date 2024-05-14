using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using universityApiBackend.Models.DataModels;
using universityApiBackend.Repositories;

namespace universityApiBackend.Helpers
{
    public static class JwtHelpers
    {
        public static IUserRepository? _userRepository;

        public static IEnumerable<Claim> GetClaims(this UserTokens userAccount,Guid id)
        {
            userAccount.SetRol(userAccount.EmailId);
            List<Claim> claims = new List<Claim>()
            {
                new Claim("id",userAccount.id.ToString()),
                new Claim(ClaimTypes.Name,userAccount.UserName),
                new Claim(ClaimTypes.Email, userAccount.EmailId),
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddSeconds(120).ToString("MMM ddd dd yyyy HH:mm:ss tt")),
            };

            if (userAccount.rol == Rol.Admin ) 
            {
                claims.Add(new Claim(ClaimTypes.Role,"Admin"));
            }else if (userAccount.rol == Rol.User )
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
                claims.Add(new Claim("UserOnly", "User 1"));

            }
            return claims;
        }
        public static IEnumerable<Claim> GetClaims(this UserTokens userAccount, out Guid id)
        {
            id = Guid.NewGuid();
            return GetClaims(userAccount,id);
        }

        public static UserTokens GenTokenKey(UserTokens model, JwtSettings jwtSettings)
        {
            try 
            {
                var userToken = new UserTokens(_userRepository);
                if (model == null)
                {
                    throw new ArgumentNullException(nameof(model));

                }

                //Obtain Secret Key

                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuserSigningKey);

                Guid id;
                //Expire in 520 seconds
                DateTime expireTime = DateTime.UtcNow.AddSeconds(520);

                //Validity of our token
                userToken.Validity = expireTime.TimeOfDay;

                // Generate our JWT

                var jwtToken = new JwtSecurityToken(
                    issuer: jwtSettings.ValidIssuer,
                    audience: jwtSettings.ValidAudience,
                    claims: GetClaims(model,out id),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(expireTime).DateTime,
                    signingCredentials: new SigningCredentials(
                            new SymmetricSecurityKey(key),
                            SecurityAlgorithms.HmacSha256
                        )

                    );
                userToken.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                userToken.UserName = model.UserName;
                userToken.id = model.id;
                userToken.GuidId = id;


                return userToken;


            }catch(Exception e){
                throw new Exception("Error generating Jwt");
            };
        }

    }
}
