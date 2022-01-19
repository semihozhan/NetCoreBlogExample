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
    public class ArticleMap : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a=> a.Id).ValueGeneratedOnAdd();
            builder.Property(a => a.Title).HasMaxLength(255);
            builder.Property(a => a.Title).IsRequired();
            builder.Property(a=>a.Content).IsRequired();
            builder.Property(a=>a.Content).HasColumnType("NVARCHAR(MAX)");
            builder.Property(a=>a.Date).IsRequired();
            builder.Property(a => a.SeoAuthor).IsRequired();
            builder.Property(a => a.SeoAuthor).HasMaxLength(50);
            builder.Property(a => a.SeoDescription).HasMaxLength(150);
            builder.Property(a => a.SeoDescription).IsRequired();
            builder.Property(a => a.SeoTags).IsRequired();
            builder.Property(a => a.SeoTags).HasMaxLength(70);
            builder.Property(a => a.ViewsCount).IsRequired();
            builder.Property(a => a.CommentCount).IsRequired();
            builder.Property(a => a.Thumbnail).IsRequired();
            builder.Property(a => a.Thumbnail).HasMaxLength(250);
            builder.Property(a => a.CreatedByName).IsRequired();
            builder.Property(a => a.CreatedByName).HasMaxLength(50);
            builder.Property(a => a.ModifiedByName).IsRequired();
            builder.Property(a => a.ModifiedByName).HasMaxLength(50);
            builder.Property(a => a.CreatedOn).IsRequired();
            builder.Property(a => a.ModifiedOn).IsRequired();
            builder.Property(a => a.IsActive).IsRequired();
            builder.Property(a => a.IsDeleted).IsRequired();
            builder.Property(a => a.Note).HasMaxLength(500);
            builder.HasOne<Category>(a => a.Category).WithMany(c=>c.Articles).HasForeignKey(a=>a.CategoryId);
            builder.HasOne<User>(a => a.User).WithMany(u => u.Articles).HasForeignKey(a => a.UserId);
            builder.ToTable("Articles");

            builder.HasData(new Article { 
                Id=1,
                CategoryId=1,
                IsActive = true,
                IsDeleted = false,
                CreatedByName = "InitialCreate",
                CreatedOn = DateTime.Now,
                ModifiedByName = "InitialCreate",
                ModifiedOn = DateTime.Now,
                Title="C# 9.0 ve .net 5",
                Content= "Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500'lerden beri endüstri standardı sahte metinler olarak kullanılmıştır. Beşyüz yıl boyunca varlığını sürdürmekle kalmamış, aynı zamanda pek değişmeden elektronik dizgiye de sıçramıştır. 1960'larda Lorem Ipsum pasajları da içeren Letraset yapraklarının yayınlanması ile ve yakın zamanda Aldus PageMaker gibi Lorem Ipsum sürümleri içeren masaüstü yayıncılık yazılımları ile popüler olmuştur.",
                Thumbnail="default.jpg",
                SeoDescription= "C# 9.0 ve .net 5",
                SeoTags= "C# 9.0,.net 5",
                SeoAuthor="Admin",
                Date=DateTime.Now,
                Note= "C# 9.0 ve .net 5",
                UserId=1,
                ViewsCount=130,
                CommentCount=1

            }, new Article
            {
                Id = 2,
                CategoryId = 2,
                IsActive = true,
                IsDeleted = false,
                CreatedByName = "InitialCreate",
                CreatedOn = DateTime.Now,
                ModifiedByName = "InitialCreate",
                ModifiedOn = DateTime.Now,
                Title = "C++ 9.0 ve .net 5",
                Content = "Yinelenen bir sayfa içeriğinin okuyucunun dikkatini dağıttığı bilinen bir gerçektir. Lorem Ipsum kullanmanın amacı, sürekli 'buraya metin gelecek, buraya metin gelecek' yazmaya kıyasla daha dengeli bir harf dağılımı sağlayarak okunurluğu artırmasıdır. Şu anda birçok masaüstü yayıncılık paketi ve web sayfa düzenleyicisi, varsayılan mıgır metinler olarak Lorem Ipsum kullanmaktadır. Ayrıca arama motorlarında 'lorem ipsum' anahtar sözcükleri ile arama yapıldığında henüz tasarım aşamasında olan çok sayıda site listelenir. Yıllar içinde, bazen kazara, bazen bilinçli olarak (örneğin mizah katılarak), çeşitli sürümleri geliştirilmiştir.",
                Thumbnail = "default.jpg",
                SeoDescription = "C++ 9.0 ve .net 5",
                SeoTags = "C++ 9.0,.net 5",
                SeoAuthor = "Admin",
                Date = DateTime.Now,
                Note = "C# 9.0 ve .net 5",
                UserId = 1,
                ViewsCount = 140,
                CommentCount = 1

            }, new Article
            {
                Id = 3,
                CategoryId = 3,
                IsActive = true,
                IsDeleted = false,
                CreatedByName = "InitialCreate",
                CreatedOn = DateTime.Now,
                ModifiedByName = "InitialCreate",
                ModifiedOn = DateTime.Now,
                Title = "Javascript ES2019",
                Content = "Javascript Yinelenen bir sayfa içeriğinin okuyucunun dikkatini dağıttığı bilinen bir gerçektir. Lorem Ipsum kullanmanın amacı, sürekli 'buraya metin gelecek, buraya metin gelecek' yazmaya kıyasla daha dengeli bir harf dağılımı sağlayarak okunurluğu artırmasıdır. Şu anda birçok masaüstü yayıncılık paketi ve web sayfa düzenleyicisi, varsayılan mıgır metinler olarak Lorem Ipsum kullanmaktadır. Ayrıca arama motorlarında 'lorem ipsum' anahtar sözcükleri ile arama yapıldığında henüz tasarım aşamasında olan çok sayıda site listelenir. Yıllar içinde, bazen kazara, bazen bilinçli olarak (örneğin mizah katılarak), çeşitli sürümleri geliştirilmiştir.",
                Thumbnail = "default.jpg",
                SeoDescription = "Javascript ES2019",
                SeoTags = "Javascript",
                SeoAuthor = "Admin",
                Date = DateTime.Now,
                Note = "Javascript",
                UserId = 1,
                ViewsCount = 70,
                CommentCount = 1

            }

            );
        }
    }
}
