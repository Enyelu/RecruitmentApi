using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

var host = new WebHostBuilder()
            .UseKestrel()
            .ConfigureServices(services =>
            {
                services.AddCors();
                services.AddRouting();
                services.AddControllers();
                services.AddAntiforgery();
                
            })
            .Configure(app =>
            {
                app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
                app.UseHttpsRedirection();
                app.UseRouting(); // Enable routing

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                });
            })
            .Build();

host.Run();