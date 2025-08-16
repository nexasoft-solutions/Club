using NexaSoft.Agro.Domain.Masters.Ubigeos;
using NexaSoft.Agro.Domain.Masters.Ubigeos.Events;
using static NexaSoft.Agro.Domain.Shareds.Enums;

namespace Nexasoft.Agro.Domain.Test.Ubigeos;

public class UbigeoTests
{
    [Fact]
    public void Create_ShouldRaiseUbigeoCreatedDomainEvent()
    {
        // Arrange
        var descripcion = "Lima";
        Guid? padreId = null;
        var nivel = 1; // Nivel.Departamento → reemplazado por su valor numérico
        var estado = (int)EstadosEnum.Activo;
        var fechaCreacion = DateTime.UtcNow;

        // Act
        var ubigeo = Ubigeo.Create(descripcion, nivel, padreId, estado, fechaCreacion);

        // Assert
        Assert.NotNull(ubigeo);
        Assert.Equal(descripcion, ubigeo.Descripcion);
        Assert.Equal(nivel, ubigeo.Nivel);
        Assert.Equal(estado, ubigeo.EstadoUbigeo);
        Assert.Contains(ubigeo.GetDomainEvents(), e => e is UbigeoCreateDomainEvent);
    }

    [Fact]
    public void Update_ShouldUpdateUbigeoAndRaiseDomainEvent()
    {
        // Arrange
        var ubigeo = Ubigeo.Create(
            descripcion: "Lima",
            nivel: 1,
            padreId: null,
            estadoUbigeo: (int)EstadosEnum.Activo,
            fechaCreacion: DateTime.UtcNow
        );

        var nuevaDescripcion = "Lima Metropolitana";
        var nuevoNivel = 2;
        var nuevoPadreId = Guid.NewGuid();
        var fechaModificacion = DateTime.UtcNow;

        // Act
        var result = ubigeo.Update(ubigeo.Id, nuevaDescripcion, nuevoNivel, nuevoPadreId, fechaModificacion);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(nuevaDescripcion, ubigeo.Descripcion);
        Assert.Equal(nuevoNivel, ubigeo.Nivel);
        Assert.Equal(nuevoPadreId, ubigeo.PadreId);
        Assert.Equal(fechaModificacion, ubigeo.FechaModificacion);
        Assert.Contains(ubigeo.GetDomainEvents(), e => e is UbigeoUpdateDomainEvent);
    }

    [Fact]
    public void Delete_ShouldSetEstadoEliminadoAndSetFechaEliminacion()
    {
        // Arrange
        var ubigeo = Ubigeo.Create(
            descripcion: "Lima",
            nivel: 1,
            padreId: null,
            estadoUbigeo: (int)EstadosEnum.Activo,
            fechaCreacion: DateTime.UtcNow
        );

        var fechaEliminacion = DateTime.UtcNow;

        // Act
        var result = ubigeo.Delete(fechaEliminacion);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal((int)EstadosEnum.Eliminado, ubigeo.EstadoUbigeo);
        Assert.Equal(fechaEliminacion, ubigeo.FechaEliminacion);
    }
}
