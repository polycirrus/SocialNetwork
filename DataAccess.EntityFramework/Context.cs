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
            var userRole = new Role() { Name = "User" };
            context.Roles.Add(userRole);

            var country = new Country() { Name = "Belarus" };
            context.Countries.Add(country);

            var users = new List<User>()
            {
                new User()
                {
                    Email = "john.smith@abc.com",
                    Password = "jsmithhashed",
                    Roles = new List<Role>() { userRole },
                    FirstName = "John",
                    LastName = "Smith",
                    Bio = "A regular guy.",
                    Country = country
                },
                new User()
                {
                    Email = "john.cena@abc.com",
                    Password = "jcenahashed",
                    Roles = new List<Role>() { userRole },
                    FirstName = "John",
                    LastName = "Cena",
                    Bio = "Too tootoo tooooooo...",
                    Country = country
                },
                new User()
                {
                    Email = "jack.doe@abc.com",
                    Password = "jdoehashed",
                    Roles = new List<Role>() { userRole },
                    FirstName = "Jack",
                    LastName = "Doe",
                    Bio = "My name's not John.",
                    Country = country
                }
            };
            foreach (var user in users)
                context.Users.Add(user);

            context.SaveChanges();
        }
    }
}
