using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.DataAccess.EntityFramework.Entities;

namespace SocialNetwork.DataAccess.EntityFramework
{
    public class Context : DbContext
    {
        public Context() : base("name=SocialNetworkMsSqlDatabase")
        {
            Database.SetInitializer(new Initializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(user => user.Friends).WithMany().Map(cfg =>
            {
                cfg.ToTable("Friendships");
            });

            modelBuilder.Entity<Message>().HasRequired(message => message.From)
                .WithMany(user => user.MessagesFrom).HasForeignKey(message => message.FromId);
            modelBuilder.Entity<Message>().HasRequired(message => message.To)
                .WithMany(user => user.MessagesTo).HasForeignKey(message => message.ToId).WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }

    public class Initializer : DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context context)
        {
            //var roles = new List<Role>()
            //{
            //    new Role() { Name = "User" }
            //};
            context.Roles.Add(new Role() { Name = "User" });

            //context.Users.Add(new User()
            //{
            //    Email = "abc@abc.com",
            //    Password = "qwertyhashed",
            //    Roles = roles,
            //    Friends = new List<User>()
            //    {
            //        new User()
            //        {
            //            Email = "abcsfriend@abc.com",
            //            Password = "ilikedogshashed",
            //            Roles = roles
            //        }
            //    }
            //});
            context.SaveChanges();
        }
    }
}
