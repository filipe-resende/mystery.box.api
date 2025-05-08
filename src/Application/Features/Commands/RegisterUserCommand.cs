﻿namespace Application.Features.Commands;
public class RegisterUserCommand : IRequest<Result>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
    public string CPF { get; set; }
}
