using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RecruitmentApi.Extentions;
using RecruitmentApi.Middlewares;
using RecruitmentCore;
using RecruitmentInfrastructure.Data;
using RecruitmentInfrastructure.Data.Interface;
using System.Configuration;

var host = new WebHostBuilder()
            .UseKestrel()
            .ConfigureServices(async services =>
            {
                services.AddCors();
                services.AddRouting();
                services.AddControllers();
                services.AddAntiforgery();
                services.AddCore();

                services.AddScoped<ICosmoDbService, CosmosDbService>();


                /*var serviceProvider = new ServiceCollection()
                .AddSingleton(configuration)
                .AddSingleton<ICosmoDbService, CosmosDbService>()
                .BuildServiceProvider();*/

                // Resolve ICosmosDbService
                //var cosmosDbService = serviceProvider.GetRequiredService<ICosmoDbService>();

                // Dispose of the service provider when done
                //serviceProvider.Dispose();

                services.ConfigureServices();
                var serviceProvider = services
                .AddSingleton<ICosmoDbService, CosmosDbService>()
                .BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();
                var cosmosDbService = scope.ServiceProvider.GetRequiredService<CosmosDbService>();
                await cosmosDbService.InitializeAsync();

            })
            .Configure(app =>
            {
                app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
                app.ConfigureExceptionHandler();
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