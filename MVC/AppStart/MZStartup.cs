using MegaZord.Framework.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MegaZord.Framework.MVC.AppStart {
    public abstract class MZStartup {
        public IConfigurationRoot Configuration { get; protected set; }
        protected abstract string GetDefaultController { get; }
        protected abstract string GetDefaultAction { get; }

        public MZStartup(IHostingEnvironment env) {
            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public virtual void ConfigureServices(IServiceCollection services) {
            services.AddSession();
            services.AddMvc();
        }

        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseDeveloperExceptionPage();
            app.UseExceptionHandler("/Home/Error");
            app.UseStaticFiles();
            app.UseDefaultFiles();
            app.UseFileServer();
            app.UseSession(new SessionOptions() { IdleTimeout = System.TimeSpan.FromMinutes(5) });
            app.UseMvc();

            var baseRoute = string.Format("{{controller={0}}}/{{action={1}}}/{{id?}}", this.GetDefaultController, this.GetDefaultAction);

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: baseRoute);
            });

            MZHelperPath.Configure(env);
        }


    }
}
