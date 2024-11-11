using Npgsql;
using RealtimeChatApp.RealtimeChatApp.Business.Services;

var builder = WebApplication.CreateBuilder(args);

// Register the Npgsql connection (you've already done this)
builder.Services.AddScoped<NpgsqlConnection>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("SupabaseConnection");
    return new NpgsqlConnection(connectionString);
});

// Register DatabaseService to use in controllers
builder.Services.AddScoped<DatabaseService>();

// Add services to the container.
builder.Services.AddControllers();

// Swagger and OpenAPI configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
