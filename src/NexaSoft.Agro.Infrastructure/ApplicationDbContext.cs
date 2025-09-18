using System.Linq.Expressions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NexaSoft.Agro.Application.Exceptions;
using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Infrastructure;

public class ApplicationDbContext : DbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;
    private IDbContextTransaction? _currentTransaction;  // Variable para la transacción


    public ApplicationDbContext(
        DbContextOptions options,
        IPublisher publisher
        ) : base(options)
    {
        _publisher = publisher;
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction != null)
        {
            throw new InvalidOperationException("Ya hay una transacción en curso.");
        }
        //await PublishDomainEventsAsync();
        _currentTransaction = await Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction == null)
        {
            throw new InvalidOperationException("No se ha iniciado una transacción.");
        }

        // 1. Publica eventos que podrían agregar o modificar entidades
        await PublishDomainEventsAsync();

        // 2. Guarda lo que hicieron los handlers de eventos
        await base.SaveChangesAsync(cancellationToken);

        // 3. Cierra la transacción
        await _currentTransaction.CommitAsync(cancellationToken);
        _currentTransaction = null!;  // Limpiar la transacción
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction == null)
        {
            throw new InvalidOperationException("No se ha iniciado una transacción.");
        }

        await _currentTransaction.RollbackAsync(cancellationToken);
        _currentTransaction = null!;  // Limpiar la transacción
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        try
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            //await PublishDomainEventsAsync();
            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyExceptions("La excepcion por concurrencia se disparo", ex);
        }

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        //base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // 👉 Aplicar filtro global para FechaEliminacion
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(Entity).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var property = Expression.Property(parameter, nameof(Entity.FechaEliminacion));
                var nullConstant = Expression.Constant(null, typeof(DateTime?));
                var condition = Expression.Equal(property, nullConstant);
                var lambda = Expression.Lambda(condition, parameter);
                entityType.SetQueryFilter(lambda);
            }
        }

        base.OnModelCreating(modelBuilder);

    }

    private async Task PublishDomainEventsAsync()
    {
        var domainEvents = ChangeTracker
        .Entries<Entity>()
        .Select(entry => entry.Entity)
        .SelectMany(entity =>
        {
            var domainEvents = entity.GetDomainEvents();
            entity.ClearDomainEvents();
            return domainEvents;
        }).ToList();

        foreach (var domainEvent in domainEvents)
        {
            Console.WriteLine($"Publicando evento de dominio: {domainEvent.GetType().Name}");
            await _publisher.Publish(domainEvent);
        }
    }
}
