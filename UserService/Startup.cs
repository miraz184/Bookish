using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using UserService.Models;
using UserService.Repository;
using UserService.Service;

namespace UserService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            ValidateToken(Configuration, services);

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, Service.UserService>();
            services.AddScoped<UserContext>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "User API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                            new string[] {}

                    }
                });

            });
            services.AddCors();
            //      services.AddCors(c => c.AddPolicy("CorsPolicy", builder =>
            //    {
            //      builder.AllowAnyOrigin()
            //      .AllowAnyMethod()
            //      .AllowAnyHeader();
            //   }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserAPI");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors(builder =>
            builder
                  .AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  );
            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private void ValidateToken(IConfiguration configuration, IServiceCollection services)
        {
            var secutiryTokenParameters = configuration.GetSection("SecutiryTokenParameters");
            var securityKey = secutiryTokenParameters["sercuritykey"];
            var keyBytes = Encoding.ASCII.GetBytes(securityKey);
            var signingKey = new SymmetricSecurityKey(keyBytes);

            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = secutiryTokenParameters["Iss"],

                ValidateAudience = true,
                ValidAudience = secutiryTokenParameters["Aud"],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(o =>
            {

                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(o => o.TokenValidationParameters = tokenValidationParameters);
        }
    }
}
