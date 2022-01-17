using NetBlog.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlog.Entities.Concreate
{
    public class User:EntityBase,IEntitiy
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public string username { get; set; }
        public int RoleID {get; set; }
        public Role Role { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public ICollection<Article> Articles { get; set; }

    }
}
