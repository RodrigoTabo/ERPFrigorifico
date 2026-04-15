using ERPFrigorifico.Client.ApiClients;
using ERPFrigorifico.Client.Components;
using MudBlazor.Services;

namespace ERPFrigorifico.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddMudServices();

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents(options =>
                {
                    options.DetailedErrors = true;
                });

            builder.Services.AddHttpClient("Api", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7104/");
            });


            builder.Services.AddScoped<IngresoApi>();
            builder.Services.AddScoped<FaenaApi>();
            builder.Services.AddScoped<CorralApi>();
            builder.Services.AddScoped<ProveedorApi>();
            builder.Services.AddScoped<OperarioApi>();
            builder.Services.AddScoped<MovimientoAnimalApi>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
