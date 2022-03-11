using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace SafetyChainChallenge
{
    internal class Program
    {
        static void Main(string[] args) =>
            CreateHostBuilder(args).Build().RunAsync();

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddHostedService<DirectoryWorker>()
                        .AddScoped<IDirectoryService, DirectoryService>());
    }
}
