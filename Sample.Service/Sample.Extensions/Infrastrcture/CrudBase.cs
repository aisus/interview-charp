using System;

namespace Sample.Extensions.Infrastrcture
{
    public interface IEntity
    {
        public Guid Id { get; set; }
    }

    public interface IAuditableEntity
    {
        public DateTimeOffset? CreatedDate { get; set; }
        public DateTimeOffset? LastModifiedDate { get; set; }
    }

    public class EntityBase : IEntity
    {
        public Guid Id { get; set; }
    }

    public class AuditableEntity : EntityBase, IAuditableEntity
    {
        public DateTimeOffset? CreatedDate { get; set; }
        public DateTimeOffset? LastModifiedDate { get; set; }
    }
}