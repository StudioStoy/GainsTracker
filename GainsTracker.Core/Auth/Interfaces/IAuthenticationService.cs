﻿using GainsTracker.Common.Models.Auth.Dto;

namespace GainsTracker.Core.Auth.Interfaces;

public interface IAuthenticationService
{
    Task<string> Register(RegisterRequestDto request);
    Task<string> Login(LoginRequestDto request);
}
