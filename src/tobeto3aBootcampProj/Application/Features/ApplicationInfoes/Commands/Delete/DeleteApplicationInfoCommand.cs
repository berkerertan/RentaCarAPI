using Application.Features.ApplicationInfoes.Constants;
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

namespace Application.Features.ApplicationInfoes.Commands.Delete;

public class DeleteApplicationInfoCommand
    : IRequest<DeletedApplicationInfoResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, ApplicationInfoesOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetApplicationInfoes"];

    public class DeleteApplicationInfoCommandHandler
        : IRequestHandler<DeleteApplicationInfoCommand, DeletedApplicationInfoResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationInfoRepository _applicationInfoRepository;
        private readonly ApplicationInfoBusinessRules _applicationInfoBusinessRules;

        public DeleteApplicationInfoCommandHandler(
            IMapper mapper,
            IApplicationInfoRepository applicationInfoRepository,
            ApplicationInfoBusinessRules applicationInfoBusinessRules
        )
        {
            _mapper = mapper;
            _applicationInfoRepository = applicationInfoRepository;
            _applicationInfoBusinessRules = applicationInfoBusinessRules;
        }

        public async Task<DeletedApplicationInfoResponse> Handle(
            DeleteApplicationInfoCommand request,
            CancellationToken cancellationToken
        )
        {
            ApplicationInfo? applicationInfo = await _applicationInfoRepository.GetAsync(
                predicate: ai => ai.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _applicationInfoBusinessRules.ApplicationInfoShouldExistWhenSelected(applicationInfo);

            await _applicationInfoRepository.DeleteAsync(applicationInfo!);

            DeletedApplicationInfoResponse response = _mapper.Map<DeletedApplicationInfoResponse>(applicationInfo);
            return response;
        }
    }
}
