using System.Data;
using System.Data.Common;
using System.Text;
using Dapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RadioScheduler.Interfaces;
using RadioScheduler.Interfaces.Auth;
using RadioScheduler.Models.Api;
using RadioScheduler.Repositories;
using RadioScheduler.Repositories.Auth;
using RadioScheduler.Services;
using RadioScheduler.Services.Auth;
using RadioScheduler.Utils;
using RadioScheduler.Utils.DatabaseHandlers;

namespace RadioScheduler.Extensions;

internal static class WebApplicationBuilderExtensions {
	public static void ConfigureConfiguration(this WebApplicationBuilder builder) {
		builder.Configuration.AddUserSecrets(typeof(Program).Assembly);
	}

	public static void ConfigureLogging(this WebApplicationBuilder builder) {
		builder.Logging.ClearProviders();
		builder.Logging.AddConsole();
	}

	public static void ConfigureServices(this WebApplicationBuilder builder) {
		builder.Services.AddControllers();

		// Repositories
		builder.Services.AddScoped<IAuthRepository, AuthRepository>();
		builder.Services.AddScoped<IUserRepository, UserRepository>();

		builder.Services.AddScoped<IRadioShowRepository, RadioShowRepository>();
		builder.Services.AddScoped<IRadioHostRepository, RadioHostRepository>();
		builder.Services.AddScoped<IStudioRepository, StudioRepository>();
		builder.Services.AddScoped<ITimeslotRepository, TimeslotRepository>();
		builder.Services.AddScoped<ITableauRepository, TableauRepository>();

		// Services
		builder.Services.AddScoped<AuthService>();
		builder.Services.AddScoped<UserService>();

		builder.Services.AddScoped<RadioShowService>();
		builder.Services.AddScoped<RadioHostService>();
		builder.Services.AddScoped<StudioService>();
		builder.Services.AddScoped<TimeslotService>();
		builder.Services.AddScoped<TableauService>();

		builder.Services.AddScoped<ApiResponse>();

		builder.Services.AddSingleton<TokenService>();
	}

	public static void ConfigureDatabase(this WebApplicationBuilder builder) {
		builder.Services.AddDbContext<AppDbContext>(options => {
			options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
		});

		builder.Services.AddScoped<IDbConnection>(provider => {
			AppDbContext context = provider.GetRequiredService<AppDbContext>();
			DbConnection connection = context.Database.GetDbConnection();
			if (connection.State != ConnectionState.Open) {
				connection.Open();
			}

			return connection;
		});
	}

	public static void ConfigureCors(this WebApplicationBuilder builder) {
		builder.Services.AddCors(options => {
			options.AddPolicy("AllowAll",
				policy => policy
					.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader());
		});
	}

	public static void ConfigureSwagger(this WebApplicationBuilder builder) {
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(o => {
			OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme {
				Name = "JWT Authentication",
				Description = "Enter your JWT token in this field",
				In = ParameterLocation.Header,
				Type = SecuritySchemeType.Http,
				Scheme = "Bearer",
				BearerFormat = "JWT",
			};

			o.AddSecurityDefinition("Bearer", securityScheme);

			OpenApiSecurityRequirement securityRequirement = new OpenApiSecurityRequirement {
				{
					new OpenApiSecurityScheme {
						Reference = new OpenApiReference {
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
						}
					},
					[]
				}
			};

			o.AddSecurityRequirement(securityRequirement);
		});
	}

	public static void ConfigureDapper() {
		SqlMapper.AddTypeHandler(new GuidHandler());
		SqlMapper.AddTypeHandler(new UnixMsDateOnlyHandler());
		DefaultTypeMap.MatchNamesWithUnderscores = true;
	}

	public static void ConfigureAuth(this WebApplicationBuilder builder) {
		builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(o => {
				o.RequireHttpsMetadata = false;
				o.TokenValidationParameters = new TokenValidationParameters {
					ValidIssuer = builder.Configuration["Jwt:Issuer"],
					ValidAudience = builder.Configuration["Jwt:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(
						Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!)),
					ClockSkew = TimeSpan.Zero
				};
			});

		builder.Services.AddAuthorization();
	}
}
