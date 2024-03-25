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

namespace Application.Features.ApplicationInfoes.Commands.Update;

public class UpdateApplicationInfoCommand
    : IRequest<UpdatedApplicationInfoResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid ApplicantId { get; set; }
    public Guid BootcampId { get; set; }
    public Guid ApplicationStateId { get; set; }

    public string[] Roles => [Admin, Write, ApplicationInfoesOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetApplicationInfoes"];

    public class UpdateApplicationInfoCommandHandler
        : IRequestHandler<UpdateApplicationInfoCommand, UpdatedApplicationInfoResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationInfoRepository _applicationInfoRepository;
        private readonly ApplicationInfoBusinessRules _applicationInfoBusinessRules;

        public UpdateApplicationInfoCommandHandler(
            IMapper mapper,
            IApplicationInfoRepository applicationInfoRepository,
            ApplicationInfoBusinessRules applicationInfoBusinessRules
        )
        {
            _mapper = mapper;
            _applicationInfoRepository = applicationInfoRepository;
            _applicationInfoBusinessRules = applicationInfoBusinessRules;
        }

        public async Task<UpdatedApplicationInfoResponse> Handle(
            UpdateApplicationInfoCommand request,
            CancellationToken cancellationToken
        )
        {
            ApplicationInfo? applicationInfo = await _applicationInfoRepository.GetAsync(
                predicate: ai => ai.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _applicationInfoBusinessRules.ApplicationInfoShouldExistWhenSelected(applicationInfo);
            applicationInfo = _mapper.Map(request, applicationInfo);

            await _applicationInfoRepository.UpdateAsync(applicationInfo!);

            UpdatedApplicationInfoResponse response = _mapper.Map<UpdatedApplicationInfoResponse>(applicationInfo);
            return response;
        }
    }
}
