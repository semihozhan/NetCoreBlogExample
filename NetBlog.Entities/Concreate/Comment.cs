using NetBlog.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlog.Entities.Concreate
{
    public class Comment: EntityBase,IEntitiy
    {
        public string Text { get; set; }
        public int AticleID { get; set; }
        public Article Article { get; set; }

    }
}
