using Conta.Application.Interfaces;
using Conta.Domain.Interfaces.IRepositories;
using Conta.Domain.Interfaces.IServices;
using Conta.Repository.Repositories;
using Conta.Application;
using Conta.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Conta.WebAPP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appstting.json", optional: false, reloadOnChange: true)
                .AddJsonFile("config.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // var sqlConnection = Configuration.GetConnectionString("DefaultConnection");

            //services.AddDbContext<FrotaContext>(options =>
            //    options.UseSqlServer(sqlConnection));

            // Application
            services.AddScoped<IContaCorrenteApplication, ContaCorrenteApplication>();
            services.AddScoped<IMovimentacaoApplication, MovimentacaoApplication>();

            // Services
            services.AddScoped<IContaCorrenteService, ContaCorrenteService>();
            services.AddScoped<IMovimentacaoService, MovimentacaoService>();

            // Repostories
            services.AddScoped<IContaCorrenteRepository, ContaCorrenteRepository>();
            services.AddScoped<IMovimentacaoRepository, MovimentacaoRepository>();


            AutoMapper.Mapper.Initialize((cfg) =>
            {
                cfg.AddProfile<Application.Mappings.MappingProfile>();
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=ContaCorrente}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "movimentacao",
                    template: "{controller=Movimentacao}/{action=Debitar}/{idConta?}");
            });
        }
    }
}
