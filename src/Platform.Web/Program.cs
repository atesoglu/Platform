using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Platform.Model.Runtime.Param;
using Platform.Model.Runtime.Response;
using RestSharp;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Platform.Web
{
    public class Program
    {
        public const string AppName = "Platform.Web";

        public static readonly IParamsCollection ParamsCollection = new ParamsCollection();

        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(path: "appSettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
            //.AddJsonFile($"appsettings.Production.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        public static void Main(string[] args)
        {
            CreateParamsCollection();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                //.WriteTo.MSSqlServer(connectionString: ParamsCollection["JournalContext"], restrictedToMinimumLevel: LogEventLevel.Information, sinkOptions: new SinkOptions { TableName = AppName, AutoCreateSqlTable = true })
                .CreateLogger();

            try { Log.Debug($"Hello from {AppName}."); CreateHostBuilder(args).Build().Run(); Log.Information($"{AppName} started."); }
            catch (Exception ex) { Log.Fatal(ex, "Host terminated unexpectedly."); }
            finally { Log.CloseAndFlush(); }
        }

        private static void CreateParamsCollection()
        {
            //Console.WriteLine($"Initiating vault api, path: {Configuration.GetValue<string>("Vault:Api:Path:Base")}");

            //var rest = new RestClient(Configuration.GetValue<string>("Vault:Api:Path:Base"));
            //var request = new RestRequest($"/Vault/{AppName}/Secret", Method.GET);
            //var response = rest.Execute(request);
            //var secrets = JsonSerializer.Deserialize<ResponseModel<ICollection<SecretModel>>>(response.Content);

            //Console.WriteLine($"Vault api response status code: {response.StatusCode}");
            //Console.WriteLine($"Vault api response status description: {response.StatusDescription}");

            //try
            //{
            //    secrets?.Data?.ToList().ForEach(secret => ParamsCollection.Add(new KeyValuePair<string, string>(secret.Name, secret.Value)));
            //    Console.WriteLine("Secrets are consumed:");
            //    Console.WriteLine(string.Join(Environment.NewLine, ParamsCollection));
            //}
            //catch (Exception ex) { Console.WriteLine($"ParamsCollection is not configured properly. Details: {ex}"); }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseConfiguration(Configuration);
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseSerilog();
                });
    }
}