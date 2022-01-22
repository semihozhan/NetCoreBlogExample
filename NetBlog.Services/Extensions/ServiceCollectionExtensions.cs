using Microsoft.Extensions.DependencyInjection;
using NetBlog.Data.Abstract;
using NetBlog.Data.Concreate;
using NetBlog.Data.Concreate.EntityFramework.Contexts;
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
        public static IServiceCollection LoadMyService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<NetBlogContext>();
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ICategoryService, CategoryManager>();
            serviceCollection.AddScoped<IArticleService, ArticleManager>();

            return serviceCollection;
        }
    }
}
