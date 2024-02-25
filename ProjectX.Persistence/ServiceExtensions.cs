using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProjectX.Application.Contracts;
using ProjectX.Application.Mapper;
using ProjectX.Application.Service;
using ProjectX.Persistence.Data;
using ProjectX.Persistence.Repositories;
using Serilog;
using System.Text;

namespace ProjectX.Persistence
{
    public static class ServiceExtensions
    {
        public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString));
            Serilog.Log.Logger = new LoggerConfiguration()
              .WriteTo.File("Serilogs/AppLogs.log", rollingInterval: RollingInterval.Hour, retainedFileCountLimit: 10)
              .CreateLogger();
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUser, UserRepository>();
            services.AddScoped<IProject, ProjectRepository>();
            services.AddScoped<IPackage, PackageRepository>();
            services.AddScoped<IClient, ClientRepository>();
            services.AddScoped<IProjectUser, ProjectUserRepository>();
            services.AddScoped<IEntity, EntityRepository>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IProjectUserService, ProjectUserService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IDropDownService, DropDownService>();
            services.AddScoped<IVersionManagementService, VersionManagementService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IPackageService, PackageService>();
            services.AddScoped<IEntityService, EntityService>();

            services.AddAutoMapper(typeof(MappingProfile));
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(Options =>
            {
                
                Options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "http://localhost:4200",
                    ValidAudience = "http://localhost:5000",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("HpnrSoitRoHucsleoNAlanGiDnsiac1lr6ie1ka2At8ivavn3iihtbyA"))
                };
  
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                                  policy.RequireRole("Login"));
                options.AddPolicy("Client", policy =>
                                  policy.RequireRole("GenerateToken"));
            });

            services.AddSwaggerGen(opt =>
            {

                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                    },
                    new string[]{}
                    }
    });
            });

        }
        //public static void UsePersistenceCustomConfig(this IApplicationBuilder app)
        //{
        //    //This is optional to monitor the Hangfire Jobs
        //    app.UseHangfireDashboard("/jobs");
        //}
    }
}
