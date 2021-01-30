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
            List<JsonObject>? smeuj = null;
            using (var reader = new StreamReader(file_path))
                smeuj = JsonConvert.DeserializeObject<List<JsonObject>>(
                    reader.ReadToEnd()
                );
            if (smeuj is null)
                yield break;
            foreach (JsonObject smeu in smeuj)
                yield return SmeuFromJson(smeu);
        }

        private static Smeu SmeuFromJson(JsonObject source)
            => new Smeu(
                source["author"]!,
                source["inspiration"],
                ParseDateTime(source["date"]!, source["time"]!),
                source["content"]!,
                source["example"]
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

    }

}
