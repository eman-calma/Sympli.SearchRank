using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Sympli.SearchRank.Core.Helpers;
using Sympli.SearchRank.Core.Helpers.Abstracts;
using Sympli.SearchRank.Core.Services.SEO.Queries.GetSearchRanking;
using Sympli.SearchRank.Infrastructure.Cache;

namespace Sympli.SearchRank.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddFluentValidation(); ;

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sympli Search Engine Ranking Service", Version = "v1", });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            });

            //FluentValidators
            services.AddTransient<IValidator<GetSearchRankingCommand>, GetSearchRankingCommandValidator>();


            services.AddTransient(typeof(IHttpRequestHelper), typeof(HttpRequestHelper));
            services.AddTransient(typeof(ISearchEngineHelper), typeof(SearchEngineHelper));
            services.AddSingleton(typeof(ICacheData<>), typeof(CacheData<>));
            services.AddTransient(typeof(IExpressionMatchHelper), typeof(ExpressionMatchHelper));

            //Setup Mediator
            services.AddMediatR(typeof(GetSearchRankingCommand).GetTypeInfo().Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(url: "../swagger/v1/swagger.json", name: "Sympli Search Engine Ranking API");
                //c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
