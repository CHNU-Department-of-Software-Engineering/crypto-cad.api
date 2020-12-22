using System.IO;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CryptoCAD.Data.Storage;
using CryptoCAD.Data.Storage.Abstractions;
using CryptoCAD.Data.Repositories;
using CryptoCAD.Core.Services;
using CryptoCAD.Core.Services.Abstractions;
using CryptoCAD.Domain.Repositories;

using AutoMapper;
using CryptoCAD.API.Mapper;

namespace CryptoCAD.API
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
            AddAutoMapper(services);
            services.AddControllers();

            //services.AddDbContext<PostgreSqlContext>(options => options.UseNpgsql(Configuration.GetConnectionString("HerokuPostgresql")));
            //services.AddIdentity<IdentityUser, IdentityRole>()
            //    .AddEntityFrameworkStores<PostgreSqlContext>();

            AddServices(services);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = Configuration["Okta:Authority"];
                    options.Audience = "api://default";
                });
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddSwaggerGen((options) =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "CryptoCAD API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CryptoCAD API v1");
            });

            app.UseStaticFiles();
        }

        private void AddAutoMapper(IServiceCollection services)
        {
            var mapperConfiguration = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile(new AutoMapperProfile());
            });

            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
        }

        private void AddServices(IServiceCollection services)
        {
            services.AddSingleton<IStorageContext>(context =>
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), Configuration["Storage:File"]);
                var dropCreate = Configuration["Storage:DropCreate"].ToLower() == "true";

                return new StorageContext(path, dropCreate);
            });
            services.AddTransient<IStandardMethodsRepository, StandardMethodsRepository>();

            services.AddTransient<ICipherService, CipherService>();
            services.AddTransient<IHashService, HashService>();
        }
    }
}