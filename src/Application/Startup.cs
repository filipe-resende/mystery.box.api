using Application.AutoMapper;
using Application.Configurations;
using Application.DBContext;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Application
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        public Startup(IConfigurationRoot configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddAutoMapper(typeof(DataMappingProfile));
            services.AddRepository();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "PostsApi", Version = "v1" });
                options.EnableAnnotations();
            });

            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });

            services.AddDbContext<Context>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
          
            var key = Encoding.ASCII.GetBytes(Settings.Secret);

            services.
               AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(x =>
               {
                   x.RequireHttpsMetadata = false;
                   x.SaveToken = true;
                   x.TokenValidationParameters = new TokenValidationParameters()
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(key),
                       ValidateIssuer = false,
                       ValidateAudience = false
                   };
               });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseSwaggerUI();
            app.UseSwagger();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
