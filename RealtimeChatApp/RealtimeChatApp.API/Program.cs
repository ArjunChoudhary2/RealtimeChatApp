using Microsoft.EntityFrameworkCore;
using Npgsql;
using RealtimeChatApp.RealtimeChatApp.API.Hubs;
using RealtimeChatApp.RealtimeChatApp.Business.Services;
using RealtimeChatApp.RealtimeChatApp.DataAccess.Repository;
using RealtimeChatApp.RealtimeChatApp.DataAccess.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Register Npgsql connection
builder.Services.AddScoped<NpgsqlConnection>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("SupabaseConnection");
    return new NpgsqlConnection(connectionString);
});

// Register the DbContext with Execution Retry enabled
//builder.Services.AddDbContext<ChatAppDbContext>(options =>
//    options.UseNpgsql(builder.Configuration.GetConnectionString("SupabaseConnection"), npgsqlOptions =>
//    {
//        npgsqlOptions.EnableRetryOnFailure(
//            maxRetryCount: 2, // Number of retry attempts
//            maxRetryDelay: TimeSpan.FromSeconds(5), // Max delay between retries
//            errorCodesToAdd: null // Optional: Add specific PostgreSQL error codes for retry
//        );
//    })
//);
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
// Add other repositories if needed

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
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/chathub"); // Map the SignalR Hub
app.Run();
