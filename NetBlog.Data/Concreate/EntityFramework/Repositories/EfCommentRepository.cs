using Microsoft.EntityFrameworkCore;
using NetBlog.Data.Abstract;
using NetBlog.Entities.Concreate;
using NetBlog.Shared.Data.Concreate.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlog.Data.Concreate.EntityFramework.Contexts
{
 
        public class EfCommentRepository : EfEntityRepositoryBase<Comment>, ICommentRepository
        {
            public EfCommentRepository(DbContext context) : base(context)
            {

            }
        }
    
}
