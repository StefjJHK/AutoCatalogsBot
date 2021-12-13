using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(AddAppConfiquration) 
                .ConfigureLogging(AddAppLogging)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static void AddAppLogging(HostBuilderContext context, 
            ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.ClearProviders();

            loggingBuilder.AddConsole();
            loggingBuilder.AddAzureWebAppDiagnostics();
            loggingBuilder.AddApplicationInsights();
        }

        public static void AddAppConfiquration(HostBuilderContext builderContext, 
            IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Sources.Clear();

            configurationBuilder.AddJsonFile(@"Configurations/appsettings.json");
            configurationBuilder.AddJsonFile(@"Configurations/Secrets.json");
            configurationBuilder.AddJsonFile(@"Configurations/UtilitySettings.json");
            configurationBuilder.AddJsonFile(@"Configurations/GoogleSheetsParsingSettings.json");
            configurationBuilder.AddJsonFile($"appsettings.{builderContext.HostingEnvironment.EnvironmentName}.json", optional: true);
            configurationBuilder.AddEnvironmentVariables();

            if (builderContext.HostingEnvironment.IsDevelopment())
            {
                configurationBuilder.AddUserSecrets<Startup>();
            }
            else if (builderContext.HostingEnvironment.IsProduction())
            {
                var root = configurationBuilder.Build();

                var url = root["KeyVault:Vault"];
                var tenantId = root["KeyVault:TenantId"];
                var clientId = root["KeyVault:ClientId"];
                var clientSecret = root["KeyVault:SecretKey"];

                var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
                var client = new SecretClient(new Uri(url), credential);

                configurationBuilder.AddAzureKeyVault(client, new AzureKeyVaultConfigurationOptions());
            }
        }
    }
}
