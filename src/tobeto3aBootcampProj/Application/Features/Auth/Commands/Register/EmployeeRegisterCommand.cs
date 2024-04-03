﻿using Application.Features.Auth.Rules;
using Application.Services.Applicants;
using Application.Services.AuthService;
using Application.Services.Employees;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Security.Hashing;
using NArchitecture.Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Register;
public class EmployeeRegisterCommand : IRequest<RegisteredResponse>
{
    public EmployeeForRegisterDto EmployeeForRegisterDto { get; set; }
    public string IpAddress { get; set; }

    public EmployeeRegisterCommand()
    {
        EmployeeForRegisterDto = null!;
        IpAddress = string.Empty;
    }

    public EmployeeRegisterCommand(EmployeeForRegisterDto employeeForRegisterDto, string ipAddress)
    {
        EmployeeForRegisterDto = employeeForRegisterDto;
        IpAddress = ipAddress;
    }

    public class RegisterCommandHandler : IRequestHandler<EmployeeRegisterCommand, RegisteredResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAuthService _authService;
        private readonly AuthBusinessRules _authBusinessRules;

        public RegisterCommandHandler(
            IEmployeeRepository employeeRepository,
            IAuthService authService,
            AuthBusinessRules authBusinessRules
        )
        {
            _employeeRepository = employeeRepository;
            _authService = authService;
            _authBusinessRules = authBusinessRules;
        }

        public async Task<RegisteredResponse> Handle(EmployeeRegisterCommand request, CancellationToken cancellationToken)
        {
            await _authBusinessRules.UserEmailShouldBeNotExists(request.EmployeeForRegisterDto.Email);

            HashingHelper.CreatePasswordHash(
                request.EmployeeForRegisterDto.Password,
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );
            Employee newEmployee =
                new()
                {
                    Email = request.EmployeeForRegisterDto.Email,
                    Position = request.EmployeeForRegisterDto.Position,
                    FirstName = request.EmployeeForRegisterDto.FirstName,
                    LastName = request.EmployeeForRegisterDto.LastName,
                    DateOfBirth = request.EmployeeForRegisterDto.DateOfBirth,
                    NationalIdentity = request.EmployeeForRegisterDto.NationalIdentity,
                    UserName = request.EmployeeForRegisterDto.UserName,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                };
            Employee createdEmployee = await _employeeRepository.AddAsync(newEmployee);

            AccessToken createdAccessToken = await _authService.CreateAccessToken(createdEmployee);

            Domain.Entities.RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(
                createdEmployee,
                request.IpAddress
            );
            Domain.Entities.RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

            RegisteredResponse registeredResponse = new() { AccessToken = createdAccessToken, RefreshToken = addedRefreshToken };
            return registeredResponse;
        }
    }
}
