using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Bootcamps.Models;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Bootcamps.Queries.GetList;

internal class GetListBootcampByInstructorIdQueryHandler : IRequestHandler<GetListBootcampByInstructorIdQuery, BootcampListModel>
{
    private readonly IBootcampRepository _bootcampRepository;
    private readonly IMapper _mapper;

    public GetListBootcampByInstructorIdQueryHandler(IBootcampRepository bootcampRepository, IMapper mapper)
    {
        _bootcampRepository = bootcampRepository;
        _mapper = mapper;
    }

    public async Task<BootcampListModel> Handle(GetListBootcampByInstructorIdQuery request, CancellationToken cancellationToken)
    {
        IPaginate<Bootcamp> bootcamp = await _bootcampRepository.GetListAsync(
            index: request.PageRequest.PageIndex,
            size: request.PageRequest.PageSize,
            include: x =>
                x.Include(x => x.Instructor)
                    .Include(x => x.Instructor)
                    .Include(x => x.BootcampImage)
                    .Include(x => x.BootcampState)
        );
        BootcampListModel carListModel = _mapper.Map<BootcampListModel>(bootcamp);
        return carListModel;
    }
}
