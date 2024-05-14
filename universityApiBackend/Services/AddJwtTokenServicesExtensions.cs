using Microsoft.AspNetCore.Authentication.JwtBearer;
using universityApiBackend.Models.DataModels;
using Microsoft.IdentityModel.Tokens;
namespace universityApiBackend.Services
{
    public static class AddJwtTokenServicesExtensions
    {
        public static void AddJwtTokenServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add settings Jwt
            var bindJwtSettings = new JwtSettings();

            configuration.Bind("JsonWebTokenKeys",bindJwtSettings);

            //add singletons of JWT Settings
            services.AddSingleton(bindJwtSettings);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters() 
                { 
                    ValidateIssuerSigningKey = bindJwtSettings.ValidateIssUserSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(bindJwtSettings.IssuserSigningKey)),
                    ValidIssuer = bindJwtSettings.ValidIssuer,
                    ValidateAudience = bindJwtSettings.ValidateAudience,
                    ValidAudience = bindJwtSettings.ValidAudience,
                    RequireExpirationTime = bindJwtSettings.RequiereExpirationTime,
                    ValidateLifetime = bindJwtSettings.ValidateLifeTime,
                    ClockSkew = TimeSpan.FromDays(1),
                };
            });
        }
    }
}
