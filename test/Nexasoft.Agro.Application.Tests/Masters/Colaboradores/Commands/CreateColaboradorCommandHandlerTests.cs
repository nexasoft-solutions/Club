using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using Moq;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Application.Masters.Consultoras.Colaboradores.Commands.CreateColaborador;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Masters.Consultoras.Colaboradores;

namespace Nexasoft.Agro.Application.Tests.Masters.Colaboradores.Commands;

public class CreateColaboradorCommandHandlerTests
{
    private readonly Mock<IGenericRepository<Colaborador>> _repositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IDateTimeProvider> _dateTimeProviderMock;
    private readonly Mock<ILogger<CreateColaboradorCommandHandler>> _loggerMock;

    private readonly CreateColaboradorCommandHandler _handler;

    public CreateColaboradorCommandHandlerTests()
    {
        _repositoryMock = new Mock<IGenericRepository<Colaborador>>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _dateTimeProviderMock = new Mock<IDateTimeProvider>();
        _loggerMock = new Mock<ILogger<CreateColaboradorCommandHandler>>();

        _handler = new CreateColaboradorCommandHandler(
            _repositoryMock.Object,
            _unitOfWorkMock.Object,
            _dateTimeProviderMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldCreateColaboradorAndReturnSuccess()
    {
        // Arrange
        var now = DateTime.UtcNow;
        var command = new CreateColaboradorCommand(
            "Carlos",                  // NombresColaborador
            "Ramírez Quispe",                 // ApellidosColaborador
            1,                         // TipoDocumentoId
            "12345678",                // NumeroDocumentoIdentidad
            new DateOnly(1990, 5, 15), // FechaNacimiento
            1,                         // GeneroColaboradorId
            1,                         // EstadoCivilColaboradorId
            "Av. Siempre Viva 123",    // Direccion
            "carlos@email.com",        // CorreoElectronico
            "987654321",               // TelefonoMovil
            1,                         // CargoId
            2,                         // DepartamentoId
            new DateOnly(2020, 5, 15), // FechaIngreso
            2500m,                     // Salario
            null,                      // FechaCese (puedes usar null o DateOnly)
            "Sin comentarios",         // Comentarios
            1,                          // ConsultoraId
            "aroblesa"                 // Usuario
        );

        _dateTimeProviderMock.Setup(p => p.CurrentTime).Returns(now);

        // Simular que no hay duplicados
        _repositoryMock.Setup(r =>
            r.ExistsAsync(It.IsAny<Expression<Func<Colaborador, bool>>>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(false);
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Colaborador>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldRollbackTransaction_OnException()
    {
        // Arrange
        var command = new CreateColaboradorCommand(
             "Carlos",                  // NombresColaborador
             "Ramírez Juarez",                 // ApellidosColaborador
             1,                         // TipoDocumentoId
             "12345678",                // NumeroDocumentoIdentidad
             new DateOnly(1990, 5, 15), // FechaNacimiento
             1,                         // GeneroColaboradorId
             1,                         // EstadoCivilColaboradorId
             "Av. Siempre Viva 123",    // Direccion
             "carlos@email.com",        // CorreoElectronico
             "987654321",               // TelefonoMovil
             1,                         // CargoId
             2,                         // DepartamentoId
             new DateOnly(2020, 5, 15), // FechaIngreso
             2500m,                     // Salario
             null,                      // FechaCese
             "Sin comentarios",         // Comentarios
             1,                         // ConsultoraId
             "aroblesa"                 // Usuario
         );

        _dateTimeProviderMock.Setup(p => p.CurrentTime).Returns(DateTime.UtcNow);

        // Simular que no hay duplicados
        _repositoryMock.Setup(r =>
            r.ExistsAsync(It.IsAny<Expression<Func<Colaborador, bool>>>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(false);

        // Simular error al guardar
        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Colaborador>(), It.IsAny<CancellationToken>()))
                       .ThrowsAsync(new Exception("DB error"));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        _unitOfWorkMock.Verify(u => u.RollbackAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
