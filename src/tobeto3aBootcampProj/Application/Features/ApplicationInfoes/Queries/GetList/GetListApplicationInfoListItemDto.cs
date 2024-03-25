using NArchitecture.Core.Application.Dtos;

namespace Application.Features.ApplicationInfoes.Queries.GetList;

public class GetListApplicationInfoListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid ApplicantId { get; set; }
    public Guid BootcampId { get; set; }
    public Guid ApplicationStateId { get; set; }
}
