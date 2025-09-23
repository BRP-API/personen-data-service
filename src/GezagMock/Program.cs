using Brp.Shared.Infrastructure.Logging;
using Brp.Shared.Infrastructure.Utils;
using GezagMock.Middleware;
using GezagMock.Repositories;
using Serilog;

Log.Logger = SerilogHelpers.SetupSerilogBootstrapLogger();

try
{
    Log.Information("Starting {AppName} v{AppVersion}. TimeZone: {TimeZone}. Now: {TimeNow}",
                        AssemblyHelpers.Name, AssemblyHelpers.Version, TimeZoneInfo.Local.StandardName, DateTime.Now);

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddHttpContextAccessor();

    builder.SetupSerilog(Log.Logger);

    // Add services to the container.

    builder.Services.AddControllers().AddNewtonsoftJson();

    builder.Services.AddScoped<GezagsrelatieRepository>();
    builder.Services.AddScoped<GezagsrelatieRepositoryDeprecated>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.

    app.SetupSerilogRequestLogging();

    app.UseMiddleware<VersionRoutingMiddleware>();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "{AppName} terminated unexpectedly", AssemblyHelpers.Name);
}
finally
{
    Log.CloseAndFlush();
}