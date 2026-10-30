using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Extensions;

public partial class Program
{
    private static async Task Main(string[] args)
    {
        WebApplicationBuilder applicationBuilder = WebApplication.CreateBuilder(args);

        applicationBuilder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddDeliveryApi()
    .AddComposers()
    .Build();

        WebApplication webApplication = applicationBuilder.Build();

        await webApplication.BootUmbracoAsync();

        webApplication.UseUmbraco()
            .WithMiddleware(u =>
            {
                u.UseBackOffice();
            })
            .WithEndpoints(u =>
            {
                u.UseDeliveryApiEndpoints();
                u.UseBackOfficeEndpoints();
            });

        await webApplication.RunAsync();
    }
}