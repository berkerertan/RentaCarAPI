using Application.Features.ApplicationInfoes.Constants;
using Application.Features.ApplicationInfoes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using static Application.Features.ApplicationInfoes.Constants.ApplicationInfoesOperationClaims;

namespace Application.Features.ApplicationInfoes.Commands.Create;

public class CreateApplicationInfoCommand
    : IRequest<CreatedApplicationInfoResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public Guid ApplicantId { get; set; }
    public Guid BootcampId { get; set; }
    public Guid ApplicationStateId { get; set; }

    public string[] Roles => [Admin, Write, ApplicationInfoesOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetApplicationInfoes"];

    public class CreateApplicationInfoCommandHandler
        : IRequestHandler<CreateApplicationInfoCommand, CreatedApplicationInfoResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationInfoRepository _applicationInfoRepository;
        private readonly ApplicationInfoBusinessRules _applicationInfoBusinessRules;

        public CreateApplicationInfoCommandHandler(
            IMapper mapper,
            IApplicationInfoRepository applicationInfoRepository,
            ApplicationInfoBusinessRules applicationInfoBusinessRules
        )
        {
            _mapper = mapper;
            _applicationInfoRepository = applicationInfoRepository;
            _applicationInfoBusinessRules = applicationInfoBusinessRules;
        }

        public async Task<CreatedApplicationInfoResponse> Handle(
            CreateApplicationInfoCommand request,
            CancellationToken cancellationToken
        )
        {
            ApplicationInfo applicationInfo = _mapper.Map<ApplicationInfo>(request);

            await _applicationInfoRepository.AddAsync(applicationInfo);

            CreatedApplicationInfoResponse response = _mapper.Map<CreatedApplicationInfoResponse>(applicationInfo);
            return response;
        }
    }
}
