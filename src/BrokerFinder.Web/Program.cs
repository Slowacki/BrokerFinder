using BrokerFinder.Core.Configuration;
using BrokerFinder.Funda.Configuration;
using BrokerFinder.Web.Components;

namespace BrokerFinder.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;
        
        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        services.AddCore();
        services.AddFunda(options => builder.Configuration.Bind("Funda", options));
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}