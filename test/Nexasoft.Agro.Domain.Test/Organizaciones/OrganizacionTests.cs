
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
        var ruc = "12345678965";
        var estado = (int)EstadosEnum.Activo;
        var observaciones = "Todo ok";
        var fechaCreacion = DateTime.UtcNow;
        var usuarioCreacion = "aroblesa";


        // Act
        var organizacion = Organizacion.Create(
            nombreOrganizacion,
            contactoOrganizacion,
            telefono,
            sectorId,
            ruc,
            observaciones,
            estado,
            fechaCreacion,
            usuarioCreacion
        );

        // Assert
        // Assert
        Assert.NotNull(organizacion);
        Assert.Equal(nombreOrganizacion, organizacion.NombreOrganizacion);
        Assert.Equal(contactoOrganizacion, organizacion.ContactoOrganizacion);
        Assert.Equal(telefono, organizacion.TelefonoContacto);
        Assert.Equal(sectorId, organizacion.SectorId);
        Assert.Equal(estado, organizacion.EstadoOrganizacion);
        Assert.Equal(observaciones, organizacion.Observaciones);
        Assert.Equal(fechaCreacion, organizacion.FechaCreacion);
        Assert.Equal(usuarioCreacion, organizacion.UsuarioCreacion);
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
            "12345678965",
            "Todo ok",
            (int)EstadosEnum.Activo,
            DateTime.UtcNow,
            "aroblesa"
        );

        var nuevoNombre = "AgroNexa Update";
        var nuevoContacto = "Ana López";
        var nuevoTelefono = "123456789";
        var nuevoSectorId = 2;
        var fechaUpdate = DateTime.UtcNow;
        var nuevoRuc = "12345678965";
        var nuevaObservaciones = "Todo ok";
        var usuarioModificacion = "aroblesa";

        // Act
        var result = organizacion.Update(
            organizacion.Id,
            nuevoNombre,
            nuevoContacto,
            nuevoTelefono,
            nuevoSectorId,
            nuevoRuc,
            nuevaObservaciones,
            fechaUpdate,
            usuarioModificacion
        );

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(nuevoNombre, organizacion.NombreOrganizacion);
        Assert.Equal(nuevoContacto, organizacion.ContactoOrganizacion);
        Assert.Equal(nuevoTelefono, organizacion.TelefonoContacto);
        Assert.Equal(nuevoSectorId, organizacion.SectorId);
        Assert.Equal(nuevoRuc, organizacion.RucOrganizacion);
        Assert.Equal(nuevaObservaciones, organizacion.Observaciones);
        Assert.Equal(fechaUpdate, organizacion.FechaModificacion);
        Assert.Equal(usuarioModificacion, organizacion.UsuarioModificacion);
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
            "12345678962",
            "Todo ok",
            (int)EstadosEnum.Activo,
            DateTime.UtcNow,
            "aroblesa"
        );

        var fechaEliminacion = DateTime.UtcNow;
        var usuarioEliminacion = "aroblesa";

        // Act
        var result = organizacion.Delete(fechaEliminacion, usuarioEliminacion);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal((int)EstadosEnum.Eliminado, organizacion.EstadoOrganizacion);
        Assert.Equal(fechaEliminacion, organizacion.FechaEliminacion);
        Assert.Equal(usuarioEliminacion, organizacion.UsuarioEliminacion);
    }
}
