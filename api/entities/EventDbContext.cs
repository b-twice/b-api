using Microsoft.EntityFrameworkCore;
using B.API.Models.Event;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace B.API.Entities
{
    // DB Context gets registered in configure services
    public class EventDatabaseContext : DbContext
    {
        public EventDatabaseContext(DbContextOptions<EventDatabaseContext> options)
            : base(options)
        { }

        public DbSet<Event> Events { get; set; }
        public DbSet<ReoccuringType> ReoccuringTypes { get; set; }
        public DbSet<EventUser> EventUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            eventModelCreating(modelBuilder);
            reoccuringTypeCreating(modelBuilder);
            eventUserCreating(modelBuilder);
        }

        private void eventModelCreating(ModelBuilder modelBuilder) {
            var builder = modelBuilder.Entity<Event>();
            var converter = new BoolToZeroOneConverter<int>();
            modelBuilder.Entity<Event>(t =>
            {
                t.HasKey(o => o.id);
                t.Property(o => o.complete).HasConversion(converter);
                // t.Property(o => o.important).HasConversion(converter);
                // t.Property(o => o.reoccuringType).HasColumnName("reoccuring_type");
                // t.Property(o => o.EventUserId).HasColumnName("event_user_id");
            });
            builder.ToTable("Events");


            modelBuilder.Entity<Event>()
                .Property<int>("EventUserId").HasColumnName("event_user_id");

            modelBuilder.Entity<Event>()
                .HasOne<EventUser>(s => s.eventUser)
                .WithMany()
                .HasForeignKey("EventUserId")
                .HasPrincipalKey(u => u.id);
        }

        private void reoccuringTypeCreating(ModelBuilder modelBuilder) {
            var builder = modelBuilder.Entity<ReoccuringType>();
            modelBuilder.Entity<ReoccuringType>(t =>
            {
                t.HasKey(o => o.id);
            });
            builder.ToTable("ReoccuringTypes");
        }


        private void eventUserCreating(ModelBuilder modelBuilder) {
            var builder = modelBuilder.Entity<EventUser>();
            modelBuilder.Entity<EventUser>(t =>
            {
                t.HasKey(o => o.id);
                t.Property(o => o.authId).HasColumnName("auth_id");
            });
            builder.ToTable("EventUsers");

 
        }


    }
}
