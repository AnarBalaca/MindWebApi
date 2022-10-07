using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Mind.Business.Mapping;
using Mind.Business.Repositories;
using Mind.Business.Services;
using Mind.Business.Token.Implementation;
using Mind.Business.Token.Interface;
using Mind.Data.Abstracts;
using Mind.Data.DAL;
using Mind.Data.İmplementations;
using Mind.Entity.Identity;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{

    options.Password.RequireLowercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;

    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = false;

    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = true;
})
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidAudience = builder.Configuration.GetSection("Jwt:audience").Value,
        ValidIssuer = builder.Configuration.GetSection("Jwt:issuer").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:securityKey").Value)),
    };
});

//mapper
builder.Services.AddMapperService();

//psyc
builder.Services.AddScoped<IPsychologistService, PsychologistRepository>();
builder.Services.AddScoped<IPsychologistDal, PsychologistRepositoryDal>();
//image
builder.Services.AddScoped<IImageService, ImageRepository>();
builder.Services.AddScoped<IImageDal, ImageRepositoryDal>();

//jwt
builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
