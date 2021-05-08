using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using THA.Entity.Main;
using THA.Model.AppSettings;
using THA.Model.User;
using THA.Service.Product;
using THA.Service.Users;

namespace THA_Api
{
   public class Startup
   {
      public Startup(IConfiguration configuration)
      {
         Configuration = configuration;
         _appSetting = Configuration.GetSection("AppSetting").Get<AppSetting>();

      }

      public IConfiguration Configuration { get; }
      private AppSetting _appSetting { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {
         services.Configure<AppSetting>(Configuration.GetSection("AppSetting"));

         // Configure DBContext
         services.AddDbContext<MainContext>(options => { options.UseInMemoryDatabase("tha_db"); });


         // Configure authentication
         services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey = true,
                  ValidIssuer = _appSetting.Jwt.Issuer,
                  ValidAudience = _appSetting.Jwt.Issuer,
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSetting.Jwt.Key))
               };
            });

         // Configure authorization
         services.AddAuthorization(options =>
         {
            options.AddPolicy("GOD", policy => policy.RequireClaim(ClaimTypes.Role, Roles.GOD.ToString()));
            options.AddPolicy("PRODUCT_ADMIN", policy => policy.RequireClaim(ClaimTypes.Role, Roles.PRODUCT_ADMIN.ToString()));
            options.AddPolicy("USER", policy => policy.RequireClaim(ClaimTypes.Role, Roles.USER.ToString()));
         });

         // Configure Controllers
         services.AddControllers().AddJsonOptions(options =>
         {
            options.JsonSerializerOptions.PropertyNamingPolicy = null;

         });

         services.AddSwaggerGen(c =>
         {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "THA_Api", Version = "v1" });
         });

         services.AddHttpContextAccessor();


         // Configure DI Service
         services.AddScoped<ProductRepository>();
         services.AddScoped<UserRepository>();

      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "THA_Api v1"));
         }

         app.UseHttpsRedirection();

         app.UseRouting();

         app.UseAuthentication();
         app.UseAuthorization();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });
      }
   }
}
