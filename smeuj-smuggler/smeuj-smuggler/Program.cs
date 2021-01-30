using System;
using System.Threading.Tasks;
using CommandLine;

using SmeujSmuggler;

Options? options = null;
Parser.Default.ParseArguments<Options>(args)
    .WithParsed(parsed_options => options = parsed_options)
    .WithNotParsed(errors => {
        foreach (var error in errors) {
            Console.WriteLine(error);
        }
    });

if (options is null) return;

var config = Deserialization.ReadConfig(options.IdentityFilePath!);
if (config is null) {
    Console.WriteLine("Could not read config file.");
    return;
}

var smeuj  = Deserialization.ReadSmeuj(options.SmeujFilePath!);
var client = new Client(config);

client.OnLoggedIn += async () => await client.DumpSmeujAsync(smeuj);

await client.LoginAsync();

await Task.Delay(-1);
