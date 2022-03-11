using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafetyChainChallenge
{
    public class DirectoryService : IDirectoryService
    {
        private readonly ILogger<DirectoryService> _logger;

        public DirectoryService(ILogger<DirectoryService> logger) =>
            _logger = logger;

        public void GetDirectory(string path, ref StringBuilder csvFile)
        {
            try
            {
                foreach (var file in Directory.GetFiles(path))
                {
                    var fileText = File.ReadAllText(file);

                    if (fileText.Length > 0)
                    {
                        dynamic fileInfo = JsonConvert.DeserializeObject(fileText);

                        if (fileInfo.Type is JTokenType.Array)
                        {
                            foreach (var item in fileInfo)
                            {
                                Dictionary<string, dynamic> dict = 
                                    item.ToObject<Dictionary<string, dynamic>>();
                                var obj = string.Join(",", dict.Values);
                                csvFile.AppendLine(obj);
                            }
                        }
                        else if (fileInfo is Object)
                        {
                            Dictionary<string, dynamic> dict =
                                    fileInfo.ToObject<Dictionary<string, dynamic>>();
                            var obj = string.Join(",", dict.Values);
                            csvFile.AppendLine(obj);
                        }
                    }
                }    

                foreach (var directory in Directory.GetDirectories(path))
                {
                    GetDirectory(directory, ref csvFile);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
