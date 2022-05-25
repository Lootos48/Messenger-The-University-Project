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

        public MessengerDBContext()
        {
            Database.EnsureCreated();
        }

        public MessengerDBContext(DbContextOptions<MessengerDBContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // обозначаем первичные ключи в таблицах
            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<User>()
                .HasIndex(x => x.Username)
                .IsUnique();

            modelBuilder.Entity<Chat>()
                .HasIndex(x => x.Title)
                .IsUnique();

            modelBuilder.Entity<Chat>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Message>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Image>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<UserPicture>()
                .HasKey(x => x.Id);

            // составной первичный ключ для таблицы связи многие-ко-многим
            modelBuilder.Entity<ChatsUsers>()
                .HasKey(cu => new { cu.UserId, cu.ChatId });

            // настройки отношений в промежуточной таблице ChatsUsers
            modelBuilder.Entity<ChatsUsers>()
               .HasOne(x => x.User)
               .WithMany(x => x.Chats)
               .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<ChatsUsers>()
                .HasOne(x => x.Chat)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.ChatId);

            // связь один-к-одному таблиц User <-> UserPicture
            modelBuilder.Entity<User>()
                .HasOne(x => x.Avatar)
                .WithOne(x => x.PictureOwner)
                .HasForeignKey<UserPicture>(x => x.UserId);

            // связь один-к-многим таблиц User <->> Messages
            modelBuilder.Entity<User>()
                .HasMany(x => x.Messages)
                .WithOne(x => x.Sender)
                .IsRequired();

            // связь один-к-одному таблиц Messages <-> Images
            modelBuilder.Entity<Image>()
                .HasOne(x => x.Message)
                .WithOne(x => x.Picture)
                .HasForeignKey<Message>(x => x.ImageId)
                .OnDelete(DeleteBehavior.SetNull);

            // связь один-ко-многим таблиц Chat <->> Messages
            modelBuilder.Entity<Chat>()
                .HasMany(x => x.Messages)
                .WithOne(x => x.Chat)
                .IsRequired();

        }
    }
}
