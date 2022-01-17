using Microsoft.EntityFrameworkCore;
using NetBlog.Entities.Concreate;
using NetBlog.Shared.Data.Concreate.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetBlog.Data.Abstract.EntityFramework.Contexts
{
    public class EfRoleRepository : EfEntityRepositoryBase<Role>, IRoleRepository
    {
        public EfRoleRepository(DbContext context) : base(context)
        {

        }
    }
}
