using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Inmobiliaria_.Net_Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace InmobiliariaApi
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
			services.AddMvc(options =>
			{
				options.EnableEndpointRouting = false;
			})
			.AddNewtonsoftJson()
			.AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.IgnoreNullValues = true;
				options.JsonSerializerOptions.WriteIndented = true;
			});
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "InmobiliariaApi", Version = "v1" });
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options =>//el sitio web valida con cookie
				{
					options.LoginPath = "/Usuarios/Login";
					options.LogoutPath = "/Usuarios/Logout";
					options.AccessDeniedPath = "/Home/Restringido";
					//options.ExpireTimeSpan = TimeSpan.FromMinutes(5);//Tiempo de expiración
				})
				.AddJwtBearer(options =>//la api web valida con token
				{
					options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = Configuration["TokenAuthentication:Issuer"],
						ValidAudience = Configuration["TokenAuthentication:Audience"],
						IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(
							Configuration["TokenAuthentication:SecretKey"])),
					};
					// opción extra para usar el token el hub
					options.Events = new JwtBearerEvents
					{
						OnMessageReceived = context =>
						{
							// Read the token out of the query string
							var accessToken = context.Request.Query["access_token"];
							// If the request is for our hub...
							var path = context.HttpContext.Request.Path;
							if (!string.IsNullOrEmpty(accessToken) &&
								path.StartsWithSegments("/chatsegurohub"))
							{//reemplazar la url por la usada en la ruta ⬆
								context.Token = accessToken;
							}
							return Task.CompletedTask;
						}
					};
				});

            services.AddDbContext<DataContext>(
				options => options.UseMySql(
					Configuration["ConnectionStrings:DefaultConnection"],
					ServerVersion.AutoDetect(Configuration["ConnectionStrings:DefaultConnection"])
				)
			);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "InmobiliariaApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
