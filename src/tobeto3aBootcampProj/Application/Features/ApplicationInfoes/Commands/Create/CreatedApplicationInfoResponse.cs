using NArchitecture.Core.Application.Responses;

namespace Application.Features.ApplicationInfoes.Commands.Create;

public class CreatedApplicationInfoResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid ApplicantId { get; set; }
    public Guid BootcampId { get; set; }
    public Guid ApplicationStateId { get; set; }
}
