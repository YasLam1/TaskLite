using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TaskLite.Application.Interfaces.Repositories;
using TaskLite.Application.UseCases.Comments;
using TaskLite.Application.UseCases.Projects;
using TaskLite.Application.UseCases.Tasks;
using TaskLite.Application.UseCases.Users;
using TaskLite.Infrastructure.Persistence;
using TaskLite.Infrastructure.Persistence.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
        };
    });
builder.Services.AddAuthorization();

string? connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

//Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

//Projects Use cases
builder.Services.AddScoped<CreateProjectHandler>();
builder.Services.AddScoped<ListProjectsByOwnerHandler>();
builder.Services.AddScoped<DeleteProjectHandler>();
builder.Services.AddScoped<UpdateProjectHandler>();

//Users Use cases
builder.Services.AddScoped<CreateUserHandler>();
builder.Services.AddScoped<GetUserByIdHandler>();
builder.Services.AddScoped<DeleteUserHandler>();
builder.Services.AddScoped<UpdateUserHandler>();

//Tasks Use cases
builder.Services.AddScoped<CreateTaskHandler>();
builder.Services.AddScoped<ListTasksByProjectHandler>();
builder.Services.AddScoped<DeleteTaskHandler>();
builder.Services.AddScoped<UpdateTaskHandler>();

//Comments Use cases
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

app.UseHttpsRedirection();
app.MapControllers();
app.Run();