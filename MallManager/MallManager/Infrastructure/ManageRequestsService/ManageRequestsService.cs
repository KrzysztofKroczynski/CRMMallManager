using Ardalis.Specification;
using Microsoft.VisualBasic;
using Shared.Core.Entities;
using Shared.Core.Specifications;

namespace MallManager.Infrastructure.ManageRequestsService;

public class ManageRequestsService : IManageRequestsService
{
    private readonly ILogger<ManageRequestsService> _logger;
    
    private readonly IRepositoryBase<LeaseApplication> _leaseApplicationRepository;
    private readonly IRepositoryBase<MarketingCampaign> _marketingCampaignRepository;
    private readonly IRepositoryBase<MassEvent> _massEventRepository;
    
    public ICollection<LeaseApplication> LeaseApplications { get; private set; } = new List<LeaseApplication>();
    public ICollection<MarketingCampaign> MarketingCampaigns { get; private set; } = new List<MarketingCampaign>();
    public ICollection<MassEvent> MassEvents { get; private set; } = new List<MassEvent>();
    
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

    public async Task DeleteLeaseApplication(LeaseApplication leaseApplication)
    {
        leaseApplication.RetailUnitPurposes.Clear();
        leaseApplication.SurfaceClassDicts.Clear();
        LeaseApplications.Remove(leaseApplication);
        
        await _leaseApplicationRepository.DeleteAsync(leaseApplication);
        await _leaseApplicationRepository.SaveChangesAsync();
        _logger.LogInformation("The lease application has been deleted successfully");
    }
    
    public async Task DeleteMarketingCampaign(MarketingCampaign marketingCampaign)
    {
        marketingCampaign.MarketingMaterials.Clear();
        MarketingCampaigns.Remove(marketingCampaign);
        
        await _marketingCampaignRepository.DeleteAsync(marketingCampaign);
        _logger.LogInformation("The marketing campaign has been deleted successfully");
    }
    
    public async Task DeleteMassEvent(MassEvent massEvent)
    {
        MassEvents.Remove(massEvent);
        
        await _massEventRepository.DeleteAsync(massEvent);
        _logger.LogInformation("The mass event has been deleted successfully");
    }
}