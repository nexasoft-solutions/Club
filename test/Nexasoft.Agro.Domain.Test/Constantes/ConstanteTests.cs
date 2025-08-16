
using NexaSoft.Agro.Domain.Masters.Constantes;
using NexaSoft.Agro.Domain.Masters.Constantes.Events;
using static NexaSoft.Agro.Domain.Shareds.Enums;

namespace Nexasoft.Agro.Domain.Test.Constantes;

public class ConstanteTests
{
    [Fact]
    public void Create_ShouldRaiseConstanteCreatedDomainEvent()
    {
        // Arrange
        var tipoConstante = "TipoOrganizacion";
        var clave = 1;
        var valor = "Organizacion";
        var estadoConstante = (int)EstadosEnum.Activo;
        var fechaCreacion = DateTime.UtcNow;

        // Act
        var constante = Constante.Create(
            tipoConstante,
            clave,
            valor,
            estadoConstante,
            fechaCreacion
        );

        // Assert
        Assert.NotNull(constante);
        Assert.Equal(tipoConstante, constante.TipoConstante);
        Assert.Equal(1, constante?.GetDomainEvents().Count); // Asegura que el evento fue levantado
        Assert.IsType<ConstanteCreateDomainEvent>(constante?.GetDomainEvents().First());
    }

    [Fact]
    public void Update_ShouldUpdateConstanteProperties()
    {
        // Arrange
        var tipoConstanteInicial = "TipoOrganizacion";
        var clave = 1;
        var valorInicial = "Organización";
        var estadoConstante = (int)EstadosEnum.Activo;
        var fechaCreacion = DateTime.UtcNow;

        var constante = Constante.Create(
            tipoConstanteInicial,
            clave,
            valorInicial,
            estadoConstante,
            fechaCreacion
        );

        var nuevoTipoConstante = "TipoEmpresa";
        var nuevoValor = "Empresa";
        var fechaModificacion = DateTime.UtcNow;

        // Act
        var result = constante.Update(
            constante.Id,
            nuevoTipoConstante,
            nuevoValor,
            fechaModificacion
        );

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(nuevoTipoConstante, constante.TipoConstante);
        Assert.Equal(nuevoValor, constante.Valor);
        Assert.Equal(fechaModificacion, constante.FechaModificacion);
    }

    [Fact]
    public void Delete_ShouldSetEstadoConstanteToEliminadoAndSetFechaEliminacion()
    {
        // Arrange
        var tipoConstante = "TipoOrganizacion";
        var clave = 1;
        var valor = "Organización";
        var estadoConstante = (int)EstadosEnum.Activo;
        var fechaCreacion = DateTime.UtcNow;

        var constante = Constante.Create(
            tipoConstante,
            clave,
            valor,
            estadoConstante,
            fechaCreacion
        );

        var fechaEliminacion = DateTime.UtcNow;

        // Act
        var result = constante.Delete(fechaEliminacion);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal((int)EstadosEnum.Eliminado, constante.EstadoConstante);
        Assert.Equal(fechaEliminacion, constante.FechaEliminacion);
    }

}
