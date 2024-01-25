using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
namespace Core.Persistence.Repositories;
public abstract class Entity
{
}
public abstract class Entity<T> : Entity
    where T : struct
{
    public T Id { get; set; }

    public Entity()
    {

    }
    public Entity(T id) :this()
    {
        Id = id;
    }
}
public abstract class BaseTimeStampEntity : Entity
{
    public DateTime CreatedAt { get; set; }
    public Guid CreatedUserId { get; set; }
    public required string CreatedUser { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedUserId { get; set; }
    public string? UpdatedUser { get; set; }
    public bool? IsUpdated { get; set; } = false;
    public DateTime? DeletedAt { get; set; }
    public Guid? DeletedUserId { get; set; }
    public string? DeletedUser { get; set; }
    public bool? IsDeleted { get; set; } = false;
}
public abstract class BaseTimeStampEntity<T> : BaseTimeStampEntity
    where T : struct
{
    public T Id { get; set; }
}