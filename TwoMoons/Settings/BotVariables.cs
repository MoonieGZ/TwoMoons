using System.Diagnostics;
using Discord.WebSocket;

namespace TwoMoons.Settings
{
    public class BotVariables
    {
        internal static bool IsDebug;
        internal static string? Version;

        public const string DiscordInvite = "https://discord.gg/JBBqF6Pw2z";
        
        internal const string ErrorMessage = $"You can report this error either in our [Support Server]({DiscordInvite}) " +
                                             "or by creating a new [Issue](https://github.com/MoonieGZ/TwoMoons/issues/new?assignees=mooniegz&labels=bug&template=bug-report.md&title=) on GitHub.";
        public static SocketTextChannel? DiscordLogChannel { get; set; }

        public static async Task Initialize()
        {
            if (Debugger.IsAttached)
            {
                IsDebug = true;
                Version = "dev-env";
            }
            else
            {
                using var httpClient = new HttpClient();
                var s = await httpClient.GetStringAsync(
                    "https://raw.githubusercontent.com/MoonieGZ/TwoMoons/main/CHANGELOG.md");
                Version = s.Split("Version: v")[1];
            }
        }

        public class Images
        {
            public const string Avatar = "https://cdn.tryfelicity.one/images/TwoMoons/avatar.png";
            public const string SadFace = "https://cdn.tryfelicity.one/images/peepoSad.png";
        }
    }
}
