﻿using Microsoft.EntityFrameworkCore;

namespace WebSignalR.Models
{
    public class UsersDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public UsersDbContext(DbContextOptions<UsersDbContext> options): base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";

            // добавляем тестовые роли
            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };
            // добавляем тестовых пользователей
            User adminUser1 = new User { Id = 1, Email = "admin@mail.com", Password = "123456", RoleId = adminRole.Id };
            User adminUser2 = new User { Id = 2, Email = "tom@mail.com", Password = "123456", RoleId = adminRole.Id };
            User simpleUser1 = new User { Id = 3, Email = "bob@mail.com", Password = "123456", RoleId = userRole.Id };
            User simpleUser2 = new User { Id = 4, Email = "sam@mail.com", Password = "123456", RoleId = userRole.Id };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser1, adminUser2, simpleUser1, simpleUser2 });
            base.OnModelCreating(modelBuilder);
        }
        public class Role
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<User> Users { get; set; }
            public Role()
            {
                Users = new List<User>();
            }
        }
        public class User
        {
            public int Id { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }

            public int? RoleId { get; set; }
            public Role Role { get; set; }
        }
    }
}
