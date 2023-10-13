using System;

namespace Team.Extensions.Infrastrcture
{
    public interface IEntity
    {
        public Guid Id { get; set; }
    }

    public interface IEntityWithCreatedDate
    {
        public DateTime? CreatedDate { get; set; }
    }

    public class EntityBase : IEntity
    {
        public Guid Id { get; set; }
    }

    public class EntityWithCreatedDate : EntityBase, IEntityWithCreatedDate
    {
        public DateTime? CreatedDate { get; set; }
    }
}