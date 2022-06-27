using NLog.Web;
using System.Text;
using TaskService;
using TaskService.API.DataManagers;
using TaskService.API.Interfaces;
using TaskService.Classes;
using TaskService.CommonTypes.Classes;
using TaskService.CommonTypes.Interfaces;
using TaskService.CommonTypes.Sql;
using TaskService.Interfaces;

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile(File.Exists("appsettings.Development.json") ? "appsettings.Development.json" : "appsettings.json")
            .Build();

        SqlDapper.InitDapper(
            config.GetSection("Settings:ConnectionString").Value,
            config.GetSection("Settings:CommandTimeout").Value);

        TaskServiceConst.InitDelay(config.GetSection("Settings:Delay").Value);

        services
            .AddSingleton<IMailServiceDataManager, MailServiceDataManager>()
            .AddSingleton<ITaskDataManager, TaskDataManager>()
            .AddSingleton<IMailService, MailService>()
            .AddSingleton<IPluginLoader, PluginLoader>()
            .AddSingleton<IPluginService, PluginService>()
            .AddHostedService<Worker>();
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.SetMinimumLevel(LogLevel.Trace);
    })
    .UseNLog()
    .UseWindowsService(x => x.ServiceName = "TaskService worker")
    .Build();

await host.RunAsync();
