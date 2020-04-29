using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace emunation_api
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
            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultAuthenticateScheme = AzureADDefaults.AuthenticationScheme;
                sharedOptions.DefaultChallengeScheme = AzureADDefaults.AuthenticationScheme;
            })
            .AddJwtBearer("AzureAD", options =>
            {
                options.Audience = Configuration.GetValue<string>("AzureAd:Audience");
                options.Authority = Configuration.GetValue<string>("AzureAd:Instance") + Configuration.GetValue<string>("AzureAd:TenantId");

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = Configuration.GetValue<string>("AzureAd:Issuer"),
                    ValidAudience = Configuration.GetValue<string>("AzureAd:Audience")
                };
            });

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = AzureADDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = AzureADDefaults.AuthenticationScheme;
            //}).AddAzureAD(options => Configuration.Bind("AzureAd"), options);


            services.AddMvc();
            
            services.AddCors(options => 
            { 
                options.AddPolicy("FrontEnd", 
                    builder => builder
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()); 
            });

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Emunation API", Version = "v1" });
            });
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
