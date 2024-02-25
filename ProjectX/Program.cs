using FluentValidation.AspNetCore;
using FluentValidation;
using ProjectX.Persistence;
using ProjectX.Application.Usecases.User;
using static ProjectX.Application.Validators.UserValidator;
using ProjectX.Application.Usecases.Login;
using static ProjectX.Application.Validators.AuthenticateValidator;
using ProjectX.Application.Usecases.Clients;
using static ProjectX.Application.Validators.ClientValidator;
using ProjectX.Application.Usecases.ProjectUsers;
using static ProjectX.Application.Validators.ProjectUserValidator;
using ProjectX.Application.Usecases.Projects;
using static ProjectX.Application.Validators.ProjectValidator;
using ProjectX.Application.Usecases.Package;
using static ProjectX.Application.Validators.PackageValidator;
using ProjectX.Infrastructure.Utility;
using ProjectX.Application.Usecases.Entity;
using static ProjectX.Application.Validators.EntityValidator;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.ConfigurePersistence(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddTransient<IValidator<UserAddRequest>, AddUserValidator>();
builder.Services.AddTransient<IValidator<UserUpdateRequest>, UpdateUserValidator>();
builder.Services.AddTransient<IValidator<UserLogin>, LoginValidator>();
builder.Services.AddTransient<IValidator<ClientAddRequest>, AddClientValidator>();
builder.Services.AddTransient<IValidator<ClientUpdateRequest>, UpdateClientValidator>();
builder.Services.AddTransient<IValidator<ProjectUserAddRequest>, AddProjectUserValidator>();
builder.Services.AddTransient<IValidator<ProjectUserUpdateRequest>, UpdateProjectUserValidator>();
builder.Services.AddTransient<IValidator<ProjectAddRequest>, ProjectAddValidator>();
builder.Services.AddTransient<IValidator<ProjectUpdateRequest>, ProjectUpdateValidator>();
builder.Services.AddTransient<IValidator<PackageAddRequest>, PackageAddValidator>();
builder.Services.AddTransient<IValidator<PackageUpdateRequest>, PackageUpdateValidator>();
builder.Services.AddTransient<IValidator<EntityAddRequest>, EntityAddValidator>();
builder.Services.AddTransient<IValidator<EntityUpdateRequest>, EntityUpdateValidator>();
builder.Services.AddTransient<ICryptography, Cryptography>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
