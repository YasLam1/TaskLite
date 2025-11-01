using Microsoft.EntityFrameworkCore;
using TaskLite.Application.Interfaces.Repositories;
using TaskLite.Application.UseCases.Projects;
using TaskLite.Application.UseCases.Users;
using TaskLite.Infrastructure.Persistence;
using TaskLite.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

string? connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<CreateProjectHandler>();
builder.Services.AddScoped<ListProjectsByOwnerHandler>();
builder.Services.AddScoped<CreateUserHandler>();
builder.Services.AddScoped<GetUserByIdHandler>();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();