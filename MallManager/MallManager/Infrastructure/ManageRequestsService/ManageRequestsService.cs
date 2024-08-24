using Ardalis.Specification;
using Shared.Core.Entities;
using Shared.Core.Specifications;

namespace MallManager.Infrastructure.ManageRequestsService;

public class ManageRequestsService : IManageRequestsService
{
    private readonly ILogger<ManageRequestsService> _logger;
    
    private readonly IRepositoryBase<LeaseApplication> _leaseApplicationRepository;
    private readonly IRepositoryBase<MarketingCampaign> _marketingCampaignRepository;
    private readonly IRepositoryBase<MassEvent> _massEventRepository;
    
    public IEnumerable<LeaseApplication> LeaseApplications { get; private set; } = Enumerable.Empty<LeaseApplication>();
    public IEnumerable<MarketingCampaign> MarketingCampaigns { get; private set; } = Enumerable.Empty<MarketingCampaign>();
    public IEnumerable<MassEvent> MassEvents { get; private set; } = Enumerable.Empty<MassEvent>();
    
    public ManageRequestsService(ILogger<ManageRequestsService> logger, IRepositoryBase<LeaseApplication> leaseApplicationRepository, IRepositoryBase<MarketingCampaign> marketingCampaignRepository,
        IRepositoryBase<MassEvent> massEventRepository)
    {
        _logger = logger;
        
        _leaseApplicationRepository = leaseApplicationRepository;
        _marketingCampaignRepository = marketingCampaignRepository;
        _massEventRepository = massEventRepository;
    }
    
    public async Task LoadAspNetUserRequestsAsync(int userId)
    {
        var userIdString = userId + "";

        var leaseApplicationsOfUserId = new LeaseApplicationsByUserId(userIdString);
        LeaseApplications = await _leaseApplicationRepository.ListAsync(leaseApplicationsOfUserId);

        var marketingCampaignsOfUser = new MarketingCampaignsByUserId(userIdString);
        MarketingCampaigns = await _marketingCampaignRepository.ListAsync(marketingCampaignsOfUser);
        
        var massEventsOfUser = new MassEventsByUserId(userIdString);
        MassEvents = await _massEventRepository.ListAsync(massEventsOfUser);
        
        _logger.LogInformation($"Successfully loaded requests of user with Id {userIdString}");
        
    }
}