using System;
using CommandLine;
using Discord;
using Discord.WebSocket;

using SmeujSmuggler;

Parser.Default.ParseArguments<Options>(args)
    .WithParsed(options => {
        Console.WriteLine(options.IdentityFilePath);
        Console.WriteLine(options.SmeujFilePath);
    })
    .WithNotParsed(errors => {
        foreach (var error in errors) {
            Console.WriteLine(error);
        }
    });
