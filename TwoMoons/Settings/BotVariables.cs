using System.Diagnostics;

namespace TwoMoons.Settings
{
    public class BotVariables
    {
        internal static bool IsDebug;
        internal static string? Version;

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
                    "https://raw.githubusercontent.com/MoonieGZ/FelicityOne/main/CHANGELOG.md");
                Version = s.Split("## [")[1].Split("]")[0];
            }
        }
    }
}
