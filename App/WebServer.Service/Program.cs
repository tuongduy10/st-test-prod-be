AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddWebServerConfigurations(configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
// Redirect root URL to Swagger
app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();

app.UseWebServerConfiguration();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.Run();
