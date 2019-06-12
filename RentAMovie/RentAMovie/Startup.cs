using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentAMovie.Core.Services;
using RentAMovie.Infrastructure.Context;
using RentAMovie.Infrastructure.Logic;
using Swashbuckle.AspNetCore.Swagger;

namespace RentAMovie
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IBorrowRepository, BorrowRepository>();
            services.AddScoped<IBorrowService, BorrowService>();
            //services.AddScoped<IAddressRepository, AddressRepository>();
            //services.AddScoped<IAddressService, AddressService>();
            services.AddDbContext<MovieContext>(options =>
                options.UseSqlite("DataSource=dbo.RentAMovieApi.db",
                    builder => builder.MigrationsAssembly("RentAMovie.Infrastructure")
                ));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {Title="RentAMovie", Version="v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RentAMovieApi V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}