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
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c=>c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Name).HasMaxLength(70);
            builder.Property(c => c.Description).HasMaxLength(255);
            builder.Property(c => c.CreatedByName).IsRequired();
            builder.Property(c => c.CreatedByName).HasMaxLength(50);
            builder.Property(c => c.ModifiedByName).IsRequired();
            builder.Property(c => c.ModifiedByName).HasMaxLength(50);
            builder.Property(c => c.CreatedOn).IsRequired();
            builder.Property(c => c.ModifiedOn).IsRequired();
            builder.Property(c => c.IsActive).IsRequired();
            builder.Property(c => c.IsDeleted).IsRequired();
            builder.ToTable("Categories");

            builder.HasData(new Category { 
                Id=1,
                Name="c#",
                Description="c# Category",
                IsActive = true,
                IsDeleted = false,
                CreatedByName = "InitialCreate",
                CreatedOn = DateTime.Now,
                ModifiedByName = "InitialCreate",
                ModifiedOn = DateTime.Now,
                Note="c#",
            }, new Category
            {
                Id = 2,
                Name = "c++",
                Description = "c++ Category",
                IsActive = true,
                IsDeleted = false,
                CreatedByName = "InitialCreate",
                CreatedOn = DateTime.Now,
                ModifiedByName = "InitialCreate",
                ModifiedOn = DateTime.Now,
                Note = "c++",
            }, new Category
            {
                Id = 3,
                Name = "Javascript",
                Description = "Javascript Category",
                IsActive = true,
                IsDeleted = false,
                CreatedByName = "InitialCreate",
                CreatedOn = DateTime.Now,
                ModifiedByName = "InitialCreate",
                ModifiedOn = DateTime.Now,
                Note = "Javascript",
            }
            );

        }
    }
}
