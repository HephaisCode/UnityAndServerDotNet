using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Model;
using System.Web;
using System.IO;
using System.Text;

namespace AspEmpty
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public static Dictionary<string, string> ParseQueryString(HttpRequest sRequest)
        {
            var nvc = HttpUtility.ParseQueryString(sRequest.QueryString.Value);
            return nvc.AllKeys.ToDictionary(k => k, k => nvc[k]);
        }

        public static string RequestBody(HttpRequest sRequest)
        {
            string documentContents = "error";
            //sRequest.Form["body"];
            //string requestContent = sRequest.co.ReadAsStringAsync().Result;
            //documentContents = await sRequest.BodyReader.ReadAsync().Result;
            return documentContents;
        }

        public static Dictionary<string, string> ParseHeaders(HttpRequest sRequest)
        {
            Dictionary<string, string> rReturn = sRequest.Headers.ToDictionary(a => a.Key, a => string.Join(";", a.Value));
            return rReturn;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDelete("/", context => {
                    return Task.CompletedTask;
                });
                endpoints.MapGet("/", async context =>
                {
                    Request tRequest = new Request();
                    tRequest.RequestParameters = ParseQueryString(context.Request);
                    tRequest.Headers = ParseHeaders(context.Request);
                    tRequest.Body = "no body in method get";
                    test tTest = new test();
                    await context.Response.WriteAsync(tTest.HelloWithRequest(tRequest));
                });
                //endpoints.MapPost("/", async context =>
                //{
                //    Request tRequest = new Request();
                //    tRequest.Method = context.Request.Method;
                //    tRequest.RequestParameters = ParseQueryString(context.Request);
                //    tRequest.Headers = ParseHeaders(context.Request);

                //    using (var reader = new StreamReader(context.Request.Body))
                //    {
                //        var tBodyString = reader.ReadToEnd();
                //        tRequest.Body = tBodyString;
                //    }

                //    //tRequest.Body = context.Request.BodyReader.ToString();
                //    test tTest = new test();
                //    await context.Response.WriteAsync(tTest.HelloWithRequest(tRequest));
                //});
                endpoints.MapPost("/", context => {
                    Request tRequest = new Request();
                    tRequest.Method = context.Request.Method;
                    tRequest.RequestParameters = ParseQueryString(context.Request);
                    tRequest.Headers = ParseHeaders(context.Request);

                    var body = new StreamReader(context.Request.BodyReader.AsStream());
                    //body.BaseStream.Seek(0, SeekOrigin.Begin);
                    var requestBody = body.ReadToEnd();
                    tRequest.Body = requestBody;
                    test tTest = new test();
                    context.Response.WriteAsync(tTest.HelloWithRequest(tRequest));
                    return Task.CompletedTask;
                });
            });
        }
    }
}
