﻿namespace Application.DTOs.AuthDTOs;

public class RegisterUserDTO
{
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
