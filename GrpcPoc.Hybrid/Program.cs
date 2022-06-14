using GrpcPoc.Hybrid.GRPCServices;
using GrpcPoc.Hybrid.Services;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IResourceTokenService, ResourceTokenService>();

builder.Services.AddSingleton(serviceProvider =>
    new CosmosClient(builder.Configuration.GetConnectionString("DefaultCosmosConnectionString"))
);

var app = builder.Build();

app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseEndpoints(options =>
{
    options.MapGrpcService<HybridResourceTokenBroker>();
    options.MapControllers();
});

app.Run();

public partial class Program { }

public class HybridProgram : Program { }