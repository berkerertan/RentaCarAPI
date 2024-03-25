using Application.Features.ApplicationInfoes.Constants;
using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;

namespace Application.Features.ApplicationInfoes.Rules;

public class ApplicationInfoBusinessRules : BaseBusinessRules
{
    private readonly IApplicationInfoRepository _applicationInfoRepository;
    private readonly ILocalizationService _localizationService;

    public ApplicationInfoBusinessRules(
        IApplicationInfoRepository applicationInfoRepository,
        ILocalizationService localizationService
    )
    {
        _applicationInfoRepository = applicationInfoRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, ApplicationInfoesBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task ApplicationInfoShouldExistWhenSelected(ApplicationInfo? applicationInfo)
    {
        if (applicationInfo == null)
            await throwBusinessException(ApplicationInfoesBusinessMessages.ApplicationInfoNotExists);
    }

    public async Task ApplicationInfoIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        ApplicationInfo? applicationInfo = await _applicationInfoRepository.GetAsync(
            predicate: ai => ai.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ApplicationInfoShouldExistWhenSelected(applicationInfo);
    }
}
