using Ohara.API.Domain.Interfaces;

namespace Ohara.API.Domain.Entities;

public class Entity : IEntity
{
    public Guid Id { get; set; }
}
