using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class UserImageRepository : EfRepositoryBase<UserImage, Guid, BaseDbContext>, IUserImageRepository
{
    public UserImageRepository(BaseDbContext context)
        : base(context) { }
}
