using Azure.Core;
using System.IO;

namespace Api;

public class Startup(IConfigurationRoot configuration)
{
    public IConfigurationRoot Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
            {
                policy.WithOrigins("https://localhost:3000")
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
        });

        string jwtSecret = Configuration["Jwt:Secret"];
        var key = Encoding.ASCII.GetBytes(jwtSecret!);

        services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        });

        services.AddSignalR(); 
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddSingleton<IConfiguration>(Configuration);
        services.AddLogging();

        services.AddRepository();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly);
        });

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "MisteryBox API",
                Version = "v1",
                Description = "A API to manage MisteryBox web site"
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Digite: Bearer {seu_token}"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        services.AddDbContext<Context>(options =>
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            options.EnableSensitiveDataLogging();
        });

        services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true
            };

            // Lê o token do cookie HttpOnly
            options.Events = new JwtBearerEvents
            {

                OnMessageReceived = context =>
                {
                    var path = context.HttpContext.Request.Path;

                    var token = context.Request.Cookies["access_token"];

                    if (!string.IsNullOrEmpty(token))
                    {
                        context.Token = token;
                    }

                    return Task.CompletedTask;
                }
            };
        });

        services.AddAuthorization();

    }

    public void Configure(IApplicationBuilder app)
    {

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSwaggerUI();
        app.UseSwagger();

        app.UseCors("AllowFrontend");

        app.UseHttpsRedirection();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<NotificationHub>("/hub/notificacoes");
        });
    }
}