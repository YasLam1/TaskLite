using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskLite.Application.Interfaces.Repositories;
using TaskLite.Application.UseCases.Comments;
using TaskLite.Application.UseCases.Projects;
using TaskLite.Application.UseCases.Tasks;
using TaskLite.Application.UseCases.Users;
using TaskLite.Domain.Entities;
using TaskLite.Infrastructure.Persistence;
using TaskLite.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

// Identity
builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = true;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// JWT Authentication
var jwtConfig = builder.Configuration.GetSection("Jwt");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtConfig["Issuer"],
            ValidAudience = jwtConfig["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtConfig["Key"]!))
        };
    });
builder.Services.AddAuthorization();

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

// Projects Use cases
builder.Services.AddScoped<CreateProjectHandler>();
builder.Services.AddScoped<ListProjectsByOwnerHandler>();
builder.Services.AddScoped<DeleteProjectHandler>();
builder.Services.AddScoped<UpdateProjectHandler>();

// Users Use cases
builder.Services.AddScoped<DeleteUserHandler>();

// Tasks Use cases
builder.Services.AddScoped<CreateTaskHandler>();
builder.Services.AddScoped<ListTasksByProjectHandler>();
builder.Services.AddScoped<DeleteTaskHandler>();
builder.Services.AddScoped<UpdateTaskHandler>();

// Comments Use cases
builder.Services.AddScoped<CreateCommentHandler>();
builder.Services.AddScoped<ListCommentsByTaskHandler>();
builder.Services.AddScoped<DeleteCommentHandler>();
builder.Services.AddScoped<UpdateCommentHandler>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
