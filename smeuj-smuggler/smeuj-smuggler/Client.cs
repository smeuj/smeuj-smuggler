using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;

namespace SmeujSmuggler {

    internal record ClientConfig(
        string Token,
        int    Delay, // in ms
        ulong  Channel
    );

    internal sealed class Client {

        private DiscordSocketClient client;
        private ClientConfig        config;

        public event Action? OnLoggedIn;

        public Client(ClientConfig config) {
            this.client = new();
            this.config = config;
            client.Log   += this.Log;
        }

        public async Task LoginAsync() {
            client.Ready += () => {
                OnLoggedIn?.Invoke();
                return Task.CompletedTask;
            };
            await client.LoginAsync(TokenType.Bot, config.Token);
            await client.StartAsync();
        }

        public async Task DumpSmeujAsync(IEnumerable<Smeu> smeuj) {
            var channel = client.GetChannel(config.Channel)
                as ISocketMessageChannel;
            if (channel is null) {
                Console.WriteLine(
                    "The channel couldn't be found or is inaccessible."
                );
                return;
            }
            foreach (var smeu in smeuj) {
                await channel.SendMessageAsync(MakeSmeuMessage(smeu));
                await Task.Delay(config.Delay);
            }
        }

        private Task Log(LogMessage message) {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }

        private string MakeSmeuMessage(Smeu smeu)
            => $"_{smeu.Content}_\nGenoemd door **{smeu.Author}** op "
            +  $"{smeu.Time.ToString("g")}"
            + (
                smeu.Inspiration is not null
                ? $"\nGeïnspireerd door {smeu.Inspiration}"
                : string.Empty
            )
            + (
                smeu.Example is not null
                ? $"\n\"{smeu.Example}\""
                : string.Empty
            );
    }

}
