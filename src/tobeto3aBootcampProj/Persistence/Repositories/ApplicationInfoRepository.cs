using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ApplicationInfoRepository : EfRepositoryBase<ApplicationInfo, Guid, BaseDbContext>, IApplicationInfoRepository
{
    public ApplicationInfoRepository(BaseDbContext context)
        : base(context) { }
}
