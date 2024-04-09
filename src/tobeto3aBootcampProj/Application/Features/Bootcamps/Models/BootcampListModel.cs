using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Bootcamps.Queries.GetList;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Bootcamps.Models;

public class BootcampListModel : BasePageableModel
{
    public List<GetListBootcampListItemDto> Items { get; set; }
}
