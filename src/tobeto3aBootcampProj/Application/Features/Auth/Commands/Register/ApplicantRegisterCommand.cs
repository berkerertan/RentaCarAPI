﻿using Application.Features.Auth.Rules;
using Application.Services.Applicants;
using Application.Services.AuthService;
using Application.Services.Employees;
using Application.Services.Instructors;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Dtos;
using NArchitecture.Core.Security.Hashing;
using NArchitecture.Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Register;
public class ApplicantRegisterCommand : IRequest<RegisteredResponse>
{
    public ApplicantForRegisterDto ApplicantForRegisterDto { get; set; }
    public string IpAddress { get; set; }

    public ApplicantRegisterCommand()
    {
        ApplicantForRegisterDto = null!;
        IpAddress = string.Empty;
    }

    public ApplicantRegisterCommand( ApplicantForRegisterDto applicantForRegisterDto,string ipAddress)
    {
        ApplicantForRegisterDto = applicantForRegisterDto;
        IpAddress = ipAddress;
    }

    public class RegisterCommandHandler : IRequestHandler<ApplicantRegisterCommand, RegisteredResponse>
    {
        private readonly IApplicantRepository _applicantRepository;
        private readonly IAuthService _authService;
        private readonly AuthBusinessRules _authBusinessRules;

        public RegisterCommandHandler(
            IApplicantRepository applicantRepository,
            IAuthService authService,
            AuthBusinessRules authBusinessRules
        )
        {
            _applicantRepository = applicantRepository;
            _authService = authService;
            _authBusinessRules = authBusinessRules;
        }

        public async Task<RegisteredResponse> Handle(ApplicantRegisterCommand request, CancellationToken cancellationToken)
        {
            await _authBusinessRules.UserEmailShouldBeNotExists(request.ApplicantForRegisterDto.Email);

            HashingHelper.CreatePasswordHash(
                request.ApplicantForRegisterDto.Password,
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );
            Applicant newApplicant =
                new()
                {
                    Email = request.ApplicantForRegisterDto.Email,
                    About = request.ApplicantForRegisterDto.About,
                    FirstName = request.ApplicantForRegisterDto.FirstName,
                    LastName = request.ApplicantForRegisterDto.LastName,
                    DateOfBirth = request.ApplicantForRegisterDto.DateOfBirth,
                    NationalIdentity = request.ApplicantForRegisterDto.NationalIdentity,
                    UserName = request.ApplicantForRegisterDto.UserName,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                };
            Applicant createdApplicant = await _applicantRepository.AddAsync(newApplicant);

            AccessToken createdAccessToken = await _authService.CreateAccessToken(createdApplicant);

            Domain.Entities.RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(
                createdApplicant,
                request.IpAddress
            );
            Domain.Entities.RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

            RegisteredResponse registeredResponse = new() { AccessToken = createdAccessToken, RefreshToken = addedRefreshToken };
            return registeredResponse;
        }

    }
}
