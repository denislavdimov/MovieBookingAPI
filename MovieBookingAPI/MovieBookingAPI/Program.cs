using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MovieBooking.BL.Interfaces;
using MovieBooking.BL.Services;
using MovieBooking.DL.Interfaces;
using MovieBooking.DL.Repositories.MongoDB;
using MovieBooking.Models.Configuration;
using System.Text;
using FluentValidation.AspNetCore;
using FluentValidation;
using MovieBookingAPI.Extensions;
using MovieBookingAPI.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbConfiguration>(
	builder.Configuration.GetSection(nameof(MongoDbConfiguration)));

// Add services to the container.
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IUserRepository, UserMongoRepository>();
builder.Services.AddSingleton<IMovieService, MovieService>();
builder.Services.AddSingleton<IMovieRepository, MovieMongoRepository>();
builder.Services.AddSingleton<IBookingService, BookingService>();
builder.Services.AddSingleton<IBookingRepository, BookingMongoRepository>();

builder.Services
	.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.RequireHttpsMetadata = false;
		options.SaveToken = true;
		options.TokenValidationParameters = new TokenValidationParameters()
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidAudience = builder.Configuration["Jwt:Audience"],
			ValidIssuer = builder.Configuration["Jwt:Issuer"],

			IssuerSigningKey = new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
		};
	});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
	var jwtSecurityScheme = new OpenApiSecurityScheme()
	{
		Scheme = "bearer",
		BearerFormat = "JWT",
		Name = "JWT Authentication",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.Http,
		Description = "Put **_ONLY_** JWT Bearer token in the text box below!",
		Reference = new OpenApiReference()
		{
			Id = JwtBearerDefaults.AuthenticationScheme,
			Type = ReferenceType.SecurityScheme
		},

	};

	x.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
	x.AddSecurityRequirement(new OpenApiSecurityRequirement()
	{
		{ jwtSecurityScheme, Array.Empty<string>() }
	});
});

builder.Services.AddFluentValidationAutoValidation()
	.AddFluentValidationClientsideAdapters();
builder.Services
	.AddValidatorsFromAssemblyContaining(typeof(Program));

builder.Services
	.AddHealthChecks()
	.AddCheck<MongoHealthCheck>("MongoDB");


BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.RegisterHealthCheck();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
