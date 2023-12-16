using Application.MediatR.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        [AllowAnonymous]
        [Route("signIn")]
        public async Task<IActionResult> Post([FromBody] GetAuthenticationTokenCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("user_not_found"))
                {
                    return BadRequest(new ProblemDetails
                    {
                        Status = 401,
                        Title = "User Already Exist",
                    });
                }

                return Problem(detail: ex.Message);
            }
        }

        [HttpPost]
        [Route("validate")]
        [Authorize(Roles = "registered")]
        public async Task<IActionResult> ValidateUser()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.Sid)?.Value;
                
                if (userId == null) 
                {
                     return BadRequest(new ProblemDetails
                    {
                        Status = 401,
                        Title = "User Not Found",
                    });
                }

                var command = new GetValidateTokenCommand(userId);
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("user_not_found"))
                {
                    return BadRequest(new ProblemDetails
                    {
                        Status = 401,
                        Title = "User Already Exist",
                    });
                }

                return Problem(detail: ex.Message);
            }
        }

        [HttpPost]
        [Route("forgotten")]
        public async Task<IActionResult> SendEmailResetUserPassword([FromBody] GetForgottenUserCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("user_not_found"))
                {
                    return BadRequest(new ProblemDetails
                    {
                        Status = 401,
                        Title = "User Already Exist",
                    });
                }

                return Problem(detail: ex.Message);
            }
        }

        [HttpGet]
        [Route("active/{token}")]
        public async Task<IActionResult> ActiverUser(string token)
        {
            try
            {
                var command = new ActiverUserCommand();
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("user_not_found"))
                {
                    return BadRequest(new ProblemDetails
                    {
                        Status = 401,
                        Title = "User Already Exist",
                    });
                }

                return Problem(detail: ex.Message);
            }
        }

        [HttpPatch]
        [Route("reset")]
        [Authorize(Roles = "registered")]
        public async Task<IActionResult> ResetPassword([FromBody] PasswordCommand password)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.Sid)?.Value;
                var command = new ResetPasswordCommand(password.Password, userId);
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("user_not_found"))
                {
                    return BadRequest(new ProblemDetails
                    {
                        Status = 401,
                        Title = "User Already Exist",
                    });
                }

                return Problem(detail: ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var command = new GetUserCommand(id);
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostUser(RegisterUserCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                return Ok(new { status = 200, erro = "", mensagem = "Usuário criado" });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("user_already_exist"))
                {
                    return Conflict(new ProblemDetails
                    {
                        Status = 409,
                        Title = "User Already Exist",
                    });
                }

                return Problem(detail: ex.Message);
            }
        }
    }
}