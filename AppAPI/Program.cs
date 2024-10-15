using AppAPI.Mapping;
using AppAPI.Repositories;
using AppData.AppDbContext;
using AppData.DTO.User_RoleDto;
using AppData.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AppAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDBContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            // Add services to the container.

            builder.Services.AddAutoMapper(typeof(ProjectProfile).Assembly);

            builder.Services.AddIdentity<User, Role>(options =>
    options.User.AllowedUserNameCharacters += " ")
        .AddEntityFrameworkStores<AppDBContext>()
        .AddDefaultTokenProviders();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter 'Bearer [jwt]'",
                    Name = "Authorrization",
                    Type = SecuritySchemeType.ApiKey
                });
                var scheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bear"
                    }
                };

                options.AddSecurityRequirement(new OpenApiSecurityRequirement { { scheme, Array.Empty<string>() } });
            });

            builder.Services.AddScoped<IRepository<Field>, Repository<Field>>();
            builder.Services.AddScoped<IRepository<FieldType>, Repository<FieldType>>();
            builder.Services.AddScoped<IRepository<User>, Repository<User>>();

            using var loggerFactory = LoggerFactory.Create(b => b.SetMinimumLevel(LogLevel.Trace).AddConsole());

            var secret = builder.Configuration["JWT:Secret"] ?? throw new InvalidOperationException("Khóa bí mật chưa đc tạo ");

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
                        ValidAudience = builder.Configuration["Jwt:ValidAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                        ClockSkew = new TimeSpan(0, 0, 5)
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = ctx => LogAttempt(ctx.Request.Headers, "OnChallenge"),
                        OnTokenValidated = ctx => LogAttempt(ctx.Request.Headers, "OnTokenValidated")
                    };
                });

            const string policy = "defaultPolicy";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(policy, p =>
                {
                    p.AllowAnyHeader();
                    p.AllowAnyMethod();
                    p.AllowAnyHeader();
                    p.AllowAnyOrigin();
                });
            });

            //Auto Mapper
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile.AutoMapperProfile).Assembly);
            builder.Services.AddScoped<IRepository<FieldShift>, Repository<FieldShift>>();

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(policy);

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();

            Task LogAttempt(IHeaderDictionary headers, string eventType)
            {
                var logger = loggerFactory.CreateLogger<Program>();

                var authorizationHeader = headers["Authorization"].FirstOrDefault();

                if (authorizationHeader is null)
                    logger.LogInformation($"{eventType}. JWT not present");
                else
                {
                    string jwtString = authorizationHeader.Substring("Bearer ".Length);

                    var jwt = new JwtSecurityToken(jwtString);

                    logger.LogInformation($"{eventType}. Expiration: {jwt.ValidTo.ToLongTimeString()}. System time: {DateTime.UtcNow.ToLongTimeString()}");
                }

                return Task.CompletedTask;
            }
        }
    }
}
