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
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c=>c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Text).IsRequired();
            builder.Property(c => c.Text).HasMaxLength(1000);
            builder.HasOne<Article>(c => c.Article).WithMany(a => a.Comments).HasForeignKey(c => c.ArticleID);
            builder.Property(c => c.CreatedByName).IsRequired();
            builder.Property(c => c.CreatedByName).HasMaxLength(50);
            builder.Property(c => c.ModifiedByName).IsRequired();
            builder.Property(c => c.ModifiedByName).HasMaxLength(50);
            builder.Property(c => c.CreatedOn).IsRequired();
            builder.Property(c => c.ModifiedOn).IsRequired();
            builder.Property(c => c.IsActive).IsRequired();
            builder.Property(c => c.IsDeleted).IsRequired();
            builder.ToTable("Comments");


            builder.HasData(new Comment { 
                Id=1,
                ArticleID=1,
                Text="C# comment example",
                IsActive = true,
                IsDeleted = false,
                CreatedByName = "InitialCreate",
                CreatedOn = DateTime.Now,
                ModifiedByName = "InitialCreate",
                ModifiedOn = DateTime.Now,
                Note = "Javascript",
            },
            new Comment
            {
                Id = 2,
                ArticleID = 2,
                Text = "C++ comment example",
                IsActive = true,
                IsDeleted = false,
                CreatedByName = "InitialCreate",
                CreatedOn = DateTime.Now,
                ModifiedByName = "InitialCreate",
                ModifiedOn = DateTime.Now,
                Note = "Javascript",
            }, new Comment
            {
                Id = 3,
                ArticleID = 3,
                Text = "Javascript comment example",
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
