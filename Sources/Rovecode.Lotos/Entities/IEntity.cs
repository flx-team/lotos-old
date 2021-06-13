using System;

namespace Lotos.Entities
{
    public interface IEntity
    {
        /// <summary>
        /// Id of entity in mongo db.
        /// If entitiy no save in db, id is Guid.Empty.
        /// </summary>
        public Guid Id { get; }
    }
}
