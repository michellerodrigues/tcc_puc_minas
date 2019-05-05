using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueService.DataContext;
using EstoqueService.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EstoqueService.Services.Interfaces;
using EstoqueService.Services.Messages;
using EstoqueService.Data.Interfaces;

namespace EstoqueService
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
            
            services.AddDbContext<AppDataContext>(
                
                option => option.UseSqlServer(Configuration.GetConnectionString("Default")),
                ServiceLifetime.Transient
            );
             
            var context = services.BuildServiceProvider().GetService<AppDataContext>();

            
             services.AddTransient<UnitOfWork>();
             var unit = services.BuildServiceProvider().GetService<UnitOfWork>();
         
            //services.AddSingleton<IEstoqueApiService>(new unit);
            //services.RegisterServices();
            services.AddSingleton<IEstoqueApiService>(new EstoqueApiService(unit)); 
            services.AddMvc();

 


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
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            DBInicializar.StartDataBase(app);
        }
    }
}
