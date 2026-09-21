using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Project
{
    public sealed class BotService : IDisposable
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;

        public BotService()
        {
            var config = new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.Guilds |
                                GatewayIntents.GuildMessages |
                                GatewayIntents.MessageContent
            };

            _client = new DiscordSocketClient(config);
            _commands = new CommandService();
        }

        public async Task IniciarAsync(string token)
        {
            _client.Log += LogAsync;

            await RegisterCommandsAsync().ConfigureAwait(false);
            await _client.LoginAsync(TokenType.Bot, token).ConfigureAwait(false);
            await _client.StartAsync().ConfigureAwait(false);

            await Task.Delay(-1).ConfigureAwait(false);
        }

        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }

        private async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), services: null).ConfigureAwait(false);
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            // Casteo tradicional compatible con C# 6
            var message = arg as SocketUserMessage;
            if (message == null || message.Author.IsBot)
            {
                return;
            }

            int argPos = 0;
            if (message.HasCharPrefix('!', ref argPos))
            {
                var context = new SocketCommandContext(_client, message);
                var result = await _commands.ExecuteAsync(context, argPos, services: null).ConfigureAwait(false);

                if (!result.IsSuccess)
                {
                    Console.WriteLine($"Error ejecutando comando: {result.ErrorReason}");
                }
            }
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }

    public class ComandosModulo : ModuleBase<SocketCommandContext>
    {
        [Command("Hola")]
        [Summary("Saluda al usuario")]
        public async Task HolaAsync()
        {
            await ReplyAsync($"¡Hola, {Context.User.Mention}! 👋").ConfigureAwait(false);
        }

        [Command("Info")]
        [Summary("Muestra información sobre los creadores del bot")]
        public async Task InfoAsync()
        {
            var embed = new EmbedBuilder()
                .WithTitle("🤖 Información del Bot")
                .WithDescription("Este bot fue creado como un proyecto de demostración en C#.")
                .AddField("👨‍💻 Creadores", "Desarrollado por el equipo de proyecto.", false)
                .AddField("🛠 Lenguaje y Librería", "C# con Discord.Net", false)
                .WithColor(Color.Blue)
                .WithCurrentTimestamp()
                .Build();

            await ReplyAsync(embed: embed).ConfigureAwait(false);
        }
    }
}