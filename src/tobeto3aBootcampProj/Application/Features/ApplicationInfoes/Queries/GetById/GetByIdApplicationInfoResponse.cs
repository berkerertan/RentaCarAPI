using NArchitecture.Core.Application.Responses;

namespace Application.Features.ApplicationInfoes.Queries.GetById;

public class GetByIdApplicationInfoResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid ApplicantId { get; set; }
    public Guid BootcampId { get; set; }
    public Guid ApplicationStateId { get; set; }
}
