using NetBlog.Data.Abstract;
using NetBlog.Data.Concreate.EntityFramework.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlog.Data.Concreate
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NetBlogContext _context;
        private readonly EfArticleRepository _ArticleRepository;
        private readonly EfCategoryRepository _CategoryRepository;
        private readonly EfCommentRepository _CommentRepository;

        public UnitOfWork(NetBlogContext context)
        {
            _context = context;
        }
        public IArticleRepository Articles => _ArticleRepository ?? new EfArticleRepository(_context);

        public ICategoryRepository Categories => _CategoryRepository ?? new EfCategoryRepository(_context);

        public ICommentRepository Comments => _CommentRepository ?? new EfCommentRepository(_context);

     

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
