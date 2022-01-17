using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlog.Shared.Entities.Abstract
{
    public abstract class EntityBase
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreatedOn { get; set; } = DateTime.Now;

        public virtual DateTime ModifiedOn { get; set; } = DateTime.Now;

        public virtual bool IsDeleted { get; set; } = false;

        public virtual bool IsActive { get; set; } = true;
        public virtual string CreatedByName { get; set; } = "Admin";
        public virtual string ModifiedByName { get; set; } = "Admin";

        public virtual string Note { get; set; }

    }
}
