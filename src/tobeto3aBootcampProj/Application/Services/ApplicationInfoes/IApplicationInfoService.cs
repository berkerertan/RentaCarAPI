using System.Linq.Expressions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Services.ApplicationInfoes;

public interface IApplicationInfoService
{
    Task<ApplicationInfo?> GetAsync(
        Expression<Func<ApplicationInfo, bool>> predicate,
        Func<IQueryable<ApplicationInfo>, IIncludableQueryable<ApplicationInfo, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ApplicationInfo>?> GetListAsync(
        Expression<Func<ApplicationInfo, bool>>? predicate = null,
        Func<IQueryable<ApplicationInfo>, IOrderedQueryable<ApplicationInfo>>? orderBy = null,
        Func<IQueryable<ApplicationInfo>, IIncludableQueryable<ApplicationInfo, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ApplicationInfo> AddAsync(ApplicationInfo applicationInfo);
    Task<ApplicationInfo> UpdateAsync(ApplicationInfo applicationInfo);
    Task<ApplicationInfo> DeleteAsync(ApplicationInfo applicationInfo, bool permanent = false);
}
