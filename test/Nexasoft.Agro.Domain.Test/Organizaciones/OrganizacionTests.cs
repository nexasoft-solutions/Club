
using NexaSoft.Agro.Domain.Features.Organizaciones;
using NexaSoft.Agro.Domain.Features.Organizaciones.Events;
using static NexaSoft.Agro.Domain.Shareds.Enums;

namespace Nexasoft.Agro.Domain.Test.Organizaciones;

public class OrganizacionTests
{
    [Fact]
    public void Create_ShouldInitializeOrganizacionCorrectly()
    {
        // Arrange
        var nombreOrganizacion = "AgroNexa";
        var contactoOrganizacion = "Juan Pérez";
        var telefono = "987654321";
        var sectorId = 3;
        var estado = (int)EstadosEnum.Activo;
        var fechaCreacion = DateTime.UtcNow;

        // Act
        var organizacion = Organizacion.Create(
            nombreOrganizacion,
            contactoOrganizacion,
            telefono,
            sectorId,
            estado,
            fechaCreacion
        );

        // Assert
        Assert.NotNull(organizacion);
        Assert.Equal(nombreOrganizacion, organizacion.NombreOrganizacion);
        Assert.Equal(contactoOrganizacion, organizacion.ContactoOrganizacion);
        Assert.Equal(telefono, organizacion.TelefonoContacto);
        Assert.Equal(sectorId, organizacion.SectorId);
        Assert.Equal(estado, organizacion.EstadoOrganizacion);
        Assert.Single(organizacion.GetDomainEvents());
    }

    [Fact]
    public void Update_ShouldModifyOrganizacionProperties()
    {
        // Arrange
        var organizacion = Organizacion.Create(
            "Original Org",
            "Carlos García",
            "999999999",
            1,
            (int)EstadosEnum.Activo,
            DateTime.UtcNow
        );

        var nuevoNombre = "AgroNexa Update";
        var nuevoContacto = "Ana López";
        var nuevoTelefono = "123456789";
        var nuevoSectorId = 2;
        var fechaUpdate = DateTime.UtcNow;

        // Act
        var result = organizacion.Update(
            organizacion.Id,
            nuevoNombre,
            nuevoContacto,
            nuevoTelefono,
            nuevoSectorId,
            fechaUpdate
        );

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(nuevoNombre, organizacion.NombreOrganizacion);
        Assert.Equal(nuevoContacto, organizacion.ContactoOrganizacion);
        Assert.Equal(nuevoTelefono, organizacion.TelefonoContacto);
        Assert.Equal(nuevoSectorId, organizacion.SectorId);
        Assert.Equal(fechaUpdate, organizacion.FechaModificacion);
        var updateEvents = organizacion.GetDomainEvents().OfType<OrganizacionUpdateDomainEvent>().ToList();
        Assert.Single(updateEvents);
    }

    [Fact]
    public void Delete_ShouldSetEstadoToEliminadoAndSetFechaEliminacion()
    {
        // Arrange
        var organizacion = Organizacion.Create(
            "Org A Eliminar",
            "María Ruiz",
            "000000000",
            4,
            (int)EstadosEnum.Activo,
            DateTime.UtcNow
        );

        var fechaEliminacion = DateTime.UtcNow;

        // Act
        var result = organizacion.Delete(fechaEliminacion);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal((int)EstadosEnum.Eliminado, organizacion.EstadoOrganizacion);
        Assert.Equal(fechaEliminacion, organizacion.FechaEliminacion);
    }
}
