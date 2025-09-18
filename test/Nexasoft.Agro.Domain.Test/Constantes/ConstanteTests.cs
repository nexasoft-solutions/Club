
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
        var usuarioCreacion = "aroblesa";

        // Act
        var constante = Constante.Create(
            tipoConstante,
            clave,
            valor,
            estadoConstante,
            fechaCreacion,
            usuarioCreacion
        );

        // Assert
        Assert.NotNull(constante);
        Assert.Equal(tipoConstante, constante.TipoConstante);
        Assert.Equal(clave, constante.Clave);
        Assert.Equal(valor, constante.Valor);
        Assert.Equal(estadoConstante, constante.EstadoConstante);
        Assert.Equal(fechaCreacion, constante.FechaCreacion);
        Assert.Equal(usuarioCreacion, constante.UsuarioCreacion);
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
        var usuarioCreacion = "aroblesa";

        var constante = Constante.Create(
            tipoConstanteInicial,
            clave,
            valorInicial,
            estadoConstante,
            fechaCreacion,
            usuarioCreacion
        );

        var nuevoTipoConstante = "TipoEmpresa";
        var nuevoValor = "Empresa";
        var fechaModificacion = DateTime.UtcNow;
        var usuarioModificacion = "aroblesa";

        // Act
        var result = constante.Update(
            constante.Id,
            nuevoTipoConstante,
            nuevoValor,
            fechaModificacion,
            usuarioModificacion
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
        var usuarioCreacion = "aroblesa";


        var constante = Constante.Create(
            tipoConstante,
            clave,
            valor,
            estadoConstante,
            fechaCreacion,
            usuarioCreacion
        );

        var fechaEliminacion = DateTime.UtcNow;
        var usuarioEliminacion = "aroblesa";


        // Act
        var result = constante.Delete(fechaEliminacion, usuarioEliminacion);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal((int)EstadosEnum.Eliminado, constante.EstadoConstante);
        Assert.Equal(fechaEliminacion, constante.FechaEliminacion);
    }

}
