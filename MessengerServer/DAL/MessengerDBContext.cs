using MessengerServer.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace MessengerServer.DAL
{
    public class MessengerDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Image> Images { get; set; }

        public DbSet<ChatsUsers> ChatsUsers { get; set; }

        public MessengerDBContext(DbContextOptions<MessengerDBContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Chat>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Message>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Image>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<ChatsUsers>()
                .HasKey(cu => new { cu.UserId, cu.ChatId });

             modelBuilder.Entity<ChatsUsers>()
                .HasOne(x => x.User)
                .WithMany(x => x.Chats)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ChatsUsers>()
                .HasOne(x => x.Chat)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.ChatId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                .HasOne(x => x.Avatar)
                .WithOne(x => x.UserAvatar)
                .HasForeignKey<Image>(x => x.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Messages)
                .WithOne(x => x.Sender)
                .IsRequired();

            modelBuilder.Entity<Message>()
                .HasOne(x => x.Picture)
                .WithOne(x => x.Message)
                .HasForeignKey<Image>(x => x.MessageId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Chat>()
                .HasMany(x => x.Messages)
                .WithOne(x => x.Chat)
                .IsRequired();

        }
    }
}
