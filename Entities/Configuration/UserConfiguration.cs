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
    public  class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData
            (
            new User
            {
                Id = 1,
                Name = "Sam Raiden",
                Age = 26,
                Email = "Sam@mai.ru",
            },
             new User
             {
                 Id = 2,
                 Name = "Bob Reel",
                 Age = 30,
                 Email = "Bob@mai.ru",
             },
             new User
             {
                 Id = 3,
                 Name = "Green Fron",
                 Age = 34,
                 Email = "Green@mai.ru",
             },
              new User
              {
                  Id = 4,
                  Name = "Black Jack",
                  Age = 38,
                  Email = "Black@mai.ru",
              },
              new User
              {
                  Id = 5,
                  Name = "Zhurko Dima",
                  Age = 47,
                  Email = "Zhurko@mai.ru",
              }
            );

        }
    }
}
