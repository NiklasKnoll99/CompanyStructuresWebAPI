using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using CompanyStructuresWebAPI.Interface;
using CompanyStructuresWebAPI.Helper;
using CompanyStructuresWebAPI.Repository;
using Microsoft.Extensions.Configuration;
using CompanyStructuresWebAPI.Model;

namespace CompanyStructuresWebAPI
{
    public class Startup
    {
        public IConfiguration config { get; }

        public Startup(IConfiguration config)
        {
            this.config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ConnectionStrings>(config.GetSection("ConnectionStrings"));

            services.AddSingleton<IDbContext, DbContext>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
