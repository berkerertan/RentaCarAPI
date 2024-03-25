using NArchitecture.Core.Application.Responses;

namespace Application.Features.ApplicationInfoes.Commands.Delete;

public class DeletedApplicationInfoResponse : IResponse
{
    public Guid Id { get; set; }
}
