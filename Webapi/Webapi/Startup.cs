using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using Webapi.Models;

namespace Webapi
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
            
            services.AddControllers().AddJsonOptions(options =>
             {
                 options.JsonSerializerOptions.Converters.Add(new Models.DateTimeConverter());
             });

            services.AddDbContext<ApiContext>(opt =>
            opt.UseNpgsql(Configuration.GetConnectionString("MyWebApiConection")));
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
            
            services.AddTransient((config) =>
            {
                var conf = new JwtToken();
                Configuration.GetSection("JWTConfiguration").Bind(conf);
                return conf;
            });
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddOptions();
            ConfigureJwt(services);
        }
        // Jwt configure
        public void ConfigureJwt(IServiceCollection services)
        {
            var config = services.BuildServiceProvider().GetService<JwtToken>();
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config.SecretKey));
            var tokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = signingKey,
                ValidIssuer = config.ValidIssuer,
                ValidAudience = config.ValidAudience,
                ValidateIssuer = false,
                ValidateAudience = false
            };
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(c =>
            {
                c.RequireHttpsMetadata = false;
                c.SaveToken = true;
                c.TokenValidationParameters = tokenValidationParameters;
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.S
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApiContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAll"); 
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
            app.UseAuthorization();   
        }
    }
}
