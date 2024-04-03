using Application.Features.BootcampImages.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Application.Services.ImageService;
using Microsoft.AspNetCore.Http;
using Application.Features.BootcampImages.Commands.Create;

namespace Application.Services.BootcampImages;

public class BootcampImageManager : IBootcampImageService
{
    private readonly IBootcampImageRepository _bootcampImageRepository;
    private readonly BootcampImageBusinessRules _bootcampImageBusinessRules;
    private readonly ImageServiceBase _imageService;

    public BootcampImageManager(IBootcampImageRepository bootcampImageRepository, BootcampImageBusinessRules bootcampImageBusinessRules, ImageServiceBase imageService)
    {
        _bootcampImageRepository = bootcampImageRepository;
        _bootcampImageBusinessRules = bootcampImageBusinessRules;
        _imageService = imageService;
    }

    public async Task<BootcampImage?> GetAsync(
        Expression<Func<BootcampImage, bool>> predicate,
        Func<IQueryable<BootcampImage>, IIncludableQueryable<BootcampImage, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        BootcampImage? bootcampImage = await _bootcampImageRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return bootcampImage;
    }

    public async Task<IPaginate<BootcampImage>?> GetListAsync(
        Expression<Func<BootcampImage, bool>>? predicate = null,
        Func<IQueryable<BootcampImage>, IOrderedQueryable<BootcampImage>>? orderBy = null,
        Func<IQueryable<BootcampImage>, IIncludableQueryable<BootcampImage, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<BootcampImage> bootcampImageList = await _bootcampImageRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return bootcampImageList;
    }

    public async Task<BootcampImage> AddAsync(IFormFile file,CreateBootcampImageCommand command)
    {
        //BootcampImage addedBootcampImage = await _bootcampImageRepository.AddAsync(bootcampImage);
        //return addedBootcampImage;
        BootcampImage bootcampImage = new BootcampImage()
        {
            BootcampId= command.BootcampId,
            ImagePath= command.ImagePath
        };
        bootcampImage.ImagePath = await _imageService.UploadAsync(file);
        return await _bootcampImageRepository.AddAsync(bootcampImage);

    }

    public async Task<BootcampImage> UpdateAsync(BootcampImage bootcampImage)
    {
        BootcampImage updatedBootcampImage = await _bootcampImageRepository.UpdateAsync(bootcampImage);

        return updatedBootcampImage;
    }

    public async Task<BootcampImage> DeleteAsync(BootcampImage bootcampImage, bool permanent = false)
    {
        BootcampImage deletedBootcampImage = await _bootcampImageRepository.DeleteAsync(bootcampImage);

        return deletedBootcampImage;
    }
}
