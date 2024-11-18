using Library.WebUI;
using Library.WebUI.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App> ("#app");
builder.RootComponents.Add<HeadOutlet> ("head::after");

builder.AddHttpServices ();

builder.Services.AddMudComponentsServices ();
builder.Services.AddAppServices ();

await builder.Build ().RunAsync ();
