using System.IO;
using System.Text;
using ePortalApi;
using ApiConnection.ConnectionFactory;
using ePortalApi.ApiConfiguration;
using FluentValidation;
using FluentValidation.AspNetCore;
using ApiConnection.Interfaces;
using ApiConnection.Repositories;
using ePortalApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApiTest.Middlewares;
using ePortalApi.Interfaces;

namespace WebApiTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private IServiceCollection _services;
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                 {
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true,
                         ValidIssuer = "http://localhost:5002/",
                         ValidAudience = "http://localhost:5002/",
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("veryVerySecretKey"))
                     };
                 });
            services.AddScoped<IDbConnectionFactory>(c => new DbConnectionFactory(Configuration.GetConnectionString("ePortal")));
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

           
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddApiVersioning(options => {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(0, 9);
            });

            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");

            //Swagger
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<SwaggerDefaultValues>();
            });
            services.Configure<ConnectionSettings>(Configuration.GetSection("ConnectionStrings"));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped(typeof(TokenService));


            services.AddHealthChecks();

            _services = services;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile($"appsettings{env.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables();//Permite accesar a configuracion en las variables de entorno


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.Map("/allservices", bldr => bldr.Run(async context =>
                {
                    var sb = new StringBuilder();
                    sb.Append("<h1>Servicios Registrados</h1>");
                    sb.Append("<table><thead>");
                    sb.Append("<tr><th>Type</th><th>Lifetime</th><th>Instance</th></tr>");
                    sb.Append("</thead><tbody>");
                    foreach (var svc in _services)
                    {
                        sb.Append("<tr>");
                        sb.Append($"<td>{svc.ServiceType.FullName}</td>");
                        sb.Append($"<td>{svc.Lifetime}</td>");
                        sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</tbody></table>");
                    await context.Response.WriteAsync(sb.ToString()).ConfigureAwait(false);
                }));

                

                //Usaremos Swagger solamente para el entorno de desarrollo (Por Ahora)
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    // build a swagger endpoint for each discovered API version
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                    c.RoutePrefix = string.Empty;
                });
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();

            }

            //Middleware de ePortal para el manejo de excepciones
            app.UseExceptionsMiddleware();
 
            app.UseCors(options => options.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseHealthChecks("/api/health");
            app.UseMvc();
        }
    }
}
