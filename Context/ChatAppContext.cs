using ChatApp.Context.EntityClasses;
using ChatApp.Context.EntityClasses.Group;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Context
{
    public class ChatAppContext : DbContext
    {
        public ChatAppContext(DbContextOptions<ChatAppContext> options)
           : base(options)
        {
        }
        /// <summary>
        /// Gets or sets Answers.
        /// </summary>
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<Salt> Salts { get; set; }
        public virtual DbSet<Connection> Connections { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Areas> Areas { get; set; }
        public virtual DbSet<GeneralMessages> GeneralMessages { get; set; }
    }
}
