using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetBlog.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlog.Data.Concreate.EntityFramework.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();
            builder.Property(u => u.Email).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(50);
            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.username).IsRequired();
            builder.Property(u => u.username)
                .HasMaxLength(50);
            builder.HasIndex(u => u.username).IsUnique();
            builder.Property(u => u.PasswordHash).IsRequired();
            builder.Property(u => u.PasswordHash).HasColumnType("VARBINARY(500)");
            builder.Property(u => u.Description).HasMaxLength(500);
            builder.Property(u => u.Firstname).IsRequired();
            builder.Property(u => u.Firstname).HasMaxLength(30);
            builder.Property(u => u.Lastname).IsRequired();
            builder.Property(u => u.Lastname).HasMaxLength(30);
            builder.Property(u => u.Picture).IsRequired();
            builder.Property(u => u.Picture).HasMaxLength(255);
            builder.HasOne<Role>(u => u.Role).WithMany(r => r.Users).HasForeignKey(u=>u.RoleID);
            builder.Property(u => u.CreatedByName).IsRequired();
            builder.Property(u => u.CreatedByName).HasMaxLength(50);
            builder.Property(u => u.ModifiedByName).IsRequired();
            builder.Property(u => u.ModifiedByName).HasMaxLength(50);
            builder.Property(u => u.CreatedOn).IsRequired();
            builder.Property(u => u.ModifiedOn).IsRequired();
            builder.Property(u => u.IsActive).IsRequired();
            builder.Property(u => u.IsDeleted).IsRequired();
            builder.ToTable("Users");

            builder.HasData(new User {
            Id=1,
            RoleID=1,
            Firstname="Semih",
            Lastname="Ozhan",
            username="semihozhan",
            Email="semihozhan@yandex.com",
            IsActive=true,
            IsDeleted=false,
            CreatedByName="InitialCreate",
            CreatedOn=DateTime.Now,
            ModifiedByName="InitialCreate",
            ModifiedOn=DateTime.Now,
            Description="First User",
            Note="Admin",
            PasswordHash= Encoding.ASCII.GetBytes("e10adc3949ba59abbe56e057f20f883e"),
            Picture= "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcSX4wVGjMQ37PaO4PdUVEAliSLi8-c2gJ1zvQ&usqp=CAU"
            });
        }
    }
}
