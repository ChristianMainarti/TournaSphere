namespace TournaSphere
{
    using Newtonsoft.Json;
    using System;
    using System.IO;

    public class JsonExporter
    {
        private readonly string _exportDirectory;

        public JsonExporter(string exportDirectory)
        {
            _exportDirectory = exportDirectory;
            if (!Directory.Exists(_exportDirectory))
            {
                Directory.CreateDirectory(_exportDirectory);
            }
        }

        public void ExportTournament(List<Match> matches, string fileName)
        {
            try
            {
                var fullPath = Path.Combine(_exportDirectory, $"{fileName}.json");
                var jsonContent = JsonConvert.SerializeObject(new
                {
                    Matches = matches
                }, Formatting.Indented);

                File.WriteAllText(fullPath, jsonContent);

                Console.WriteLine($"Tournament successfully exported to: {fullPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error exporting tournament: {ex.Message}");
            }
        }
    }
}
