using Microsoft.EntityFrameworkCore;
using RealtimeChatApp.RealtimeChatApp.Domain.Entities;
using System;

public class ChatAppDbContext : DbContext
{
    public ChatAppDbContext(DbContextOptions<ChatAppDbContext> options) : base(options) { }

    // DbSets for each entity
    public DbSet<Users> Users { get; set; }
    public DbSet<Chats> Chats { get; set; }
    public DbSet<Messages> Messages { get; set; }
    public DbSet<Notifications> Notifications { get; set; }

    // Add DbSets for other tables (e.g., ChatParticipants, Challenges)
    public DbSet<Challenges> Challenges { get; set; }
    public DbSet<ChatParticipants> ChatParticipants { get; set; }
    public DbSet<UserStatusHistory> UserStatusHistories { get; set; }

    public DbSet<MessageMetadata> MessageMetadata { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Configure entity relationships and constraints
        modelBuilder.Entity<Chats>()
        .HasMany(c => c.Messages)
        .WithOne(m => m.Chat)
        .HasForeignKey(m => m.ChatId) // Reference the ChatId property
        .IsRequired(); // Ensures every message is linked to a chat

    }
}
