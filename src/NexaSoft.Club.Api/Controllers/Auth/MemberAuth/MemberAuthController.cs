using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.Auth.MemberAuth.Request;
using NexaSoft.Club.Api.Extensions;
using NexaSoft.Club.Application.Features.Members.Commands.InitMemberRegistration;
using NexaSoft.Club.Application.Features.Members.Commands.MemberPasswordLogin;
using NexaSoft.Club.Application.Features.Members.Commands.RefreshMemberToken;
using NexaSoft.Club.Application.Features.Members.Commands.SetMemberPassword;
using NexaSoft.Club.Application.Features.Members.Commands.VerifyMemberPin;

namespace NexaSoft.Club.Api.Controllers.Auth.MemberAuth
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberAuthController(ISender _sender) : ControllerBase
    {
        [HttpPost("init-registration")]
        public async Task<IActionResult> InitRegistration(MemberRegistrtionRequest request, CancellationToken cancellationToken)
        {
            var command = new InitMemberRegistrationCommand(
                request.Dni,
                request.BirthDate,
                request.DeviceId,
                request.CreatedBy
            );
            var resultado = await _sender.Send(command, cancellationToken);

            return resultado.ToActionResult(this);

        }

        [HttpPost("set-password")]
        public async Task<IActionResult> SetPassword(SetMemberPasswordRequest request, CancellationToken cancellationToken)
        {
            var command = new SetMemberPasswordCommand(
                request.MemberId,
                request.Password,
                request.BiometricToken,
                request.DeviceId
            );
            var resultado = await _sender.Send(command, cancellationToken);

            return resultado.ToActionResult(this);
        }

        [HttpPost("verify-pin")]
        //[AllowAnonymous]
        public async Task<IActionResult> VerifyPin(VerifyMemberPinRequest request, CancellationToken cancellationToken)
        {
            var command = new VerifyMemberPinCommand(
                request.Dni,
                request.BirthDate,
                request.Pin,
                request.DeviceId
            );
            var resultado = await _sender.Send(command, cancellationToken);
            return resultado.ToActionResult(this);

        }


        [HttpPost("password-login")]
        //[AllowAnonymous]
        public async Task<IActionResult> PasswordLogin(MemberPasswordLoginRequest request, CancellationToken cancellationToken)
        {

            var command = new MemberPasswordLoginCommand(
                request.Dni,
                request.Password,
                request.DeviceId,
                request.BiometricToken
            );
            // Este endpoint usaría tu AuthService existente también
            var resultado = await _sender.Send(command, cancellationToken);
            return resultado.ToActionResult(this);

        }

        [HttpPost("refresh-token")]
        //[AllowAnonymous]
        public async Task<IActionResult> RefreshToken(RefreshMemberTokenCommand request, CancellationToken cancellationToken)
        {

            var commad = new RefreshMemberTokenCommand(
                request.RefreshToken,
                request.DeviceId
            );

            var resultado = await _sender.Send(commad, cancellationToken);
            return resultado.ToActionResult(this);

        }
    }
}
