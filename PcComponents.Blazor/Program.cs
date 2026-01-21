using PcComponents.Blazor.Clients;
using PcComponents.Blazor.Components;

namespace PcComponents.Blazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents().AddInteractiveServerComponents();

            var pcComponentApiUrl = 
                builder.Configuration["componentApiUrl"] ?? throw new Exception("PC Component API URL is not set");

            builder.Services.AddHttpClient<PcComponentsClient>(client => client.BaseAddress = new Uri(pcComponentApiUrl));
            builder.Services.AddHttpClient<CategoriesClient>(client => client.BaseAddress = new Uri(pcComponentApiUrl));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error"); 
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);  
            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();

            // Server-side rendering enabled
            app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
