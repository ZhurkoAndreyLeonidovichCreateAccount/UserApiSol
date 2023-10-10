using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;


namespace Repository.Extensions
{
    public static class RepositoryUserExtensions
    {
        public static IQueryable<User> FilterUsers(this IQueryable<User>
       employees, uint minAge, uint maxAge) => employees.Where(e => (e.Age >= minAge && e.Age <= maxAge));

        public static IQueryable<User> Search(this IQueryable<User> users, UserParameters parameters)
        {
            if (!string.IsNullOrEmpty(parameters.Name))
            {
                var lowerCaseName = parameters.Name.Trim().ToLower();
                return users.Where(u => u.Name.ToLower().Contains(lowerCaseName));
            }
            else if (!string.IsNullOrEmpty(parameters.Email))
            {
                var lowerCaseEmail = parameters.Email.Trim().ToLower();
                return users.Where(u => u.Email.ToLower().Contains(lowerCaseEmail));
            }
            else if (!string.IsNullOrEmpty(parameters.Role))
            {
                var lowerCaseRole = parameters.Role.Trim().ToLower();
                return users.Where(u => u.Roles.Select(r => r.RoleName).Contains(parameters.Role));
            }
            else
            {
                return users;
            }


        }

        public static IQueryable<User> Sort(this IQueryable<User> users, string? orderByQueryString)

        {
            if (string.IsNullOrWhiteSpace(orderByQueryString)) return users.OrderBy(e => e.Name);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(User).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param)) continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi =>
                    pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null) continue;

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction},");

            }
            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            if (string.IsNullOrWhiteSpace(orderQuery)) return users.OrderBy(e => e.Name);

            return users.OrderBy(orderQuery);

        }

    }

    }
