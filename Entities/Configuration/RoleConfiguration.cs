using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Entities.Configuration
{
   public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {

            var user = new Role { Id =1, RoleName = "Users" };
            var admin = new Role { Id = 2, RoleName = "Admin" };
            var support = new Role { Id = 3, RoleName = "Support" };
            var superAdmin = new Role { Id = 4, RoleName = "SuperAdmin" };
            builder.HasData(user, admin, support, superAdmin);


        }
    }
}
