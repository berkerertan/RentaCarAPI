using NArchitecture.Core.Application.Responses;

namespace Application.Features.UserImages.Queries.GetById;

public class GetByIdUserImageResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string ImagePath { get; set; }
}
