var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/nazdarSvete", () => "Ahoj světe!");

app.Run();
