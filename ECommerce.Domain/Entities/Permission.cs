using CSharpFunctionalExtensions;
using ECommerce.Domain.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities
{
    public class Permission : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public List<Role> Roles { get; set; } = [];

        protected Permission() { }

        protected Permission(Guid id, string name, string description)
        {
            Id = id; 
            Name = name; 
            Description = description;
            CreatedDate = DateTimeOffset.UtcNow;
            MakeModify();
        }

        public static Result<Permission> Create(Guid id, string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name)) return Result.Failure<Permission>("Название права не может быть пустым");
            if (string.IsNullOrWhiteSpace(description)) return Result.Failure<Permission>("Описание права не может быть пустым");

            return Result.Success(new Permission(id, name, description));
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }
    }
}
