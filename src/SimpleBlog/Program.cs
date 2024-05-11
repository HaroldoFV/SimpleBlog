using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SimpleBlog.AuthAPI.Data;
using SimpleBlog.AuthAPI.Data.Repository;
using SimpleBlog.AuthAPI.Domain.Entity;
using SimpleBlog.AuthAPI.Domain.Repository;
using SimpleBlog.AuthAPI.Services.Post;
using SimpleBlog.AuthAPI.Services.User;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");

builder.Services.AddDbContext<BlogDbContext>(
    options => options.UseNpgsql(
            connectionString ?? throw new Exception("Postgres configuration section not found")
        )
        .LogTo(Console.WriteLine, LogLevel.Information)
);

// Configuração do Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        options.SignIn.RequireConfirmedAccount = true
    )
    .AddEntityFrameworkStores<BlogDbContext>();

builder.Services.AddHealthChecks()
    .AddNpgSql(
        connectionString ?? throw new Exception("Postgres configuration section not found")
    );

// Configuração de JWT
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["JwtKey"] ??
                                           throw new InvalidOperationException(
                                               "JWT Key not found in configuration")
                    )
                ),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddTransient<IPostService, PostService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IPostRepository, PostRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();