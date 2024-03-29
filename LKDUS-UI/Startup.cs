using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LKDUS_UI.Data;
using Blazored.LocalStorage;
using LKDUS_UI.Contracts;
using LKDUS_UI.Service;
using System.IdentityModel.Tokens.Jwt;
using LKDUS_UI.Providers;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.Localisation;
 
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;

namespace LKDUS_UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });
            //
            services.AddBlazoredLocalStorage();
            services.AddHttpClient();
            services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();

            
            
            
            services.AddScoped<ApiAuthenticationStateProvider>();
           

            services.AddTransient<IMeasurementPositionsRepository, MeasurementPositionRepository>();
            services.AddTransient<IMeasurementRepository, MeasurementRepository>();
            services.AddTransient<IMachinesRepository, MachineRepository>();
            services.AddTransient<IMeasurementTypeRepository, MeasurementTypeRepository>();
            services.AddTransient<IPacksRepository, PackRepository>();
            services.AddTransient<IDefectRepository, DefectRepository>();
            services.AddTransient<IClasssRepository, ClasssRepository>();
            services.AddTransient<IMeasurementRangeRepository, MeasurementRangeRepository>();
            
            
            services.AddTransient<IFusPacksRepository, FusPackRepository>();


            // services.AddScoped<IMeasurementPositionsRepository, MeasurementPositionRepository>();



            services.AddBlazoredLocalisation();
            services.AddScoped<JwtSecurityTokenHandler>();



            services.AddScoped<AuthenticationStateProvider>(prop =>
                prop.GetRequiredService<ApiAuthenticationStateProvider>());

            services.AddScoped<JwtSecurityTokenHandler>();

            //
            services.AddSingleton<ToastService>();
            services.AddMvc();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddProgressiveWebApp();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                 

            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

           // app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
           // app.UseHttpsRedirection();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
              endpoints.MapFallbackToPage("/_Host");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                //d endpoints.MapFallbackToPage("/index.html");

            });

            
        }
    }
}
