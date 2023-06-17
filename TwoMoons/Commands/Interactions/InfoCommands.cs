using Discord;
using Discord.Interactions;
using TwoMoons.Settings;
using TwoMoons.Util;
// ReSharper disable UnusedMember.Global

namespace TwoMoons.Commands.Interactions;

public class InfoCommands : InteractionModuleBase<ShardedInteractionContext>
{
    [SlashCommand("support", "Get helpful links for the TwoMoons project.")]
    public async Task Support()
    {
        await DeferAsync();

        var embed = Embeds.MakeBuilder();
        embed.Title = "Thank you for your interest in TwoMoons.";
        embed.Color = Color.Green;
        embed.ThumbnailUrl = BotVariables.Images.Avatar;
        embed.Description = Format.Bold("--- Useful links:") +
                            $"\n<:discord:994211332301791283> [Support Server]({BotVariables.DiscordInvite})" +
                            "\n<:twitter:994216171110932510> [Twitter](https://twitter.com/MoonieGZ)" +
                            "\n\n" + Format.Bold("--- Contribute to upkeep:") +
                            "\n• <:kofi:994212063041835098> Donate one-time or monthly on [Ko-Fi](https://ko-fi.com/mooniegz)" +
                            "\n• <:paypal:994215375141097493> Donate any amount through [PayPal](https://donate.tryfelicity.one)" +
                            "\n• <:github:994212386204549160> Become a sponsor on [GitHub](https://github.com/sponsors/MoonieGZ)" +
                            "\n• <:twitch:994214014055895040> Subscribe on [Twitch](https://twitch.tv/subs/MoonieGZ) *(free once per month with Amazon Prime)*";

        await FollowupAsync(embed: embed.Build());
    }
}