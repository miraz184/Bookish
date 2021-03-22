using AuthenticationService.Models;
using AuthenticationService.Repository;
using AuthenticationService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace AuthenticationService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public static IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvc();

            string sqlConnectivity = Environment.GetEnvironmentVariable("SQL_USTCONNECTION");
            if (sqlConnectivity == null)
            {
                sqlConnectivity = Configuration.GetConnectionString("AuthDb");
            }
            //Register DbContext with connection string read from configuration
            services.AddDbContext<AuthDbContext>(u => u.UseSqlServer(sqlConnectivity));


            //string sqlcon = Configuration.GetConnectionString("AuthCon");
            //services.AddDbContext<AuthDbContext>(u => u.UseSqlServer(sqlcon));

            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<AuthDbContext>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuthApi", Version = "v1" });
            });
            services.AddCors(c => c.AddPolicy("CorsPolicy", builder =>
               {
                   builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
               }));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth Api V1");
            });


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        internal static bool IsTokenValid(string token)
        {
            return ValidateToken.Validate(Configuration, token);
        }

    }
}
