using NeoBoilerplate.Application;
using NeoBoilerplate.Application.Contracts;
using NeoBoilerplate.gRPC.LoggedServices;
using NeoBoilerplate.gRPC.Middleware;
using NeoBoilerplate.gRPC.Services;
using NeoBoilerplate.gRPC.Services.v1;
using NeoBoilerplate.Identity;
using NeoBoilerplate.Infrastructure;
using NeoBoilerplate.Persistence;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager Configuration = builder.Configuration;

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.ListenAnyIP(10043);

    // BloomRPC needs pure HTTP/2 instead of HTTP/1.1+HTTP/2
    options.ListenLocalhost(5050, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});

builder.Services.AddGrpc(
    options =>
    {
        options.Interceptors.Add<ExceptionInterceptor>();
    });
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDataProtection();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(Configuration);
builder.Services.AddPersistenceServices(Configuration);
builder.Services.AddIdentityServices(Configuration);
builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();



var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

//#if (Database == "PGSQL")
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
//#endif
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCustomExceptionHandler();
app.UseEndpoints(endpoints =>
{
    //For registeration of v1 services
    endpoints.MapGrpcService<EventService>();
    endpoints.MapGrpcService<CategoryService>();
    endpoints.MapGrpcService<AccountService>();

    //For registeration of v2 services
    endpoints.MapGrpcService<NeoBoilerplate.gRPC.Services.v2.CategoryService>();

    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
    });
});

app.Run();

public partial class Program{ }