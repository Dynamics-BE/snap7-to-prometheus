using snap7_to_prometheus.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace snap7_to_prometheus.Services
{
    public class ConfigService
    {
        public Configuration ActiveConfig { get; set; }
        private FileSystemWatcher watcher;
        public ConfigService()
        {
            ReadConfiguration();
            StartFileWatch();
        }

        public void ReadConfiguration()
        {
            Console.WriteLine($"Reading config file...");

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(PascalCaseNamingConvention.Instance)
                .Build();

            Configuration readConfig = null;
            // Read the file
            try
            {
                // Open the text file using a stream reader.
                using (var sr = new StreamReader("config.yml"))
                {
                    readConfig = deserializer.Deserialize<Configuration>(sr);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The config file could not be read.");
                Console.WriteLine(e.Message);
            }

            Console.WriteLine($"Config file read succesfully.");

            ActiveConfig = readConfig;
        }

        #region Monitor config file changes

        private void StartFileWatch()
        {
            //Doesn't seem to work in docker yet.
            watcher = new FileSystemWatcher(Directory.GetCurrentDirectory(), "config.yml");
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            watcher.Changed += OnChanged;
            watcher.Renamed += OnChanged; //
            watcher.EnableRaisingEvents = true;
            Console.WriteLine($"Current directory {Directory.GetCurrentDirectory()}");
            Console.WriteLine($"Watching config file for changes.");
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            Console.WriteLine($"Config file changed, rereading.");
            ReadConfiguration();
        }

        #endregion

    }
}
