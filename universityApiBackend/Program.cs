// 1. usings to work qith Entity FrameWork
//Migration-Codes : add-migration "Create (migration name) table"; crea la migracion que se va a hacer con el dbSet
//Update-Database (optional) -Verbose; actualiza la base de datos con la nueva migration que se hace

//script-migration; se puede ver el script q    ue se va a lanzar con el migration-update
using Microsoft.EntityFrameworkCore;

using universityApiBackend.DataAccess;
using universityApiBackend.Repositories;
using universityApiBackend.Repositories.Implements;
using universityApiBackend.Services;
using universityApiBackend.Services.Implements;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
var builder = WebApplication.CreateBuilder(args);

// 2. Connection with SQL server Exrpess
const String CONNECTIONNAME = "UniversityDB";

var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);

// 3. Add Context to services of builder

builder.Services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(connectionString));

//builder.Services.AddGraphQLServer()
    //.AddQueryType<UserQuery>();
// 7. Add services of JWT Authorization

builder.Services.AddJwtTokenServices(builder.Configuration);

//TODO: config Swager To make Authorization of jwt

// Add services to the container.

builder.Services.AddControllers();

// 4. Add custom Services (folder Services)

builder.Services.AddScoped<IStudentsService, StudentsServicecs>();

builder.Services.AddScoped<IUserRepository, UserRepositoryIMPL>();

// 8. Add Authorization 

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserOnlyPolicy", policy => policy.RequireClaim("UserOnly", "User1"));
});

//TODO: Add the rest of services

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//TODO: Config Swagger to take of Authorization of JWT

builder.Services.AddSwaggerGen(options =>
    {

        // Define the security scheme (Bearer)
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme"
        });

        // Define the security requirement
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

    }
);

// 5. CORS Configuration

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy",builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//email: admin@admin.com
//password: admin
//name: nicoxg


//app.MapGraphQL(path: "/graphql");


// HTTPS redirection
app.UseHttpsRedirection();



// Routing middleware
app.UseRouting();

// Authorization middleware
app.UseAuthorization();
// CORS middleware
app.UseCors("CorsPolicy");

// Endpoint mapping
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();