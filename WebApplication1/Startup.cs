using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using WebRepository;
using WebBiz.Interface;
using WebBiz;
using WebRepository.Interface;

namespace WebApplication1
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();//启用session之前必须先添加内存

            string connStr = Configuration.GetConnectionString("DefaultConnection");
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Add(ServiceDescriptor.Singleton(typeof(DBContext), new DBContext(connStr)));
            services.Add(ServiceDescriptor.Transient(typeof(ILoginBiz), typeof(LoginBiz)));
            services.Add(ServiceDescriptor.Transient(typeof(ILoginRepository), typeof(LoginRepository)));
            
            services.AddCors(options =>
            {
                options.AddPolicy("any",
                    builder => builder
                    .WithOrigins("http://localhost:5001")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .SetPreflightMaxAge(TimeSpan.FromSeconds(2520)));
            });

            services.AddSession();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromHours(24);
            });
            services.AddSwaggerGen(c =>
            {
                
                    c.SwaggerDoc("V1", new Info
                    {
                        Version = "v1",
                        Title = $"API接口 v1",
                        
                        TermsOfService = "None"
                    });
               
                
                c.CustomSchemaIds(type => type.FullName);
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "SwaggerUIDemo.xml");
                c.IncludeXmlComments(xmlPath);
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="provider"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                
                c.SwaggerEndpoint("/swagger/V1/swagger.json", "ApiHelp V1");
                
                
            });
            app.UseHttpsRedirection();
            app.UseSession();
            app.UseCors("any");
            
            app.UseMvc();
        }
    }
}
