namespace Api;

public class Startup(IConfigurationRoot configuration)
{
    public IConfigurationRoot Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddSingleton<IConfiguration>(Configuration);

        services.AddRepository();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly);
        });

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "PostsApi", Version = "v1" });
            options.EnableAnnotations();
        });

        services.AddConflitExceptionFilter();

        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });

        services.AddDbContext<Context>(options =>
        {

            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            options.EnableSensitiveDataLogging();
        });


        string jwtSecret = Configuration["JwtSecret"];
        var key = Encoding.ASCII.GetBytes(jwtSecret!);

        services.
           AddAuthentication(x =>
           {
               x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
           })
           .AddJwtBearer(x =>
           {
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters()
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(key),
                   ValidateIssuer = false,
                   ValidateLifetime = false,
                   ValidateAudience = false
               };
           });
    }

    public void Configure(IApplicationBuilder app)
    {

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSwaggerUI();
        app.UseSwagger();

        app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
        app.UseHttpsRedirection();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}