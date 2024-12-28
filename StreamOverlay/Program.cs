var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

var app = builder.Build();
if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
}

app.UseWebSockets(new WebSocketOptions {
    KeepAliveInterval = TimeSpan.FromSeconds(60)
});
app.MapControllers();
await app.RunAsync();