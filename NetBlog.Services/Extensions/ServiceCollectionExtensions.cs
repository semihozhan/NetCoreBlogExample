using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetBlog.Data.Abstract;
using NetBlog.Data.Concreate;
using NetBlog.Data.Concreate.EntityFramework.Contexts;
using NetBlog.Entities.Concreate;
using NetBlog.Services.Abstract;
using NetBlog.Services.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlog.Services.Extensions
{
    public static class ServiceCollectionExtensions 
    {
        public static IServiceCollection LoadMyService(this IServiceCollection serviceCollection,string connectionString)
        {
            serviceCollection.AddDbContext<NetBlogContext>(options => options.UseSqlServer(connectionString));
            serviceCollection.AddIdentity<User,Role>(options => {
                //password option
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                //username and mail
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail=true;


            }).AddEntityFrameworkStores<NetBlogContext>();
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ICategoryService, CategoryManager>();
            serviceCollection.AddScoped<IArticleService, ArticleManager>();
            serviceCollection.AddScoped<ICommentService, CommentManager>();
            return serviceCollection;
        }
    }
}
