using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GSmartHR.Web.Infrastructure
{
    public static class AppConfiguration
    {

        public static void Configure(this IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseDeveloperExceptionPage();

            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();

            }
            else
            {
                // app.UseExceptionHandler("/error");

               // app.UseRewriter(new RewriteOptions().AddRedirectToHttpsPermanent());
            }


            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseStaticHttpContext();

            app.UseRequestLocalization();

            app.UseCors("MyPolicy");

            app.UseMvc(cfg =>
            {

                cfg.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );


                cfg.MapRoute("Default",
                  "{controller}/{action}/{id?}",
                  new { controller = "User", Action = "Login" });
            });      
        }
    }
}
