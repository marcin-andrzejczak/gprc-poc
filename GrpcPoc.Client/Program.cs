using static GrpcPoc.Hybrid.HybridResourceTokenBroker;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGrpcClient<HybridResourceTokenBrokerClient>(configureClient =>
{
    var url = builder.Configuration["ServerApi:GrpcBaseUrl"];
    configureClient.Address = new Uri(url);
});

builder.Services.AddHttpClient("ServerApi", options =>
{
    var url = builder.Configuration["ServerApi:RestBaseUrl"];
    options.BaseAddress = new Uri(url);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();