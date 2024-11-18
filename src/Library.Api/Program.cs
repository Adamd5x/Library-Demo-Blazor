

using Library.Core.Extensions.DependencyInjection;
using Library.Repository.Extensions.DependencyInjection;
using Library.Api.Extensions.DependencyInjection;
using Library.Repository.Seeder;

var builder = WebApplication.CreateBuilder(args);

builder.Host.SerilogConfiguration ();

builder.Services.AddGlobalExceptionHandling()
                .AddCorsServices()
                .AddInfrastructureServices (builder.Configuration)
                .AddCoreServices ()
                .AddEndpointsApiExplorer ()
                .AddSwaggerGen()
                .AddControllers ();

var app = builder.Build();

app.UseGlobalExceptionHandling ();

using var scope = app.Services.CreateScope();
var dbMigrator = scope.ServiceProvider.GetRequiredService<LibrararyDemoDbMigrator>();
await dbMigrator.MigrateAsync ();


if (app.Environment.IsDevelopment ())
{
    app.UseSwagger ();
    app.UseSwaggerUI ();
}

app.UseCorsServices ();
app.UseHttpsRedirection ();
//app.UseAuthorization ();

app.MapControllers ();

await app.RunAsync ();
