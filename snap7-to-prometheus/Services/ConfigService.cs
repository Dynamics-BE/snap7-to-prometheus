using snap7_to_prometheus.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
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

            // Check tags
            Console.WriteLine($"Checking tags...");
            var allTags = readConfig.DBReads.SelectMany(db => db.Tags);
            // Check if strings tags have a matrics name
            var stringsTags = allTags.Where(tag => tag.Type == "String");
            var stringsTagsWithMetricsName = stringsTags.Where(tag => !String.IsNullOrEmpty(tag.MetricsName));
            foreach (var tag in stringsTagsWithMetricsName)
            {
                Console.WriteLine("Strings tags can't have a metrics name. Prometheus does not accept string data types. Removing Metrics name.");
                tag.MetricsName = null;
            }

            // Attach labels
            Console.WriteLine($"Attaching labels...");
            // Attach the tags to labels
            var tagsWithAttachableLable = allTags.Where(t => t.Labels != null);
            foreach (var tag in tagsWithAttachableLable)
            {
                foreach (var label in tag.Labels.Where(l => !String.IsNullOrEmpty(l.ValueTagName)))
                {
                    var valueTag = allTags.Where(tag => tag.Name == label.ValueTagName).FirstOrDefault();
                    if (valueTag == null)
                    {
                        Console.WriteLine($"In tag {tag.FormattedTagReference} for label {label.Name}: cannot find Tag {label.ValueTagName}, skipping...");
                        continue;
                    }
                    else if (!String.IsNullOrEmpty(label.Value))
                    {
                        Console.WriteLine($"In tag {tag.FormattedTagReference} for label {label.Name}: has a ValueTag and a Value, ignoring value.");
                        label.Value = null;
                    }
                    label.ValueTag = valueTag;

                }
            }


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
