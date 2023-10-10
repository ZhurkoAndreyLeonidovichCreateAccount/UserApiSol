using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    public class UserIdentityConfiguration : IEntityTypeConfiguration<UserIdentity>
    {
        public void Configure(EntityTypeBuilder<UserIdentity> builder)
        {
            builder.HasData(
                new UserIdentity
                {    
                     Id = 1,
                     EmailAddress = "Andrei@mail.ru",
                     Name = "Andrei",
                     LastName = "Zhurko",
                     Password = "E10ADC3949BA59ABBE56E057F20F883E",
                     Role = "Admin"
                });

        }
    }
}
