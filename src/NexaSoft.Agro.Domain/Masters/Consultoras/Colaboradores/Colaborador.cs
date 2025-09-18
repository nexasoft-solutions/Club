using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Masters.Users;
using static NexaSoft.Agro.Domain.Shareds.Enums;

namespace NexaSoft.Agro.Domain.Masters.Consultoras.Colaboradores;

public class Colaborador : Entity
{
    public string? NombresColaborador { get; private set; }
    public string? ApellidosColaborador { get; private set; }
    public string? NombreCompletoColaborador { get; private set; }
    public int TipoDocumentoId { get; private set; }
    public string? NumeroDocumentoIdentidad { get; private set; }
    public DateOnly? FechaNacimiento { get; private set; }
    public int GeneroColaboradorId { get; private set; }
    public int EstadoCivilColaboradorId { get; private set; }
    public string? Direccion { get; private set; }
    public string? CorreoElectronico { get; private set; }
    public string? TelefonoMovil { get; private set; }
    public int CargoId { get; private set; }
    public int DepartamentoId { get; private set; }
    public DateOnly? FechaIngreso { get; private set; }
    public decimal? Salario { get; private set; }
    public DateOnly? FechaCese { get; private set; }
    public string? Comentarios { get; private set; }
    public long ConsultoraId { get; private set; }
    public int EstadoColaborador { get; private set; }
    public Consultora? Consultora { get; private set; }

    public string? UserName { get; private set; }

    private Colaborador() { }

    private Colaborador(
        string? nombresColaborador,
        string? apellidosColaborador,
        string? nombreCompletoColaborador,
        int tipoDocumentoId,
        string? numeroDocumentoIdentidad,
        DateOnly? fechaNacimiento,
        int generoColaboradorId,
        int estadoCivilColaboradorId,
        string? direccion,
        string? correoElectronico,
        string? telefonoMovil,
        int cargoId,
        int departamentoId,
        DateOnly? fechaIngreso,
        decimal? salario,
        DateOnly? fechaCese,
        string? comentarios,
        long consultoraId,
        string? userName,
        int estadoColaborador,
        DateTime fechaCreacion,
        string? usuarioCreacion,
        string? usuarioModificacion = null,
        string? usuarioEliminacion = null
    ) : base(fechaCreacion, usuarioCreacion, usuarioModificacion, usuarioEliminacion)
    {
        NombresColaborador = nombresColaborador;
        ApellidosColaborador = apellidosColaborador;
        NombreCompletoColaborador = nombreCompletoColaborador;
        TipoDocumentoId = tipoDocumentoId;
        NumeroDocumentoIdentidad = numeroDocumentoIdentidad;
        FechaNacimiento = fechaNacimiento;
        GeneroColaboradorId = generoColaboradorId;
        EstadoCivilColaboradorId = estadoCivilColaboradorId;
        Direccion = direccion;
        CorreoElectronico = correoElectronico;
        TelefonoMovil = telefonoMovil;
        CargoId = cargoId;
        DepartamentoId = departamentoId;
        FechaIngreso = fechaIngreso;
        Salario = salario;
        FechaCese = fechaCese;
        Comentarios = comentarios;
        ConsultoraId = consultoraId;
        UserName = userName;
        EstadoColaborador = estadoColaborador;
        FechaCreacion = fechaCreacion;
        UsuarioCreacion = usuarioCreacion;
        UsuarioModificacion = usuarioModificacion;
        UsuarioEliminacion = usuarioEliminacion;
    }

    public static Colaborador Create(
        string? nombresColaborador,
        string? apellidosColaborador,
        int tipoDocumentoId,
        string? numeroDocumentoIdentidad,
        DateOnly? fechaNacimiento,
        int generoColaboradorId,
        int estadoCivilColaboradorId,
        string? direccion,
        string? correoElectronico,
        string? telefonoMovil,
        int cargoId,
        int departamentoId,
        DateOnly? fechaIngreso,
        decimal? salario,
        DateOnly? fechaCese,
        string? comentarios,
        long consultoraId,
        int estadoColaborador,
        DateTime fechaCreacion,
        string? usuarioCreacion
    )
    {
        var nombreCompleto = UserService.CreateNombreCompleto(apellidosColaborador ?? "", nombresColaborador ?? "");
        var userName = UserService.CreateUserName(apellidosColaborador ?? "", nombresColaborador ?? "");

        var entity = new Colaborador(
            nombresColaborador,
            apellidosColaborador,
            nombreCompleto,
            tipoDocumentoId,
            numeroDocumentoIdentidad,
            fechaNacimiento,
            generoColaboradorId,
            estadoCivilColaboradorId,
            direccion,
            correoElectronico,
            telefonoMovil,
            cargoId,
            departamentoId,
            fechaIngreso,
            salario,
            fechaCese,
            comentarios,
            consultoraId,
            userName,
            estadoColaborador,
            fechaCreacion,
            usuarioCreacion
        );
        //entity.RaiseDomainEvent(new ColaboradorCreateDomainEvent(entity.Id));
        return entity;
    }

    public Result Update(
        long Id,
        string? nombresColaborador,
        string? apellidosColaborador,
        int tipoDocumentoId,
        string? numeroDocumentoIdentidad,
        DateOnly? fechaNacimiento,
        int generoColaboradorId,
        int estadoCivilColaboradorId,
        string? direccion,
        string? correoElectronico,
        string? telefonoMovil,
        int cargoId,
        int departamentoId,
        DateOnly? fechaIngreso,
        decimal? salario,
        DateOnly? fechaCese,
        string? comentarios,
        long consultoraId,
        DateTime utcNow,
        string? usuarioModificacion
    )
    {
        var nombreCompleto = UserService.CreateNombreCompleto(apellidosColaborador ?? "", nombresColaborador ?? "");
        var userName = UserService.CreateUserName(apellidosColaborador ?? "", nombresColaborador ?? "");

        NombresColaborador = nombresColaborador;
        ApellidosColaborador = apellidosColaborador;
        NombreCompletoColaborador = nombreCompleto;
        TipoDocumentoId = tipoDocumentoId;
        NumeroDocumentoIdentidad = numeroDocumentoIdentidad;
        FechaNacimiento = fechaNacimiento;
        GeneroColaboradorId = generoColaboradorId;
        EstadoCivilColaboradorId = estadoCivilColaboradorId;
        Direccion = direccion;
        CorreoElectronico = correoElectronico;
        TelefonoMovil = telefonoMovil;
        CargoId = cargoId;
        DepartamentoId = departamentoId;
        FechaIngreso = fechaIngreso;
        Salario = salario;
        FechaCese = fechaCese;
        Comentarios = comentarios;
        UserName = userName;
        ConsultoraId = consultoraId;
        FechaModificacion = utcNow;
        UsuarioModificacion = usuarioModificacion;

        //RaiseDomainEvent(new ColaboradorUpdateDomainEvent(this.Id));

        return Result.Success();
    }

    public Result Delete(DateTime utcNow, string usuarioEliminacion)
    {
        EstadoColaborador = (int)EstadosEnum.Eliminado;
        FechaEliminacion = utcNow;
        UsuarioEliminacion = usuarioEliminacion;
        return Result.Success();
    }
}
