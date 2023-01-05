using HR.DomainModels.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace HR.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Employee> Employees { get; set; }

        public DbSet<EmployeeLogs> EmployeeLogs { get; set; }

		public DbSet<EmployeesUsers> EmployeesUsers { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var identityUserLogin = modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
            var identityUserRole = modelBuilder.Entity<IdentityUserRole<string>>().HasKey(a => new { a.UserId, a.RoleId });
            var identityUserToken = modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();
            var identityUserClaim = modelBuilder.Entity<IdentityUserClaim<string>>().HasNoKey();
           // Seed Admin and Role With Assign it to Account.
            PasswordHasher<EmployeesUsers> passwordHasher = new PasswordHasher<EmployeesUsers>();
            EmployeesUsers user = new EmployeesUsers
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                PhoneNumber = "01000000000",
                AccessFailedCount = 0,
                PhoneNumberConfirmed = false,
                Emp_ID = 0,

            };
            user.PasswordHash = passwordHasher.HashPassword(user, "Admin@123");
            modelBuilder.Entity<EmployeesUsers>().HasData(user);


            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = "fab4fac1-c546-41de-aebc-a14da6895711",
                    Name = "Admin",
                    ConcurrencyStamp = "1",
                    NormalizedName = "Admin"
                },
                new IdentityRole()
                {
                    Id = "fab4fac1-c546-41de-aebc-a14da6895712",
                    Name = "Employee",
                    ConcurrencyStamp = "2",
                    NormalizedName = "Employee"
                }
                );

            modelBuilder.Entity<IdentityUserRole<string>> ().HasData(
              new IdentityUserRole<string>()
              {
                  RoleId= "fab4fac1-c546-41de-aebc-a14da6895711",
                  UserId = "b74ddd14-6340-4840-95c2-db12554843e5",
                
              });

		}
    }
}
