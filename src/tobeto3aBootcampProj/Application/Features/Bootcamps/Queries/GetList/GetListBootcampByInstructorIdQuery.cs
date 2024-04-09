using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Bootcamps.Models;
using MediatR;
using NArchitecture.Core.Application.Requests;

namespace Application.Features.Bootcamps.Queries.GetList;

public class GetListBootcampByInstructorIdQuery : IRequest<BootcampListModel>
{
    public PageRequest PageRequest { get; set; }
    public Guid InstructorId { get; set; }
}
