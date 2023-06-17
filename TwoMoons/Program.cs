using Discord;
using Discord.WebSocket;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;
using Serilog.Events;
using TwoMoons.Extensions;
using TwoMoons.Settings;

namespace TwoMoons;

public class Program
{
    public static async Task Main()
    {
        try
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Quartz", LogEventLevel.Information)
                .WriteTo.Console()
                .WriteTo.File("Logs/latest-.log", rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 14)
                .CreateLogger();

            await BotVariables.Initialize();

            var builder = WebApplication.CreateBuilder();

            Console.Title = $"TwoMoons v.{BotVariables.Version}";

            builder.Host.UseSerilog((context, services, configuration) =>
            {
                configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .WriteTo.File("Logs/latest-.log", rollingInterval: RollingInterval.Day);
            });

            builder.Host.UseDefaultServiceProvider(o => o.ValidateScopes = false);

            builder.Services
                .AddDiscord(
                    discordClient =>
                    {
                        discordClient.GatewayIntents = GatewayIntents.AllUnprivileged & ~GatewayIntents.GuildInvites &
                                                       ~GatewayIntents.GuildScheduledEvents;
                        discordClient.AlwaysDownloadUsers = false;
                    },
                    _ => { },
                    textCommandsService => { textCommandsService.CaseSensitiveCommands = false; },
                    builder.Configuration)
                .AddLogging(options => options.AddSerilog(dispose: true))
                .AddSingleton<LogAdapter<BaseSocketClient>>();

            /*
            builder.Services.AddMvc();
            builder.Services
                .AddControllers(options => { options.EnableEndpointRouting = false; })
                .AddJsonOptions(x => { });
            builder.Services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin",
                    options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            app.UseRouting();

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                Secure = CookieSecurePolicy.Always
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.UseHttpsRedirection();
            if (!app.Environment.IsDevelopment())
                app.UseHsts();
            */

            await builder.Build().RunAsync();
        }
        catch (Exception exception)
        {
            Log.Fatal(exception, "Host terminated unexpectedly");
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }
}