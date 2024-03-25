using System.Linq.Expressions;
using Application.Features.ApplicationInfoes.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Services.ApplicationInfoes;

public class ApplicationInfoManager : IApplicationInfoService
{
    private readonly IApplicationInfoRepository _applicationInfoRepository;
    private readonly ApplicationInfoBusinessRules _applicationInfoBusinessRules;

    public ApplicationInfoManager(
        IApplicationInfoRepository applicationInfoRepository,
        ApplicationInfoBusinessRules applicationInfoBusinessRules
    )
    {
        _applicationInfoRepository = applicationInfoRepository;
        _applicationInfoBusinessRules = applicationInfoBusinessRules;
    }

    public async Task<ApplicationInfo?> GetAsync(
        Expression<Func<ApplicationInfo, bool>> predicate,
        Func<IQueryable<ApplicationInfo>, IIncludableQueryable<ApplicationInfo, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ApplicationInfo? applicationInfo = await _applicationInfoRepository.GetAsync(
            predicate,
            include,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return applicationInfo;
    }

    public async Task<IPaginate<ApplicationInfo>?> GetListAsync(
        Expression<Func<ApplicationInfo, bool>>? predicate = null,
        Func<IQueryable<ApplicationInfo>, IOrderedQueryable<ApplicationInfo>>? orderBy = null,
        Func<IQueryable<ApplicationInfo>, IIncludableQueryable<ApplicationInfo, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ApplicationInfo> applicationInfoList = await _applicationInfoRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return applicationInfoList;
    }

    public async Task<ApplicationInfo> AddAsync(ApplicationInfo applicationInfo)
    {
        ApplicationInfo addedApplicationInfo = await _applicationInfoRepository.AddAsync(applicationInfo);

        return addedApplicationInfo;
    }

    public async Task<ApplicationInfo> UpdateAsync(ApplicationInfo applicationInfo)
    {
        ApplicationInfo updatedApplicationInfo = await _applicationInfoRepository.UpdateAsync(applicationInfo);

        return updatedApplicationInfo;
    }

    public async Task<ApplicationInfo> DeleteAsync(ApplicationInfo applicationInfo, bool permanent = false)
    {
        ApplicationInfo deletedApplicationInfo = await _applicationInfoRepository.DeleteAsync(applicationInfo);

        return deletedApplicationInfo;
    }
}
