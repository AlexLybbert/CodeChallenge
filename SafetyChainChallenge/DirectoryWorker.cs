using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SafetyChainChallenge
{
    public class DirectoryWorker : BackgroundService
    {
        private readonly IDirectoryService _directoryService;

        public DirectoryWorker(IDirectoryService directoryService) =>
            _directoryService = directoryService;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Directory path?");
            var path = Console.ReadLine();

            if (path == null)
            {
                Console.WriteLine("Please add a path");
                path = Console.ReadLine();
            }
            
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Folder does not exist");

                Console.ReadKey();
                await Task.Delay(100, stoppingToken);
            }
            else
            {
                Console.WriteLine("Output path?");
                var output = Console.ReadLine();

                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var result = GetDirectory(path);
                        Console.WriteLine(result);

                        if (result == null)
                        {
                            Console.WriteLine("Could not read input file");
                        }
                        else
                        {
                            var outputDirectory = Path.GetDirectoryName(output);
                            if (!Directory.Exists(outputDirectory))
                            {
                                Directory.CreateDirectory(Path.GetDirectoryName(output));
                            }

                            StreamWriter sw = new StreamWriter(output);
                            await sw.WriteAsync(result);

                            sw.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Couldn't create output file");
                    }

                    Console.WriteLine("Press any key to close the app");
                    Console.ReadKey();
                    await Task.Delay(100, stoppingToken);
                }
            }
        }

        public string GetDirectory(string path)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                _directoryService.GetDirectory(path, ref sb);
                return sb.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
