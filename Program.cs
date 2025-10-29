using Microsoft.EntityFrameworkCore;
using ResumeAligner.Components;
using ResumeAligner.Data;
using ResumeAligner.Services;
using System.Net.Http;

namespace ResumeAligner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Standard Blazor Server setup
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor().AddCircuitOptions(o => o.DetailedErrors = true);

            // Ensure an HttpClient service is available for component injection
            // Register IHttpClientFactory for named/typed clients if needed
            builder.Services.AddHttpClient();

            // Also register HttpClient as the concrete service type components expect
            builder.Services.AddScoped<HttpClient>(_ =>
                new HttpClient { BaseAddress = new Uri(builder.Configuration["AppBaseUrl"] ?? "https://localhost:7081/") });

            // Add controllers and text extraction service for binary-file previews
            builder.Services.AddControllers();
            builder.Services.AddSingleton<TextExtractionService>();

            // Database and application services
            builder.Services.AddDbContext<ResumeAlignerDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<WorkspaceService>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ResumeMatcherService>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.Use(async (context, next) =>
            {
                Console.WriteLine("Before session middleware");
                await next.Invoke();
                Console.WriteLine("After session middleware");
            });

            app.UseAntiforgery();

            // SignalR endpoint for Blazor Server
            app.MapBlazorHub();

            // Enable API controllers
            app.MapControllers();

            // Serve the Razor host page as the fallback — _Host.cshtml is at @page "/_Host"
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}