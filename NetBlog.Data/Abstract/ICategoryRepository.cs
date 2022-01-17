using NetBlog.Entities.Concreate;
using NetBlog.Shared.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlog.Data.Abstract
{
    public interface ICategoryRepository : IEntityRepository<Category>
    {
    }
}
