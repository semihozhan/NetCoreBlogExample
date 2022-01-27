using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetBlog.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlog.Data.Concreate.EntityFramework.Mappings
{
    public class UserClaimMap:IEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> builder)
        {
            // Primary key
            builder.HasKey(uc => uc.Id);

            // Maps to the AspNetUserClaims table
            builder.ToTable("AspNetUserClaims");
        }

        
    }
}
