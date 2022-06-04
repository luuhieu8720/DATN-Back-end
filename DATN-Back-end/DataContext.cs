using DATN_Back_end.Extensions;
using DATN_Back_end.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DATN_Back_end
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Report>()
                .HasMany<Comment>(x => x.Comments);

            modelBuilder.Entity<User>()
                .HasData(new User()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Username = "admin",
                    Email = "lethiluuhieu@gmail.com",
                    Password = "admin123456".Encrypt(),
                    FirstName = "Admin",
                    LastName = "01",
                    Role = Role.Admin
                });
        }

        public virtual DbSet<Department> Departments { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<FormRequest> FormRequests { get; set; }

        public virtual DbSet<Report> Reports { get; set; }

        public virtual DbSet<RequestType> RequestTypes { get; set; }

        public virtual DbSet<FormStatus> FormStatuses { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<Timekeeping> Timekeepings { get; set; }

        public virtual DbSet<ForgetPassword> ForgetPasswords { get; set; }
    }
}
