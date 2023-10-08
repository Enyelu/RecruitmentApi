﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecruitmentApi.Extentions;
using RecruitmentInfrastructure.Data;
using RecruitmentInfrastructure.Data.Interface;


var host = new WebHostBuilder()
            .UseKestrel()
            .ConfigureServices(async services =>
            {
                services.AddCors();
                services.AddRouting();
                services.AddControllers();
                services.AddAntiforgery();

                services.AddScoped<ICosmoDbService, CosmosDbService>();

                services.ConfigureServices();
                var serviceProvider = services.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();
                var cosmosDbService = scope.ServiceProvider.GetRequiredService<CosmosDbService>();
                await cosmosDbService.InitializeAsync();

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