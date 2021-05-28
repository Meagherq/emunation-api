using AutoMapper;
using Emunation.Data.Contexts;
using Emunation.Data.Entities;
using Emunation.Data.Helpers;
using Emunation.Services.Concretes;
using Emunation.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;

namespace Emunation.API
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
            IdentityModelEventSource.ShowPII = true;

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), options =>
                {
                    options.MigrationsAssembly("Emunation.Data");
                });
            });

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfiles());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultAuthenticateScheme = AzureADDefaults.AuthenticationScheme;
                sharedOptions.DefaultChallengeScheme = AzureADDefaults.AuthenticationScheme;
            })
            .AddJwtBearer("AzureAD", options =>
            {
                options.Audience = Configuration.GetValue<string>("AzureAd:Audience");
                options.Authority = Configuration.GetValue<string>("AzureAd:Instance") + Configuration.GetValue<string>("AzureAd:TenantId") + "/v2.0";

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = Configuration.GetValue<string>("AzureAd:Issuer"),
                    ValidAudience = Configuration.GetValue<string>("AzureAd:Audience")
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var objectId = context.Principal.Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
                        var dbContext = context.HttpContext.RequestServices
                                .GetRequiredService<DataContext>();
                        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.UserId == Guid.Parse(objectId));
                        if (user == null)
                        {
                            await dbContext.Users.AddAsync(new User { UserId = Guid.Parse(objectId) });
                            await dbContext.SaveChangesAsync();
                        }
                        else
                        {
                            return;
                        }
                    }
                };
            });

            services.AddMvc();

            services.AddCors(options =>
            {
                options.AddPolicy("FrontEnd",
                    builder => builder
                    .WithOrigins("https://app.emunation.org")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Emunation API", Version = "v1" });
            });

            services.AddScoped<IGameService, GameService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("FrontEnd");

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Emunation API V1");
            });

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
