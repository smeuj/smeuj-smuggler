using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

using JsonObject = System.Collections.Generic.Dictionary<string, string?>;

namespace SmeujSmuggler {

    internal static class Deserialization {

        public static ClientConfig? ReadConfig(string file_path) {
            using (var reader = new StreamReader(file_path))
                return JsonConvert.DeserializeObject<ClientConfig>(
                    reader.ReadToEnd()
                );
        }

        public static IEnumerable<Smeu> ReadSmeuj(string file_path) {
            List<SmeuEntry>? entries = null;
            using (var reader = new StreamReader(file_path))
                entries = JsonConvert.DeserializeObject<List<SmeuEntry>>(
                    reader.ReadToEnd()
                );
            if (entries is null)
                yield break;
            foreach (var entry in entries)
                yield return SmeuFromJson(entry);
        }

        private static Smeu SmeuFromJson(SmeuEntry entry)
            => new Smeu(
                entry.Author,
                entry.Inspirations,
                ParseDateTime(entry.Date, entry.Time),
                entry.Content,
                entry.Examples.Select(example => new Example(
                    example.Author,
                    ParseDateTime(example.Date, example.Time),
                    example.Content
                )).ToList()
            );

        private static DateTime ParseDateTime(string date, string time) {
            var date_tokens = date.Split('/').Select(int.Parse).ToArray();
            var time_tokens = time.Split(':').Select(int.Parse).ToArray();
            return new DateTime(
                date_tokens[2] + 2000,
                date_tokens[0],
                date_tokens[1],
                time_tokens[0],
                time_tokens[1],
                0
            );
        }

        private record SmeuEntry(
            string Author,
            List<string> Inspirations,
            string Date,
            string Time,
            string Content,
            List<ExampleEntry> Examples
        );

        private record ExampleEntry(
            string Content,
            string Author,
            string Date,
            string Time
        );

    }

}
