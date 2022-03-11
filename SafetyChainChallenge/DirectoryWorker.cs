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
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = GetDirectory();
                Console.WriteLine(result);

                StreamWriter sw = new StreamWriter("C:\\ChallengeFolder\\Test.txt");

                await sw.WriteAsync(result);

                sw.Close();

                await Task.Delay(1000, stoppingToken);
            }
        }

        public string GetDirectory()
        {
            StringBuilder sb = new StringBuilder();
            _directoryService.GetDirectory("C:\\ChallengeFolder", ref sb);
            return sb.ToString();
        }
    }
}
