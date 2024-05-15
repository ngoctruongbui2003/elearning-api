using ElearningAPI.Datas;
using ElearningAPI.Repositories;
using ElearningAPI.Repositories.Impl;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Text;

namespace ElearningAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{

            

            var builder = WebApplication.CreateBuilder(args);
			var config = builder.Configuration;
			builder.Services.AddAuthorization();

            // Add services to the container.
           

            builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();

			// Swagger UI
			builder.Services.AddSwaggerGen(setup =>
			{
				// Include 'SecurityScheme' to use JWT Authentication
				var jwtSecurityScheme = new OpenApiSecurityScheme
				{
					BearerFormat = "JWT",
					Name = "JWT Authentication",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.Http,
					Scheme = JwtBearerDefaults.AuthenticationScheme,
					Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

					Reference = new OpenApiReference
					{
						Id = JwtBearerDefaults.AuthenticationScheme,
						Type = ReferenceType.SecurityScheme
					}
				};

				setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

				setup.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{ jwtSecurityScheme, Array.Empty<string>() }
	});

			});

			// Connect dbs
			builder.Services.AddDbContext<AppDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ??
					throw new InvalidOperationException("Connection String is not found"));
			});

			// Identity
			builder.Services.AddIdentity<User, IdentityRole>()
				.AddEntityFrameworkStores<AppDbContext>()
				.AddSignInManager()
				.AddRoles<IdentityRole>();

			// JWT
			builder.Services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(x =>
			{
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidIssuer = config["Jwt:Issuer"],
					ValidAudience = config["Jwt:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(
						Encoding.UTF8.GetBytes(config["Jwt:Key"]!
					)),
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
				};
			});

			builder.Services.AddScoped<IUserRepository, UserRepository>();

			// Add Http Context Accessor
			builder.Services.AddHttpContextAccessor();


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseCors(builder =>
			{
				builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
			});
            app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}