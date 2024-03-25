using Application.Features.ApplicationInfoes.Constants;
using Application.Features.ApplicationInfoes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using static Application.Features.ApplicationInfoes.Constants.ApplicationInfoesOperationClaims;

namespace Application.Features.ApplicationInfoes.Queries.GetById;

public class GetByIdApplicationInfoQuery : IRequest<GetByIdApplicationInfoResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdApplicationInfoQueryHandler : IRequestHandler<GetByIdApplicationInfoQuery, GetByIdApplicationInfoResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationInfoRepository _applicationInfoRepository;
        private readonly ApplicationInfoBusinessRules _applicationInfoBusinessRules;

        public GetByIdApplicationInfoQueryHandler(
            IMapper mapper,
            IApplicationInfoRepository applicationInfoRepository,
            ApplicationInfoBusinessRules applicationInfoBusinessRules
        )
        {
            _mapper = mapper;
            _applicationInfoRepository = applicationInfoRepository;
            _applicationInfoBusinessRules = applicationInfoBusinessRules;
        }

        public async Task<GetByIdApplicationInfoResponse> Handle(
            GetByIdApplicationInfoQuery request,
            CancellationToken cancellationToken
        )
        {
            ApplicationInfo? applicationInfo = await _applicationInfoRepository.GetAsync(
                predicate: ai => ai.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _applicationInfoBusinessRules.ApplicationInfoShouldExistWhenSelected(applicationInfo);

            GetByIdApplicationInfoResponse response = _mapper.Map<GetByIdApplicationInfoResponse>(applicationInfo);
            return response;
        }
    }
}
