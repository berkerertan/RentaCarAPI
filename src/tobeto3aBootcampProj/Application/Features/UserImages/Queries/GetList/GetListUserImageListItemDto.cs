using NArchitecture.Core.Application.Dtos;

namespace Application.Features.UserImages.Queries.GetList;

public class GetListUserImageListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string ImagePath { get; set; }
}
