using NetBlog.Data.Abstract;
using NetBlog.Data.Abstract.EntityFramework.Contexts;
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
        private readonly EfRoleRepository _RoleRepository;
        private readonly EfUserRepository _UserRepository;

        public UnitOfWork(NetBlogContext context)
        {
            _context = context;
        }
        public IArticleRepository Articles => _ArticleRepository ?? new EfArticleRepository(_context);

        public ICategoryRepository Categories => _CategoryRepository ?? new EfCategoryRepository(_context);

        public ICommentRepository Comments => _CommentRepository ?? new EfCommentRepository(_context);

        public IRoleRepository Roles => _RoleRepository ?? new EfRoleRepository(_context);

        public IUserRepository Users => _UserRepository ?? new EfUserRepository(_context);

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
