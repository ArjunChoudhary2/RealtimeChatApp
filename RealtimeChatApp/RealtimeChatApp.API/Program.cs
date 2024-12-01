using Microsoft.EntityFrameworkCore;
using Npgsql;
using RealtimeChatApp.RealtimeChatApp.API.Hubs;
using RealtimeChatApp.RealtimeChatApp.Business.Services;
using RealtimeChatApp.RealtimeChatApp.DataAccess.Repository.IRepository;
using RealtimeChatApp.RealtimeChatApp.DataAccess.Repository;

var builder = WebApplication.CreateBuilder(args);

// Register CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:3000")  // Specify the allowed origin(s)
              .AllowAnyMethod()  // Allow all HTTP methods (GET, POST, etc.)
              .AllowAnyHeader()  // Allow all headers
              .AllowCredentials();  // Allow credentials (cookies, authorization headers, etc.)
    });
});



// Register Npgsql connection
builder.Services.AddScoped<NpgsqlConnection>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("SupabaseConnection");
    return new NpgsqlConnection(connectionString);
});

// Register the DbContext with Execution Retry enabled
builder.Services.AddDbContext<ChatAppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("SupabaseConnection"))
);

// Register Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IChallangeRepository, ChallangeRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IChallengeService, ChallengeService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

// Add controllers
builder.Services.AddControllers();
builder.Services.AddSignalR(); // Add SignalR service

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS before UseAuthorization
app.UseCors("AllowSpecificOrigins");

app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/chathub"); // Map the SignalR Hub
app.Run();
