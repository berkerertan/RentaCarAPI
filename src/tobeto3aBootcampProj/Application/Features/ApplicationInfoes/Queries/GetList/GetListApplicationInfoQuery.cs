using Application.Features.ApplicationInfoes.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using static Application.Features.ApplicationInfoes.Constants.ApplicationInfoesOperationClaims;

namespace Application.Features.ApplicationInfoes.Queries.GetList;

public class GetListApplicationInfoQuery
    : IRequest<GetListResponse<GetListApplicationInfoListItemDto>>,
        ISecuredRequest,
        ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListApplicationInfoes({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetApplicationInfoes";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListApplicationInfoQueryHandler
        : IRequestHandler<GetListApplicationInfoQuery, GetListResponse<GetListApplicationInfoListItemDto>>
    {
        private readonly IApplicationInfoRepository _applicationInfoRepository;
        private readonly IMapper _mapper;

        public GetListApplicationInfoQueryHandler(IApplicationInfoRepository applicationInfoRepository, IMapper mapper)
        {
            _applicationInfoRepository = applicationInfoRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListApplicationInfoListItemDto>> Handle(
            GetListApplicationInfoQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<ApplicationInfo> applicationInfoes = await _applicationInfoRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListApplicationInfoListItemDto> response = _mapper.Map<
                GetListResponse<GetListApplicationInfoListItemDto>
            >(applicationInfoes);
            return response;
        }
    }
}
