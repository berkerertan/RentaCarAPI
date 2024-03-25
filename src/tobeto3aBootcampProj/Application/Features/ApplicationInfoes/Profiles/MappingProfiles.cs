using Application.Features.ApplicationInfoes.Commands.Create;
using Application.Features.ApplicationInfoes.Commands.Delete;
using Application.Features.ApplicationInfoes.Commands.Update;
using Application.Features.ApplicationInfoes.Queries.GetById;
using Application.Features.ApplicationInfoes.Queries.GetList;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.ApplicationInfoes.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ApplicationInfo, CreateApplicationInfoCommand>().ReverseMap();
        CreateMap<ApplicationInfo, CreatedApplicationInfoResponse>().ReverseMap();
        CreateMap<ApplicationInfo, UpdateApplicationInfoCommand>().ReverseMap();
        CreateMap<ApplicationInfo, UpdatedApplicationInfoResponse>().ReverseMap();
        CreateMap<ApplicationInfo, DeleteApplicationInfoCommand>().ReverseMap();
        CreateMap<ApplicationInfo, DeletedApplicationInfoResponse>().ReverseMap();
        CreateMap<ApplicationInfo, GetByIdApplicationInfoResponse>().ReverseMap();
        CreateMap<ApplicationInfo, GetListApplicationInfoListItemDto>().ReverseMap();
        CreateMap<IPaginate<ApplicationInfo>, GetListResponse<GetListApplicationInfoListItemDto>>().ReverseMap();
    }
}
