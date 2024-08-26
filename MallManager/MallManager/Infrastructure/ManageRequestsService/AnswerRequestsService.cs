using Ardalis.Specification;
using Shared.Core.Entities;

namespace MallManager.Infrastructure.ManageRequestsService;

public class AnswerRequestsService : IAnswerRequestsService
{
    private readonly ILogger<AnswerRequestsService> _logger;
    
    private readonly IRepositoryBase<LeaseApplication> _leaseApplicationRepository;
    private readonly IRepositoryBase<MarketingCampaign> _marketingCampaignRepository;
    private readonly IRepositoryBase<MassEvent> _massEventRepository;
    
    public IEnumerable<LeaseApplication> LeaseApplications { get; private set; } = Enumerable.Empty<LeaseApplication>();
    public IEnumerable<MarketingCampaign> MarketingCampaigns { get; private set; } = Enumerable.Empty<MarketingCampaign>();
    public IEnumerable<MassEvent> MassEvents { get; private set; } = Enumerable.Empty<MassEvent>();
    
    public AnswerRequestsService(ILogger<AnswerRequestsService> logger, IRepositoryBase<LeaseApplication> leaseApplicationRepository, IRepositoryBase<MarketingCampaign> marketingCampaignRepository,
        IRepositoryBase<MassEvent> massEventRepository)
    {
        _logger = logger;
        
        _leaseApplicationRepository = leaseApplicationRepository;
        _marketingCampaignRepository = marketingCampaignRepository;
        _massEventRepository = massEventRepository;
    }
    
    
    public async Task LoadRequestsAsync()
    {
        
        LeaseApplications = await _leaseApplicationRepository.ListAsync();
        
        MarketingCampaigns = await _marketingCampaignRepository.ListAsync();
        
        MassEvents = await _massEventRepository.ListAsync();
        
        _logger.LogInformation($"Successfully loaded requests ");
        
    }
    
    public async Task AcceptLeaseApplication(LeaseApplication leaseApplication)
    {
        await _leaseApplicationRepository.DeleteAsync(leaseApplication);
        _logger.LogInformation("The lease application has been deleted successfully");
    }
    public async Task DenyLeaseApplication(LeaseApplication leaseApplication)
    {
        await _leaseApplicationRepository.DeleteAsync(leaseApplication);
        _logger.LogInformation("The lease application has been deleted successfully");
    }
    
    public async Task AcceptMarketingCampaign(MarketingCampaign marketingCampaign)
    {
        await _marketingCampaignRepository.DeleteAsync(marketingCampaign);
        _logger.LogInformation("The marketing campaign has been deleted successfully");
    }
    public async Task DenyMarketingCampaign(MarketingCampaign marketingCampaign)
    {
        await _marketingCampaignRepository.DeleteAsync(marketingCampaign);
        _logger.LogInformation("The marketing campaign has been deleted successfully");
    }
    
    public async Task AcceptMassEvent(MassEvent massEvent)
    {
        await _massEventRepository.DeleteAsync(massEvent);
        _logger.LogInformation("The mass event has been deleted successfully");
    }
    public async Task DenyMassEvent(MassEvent massEvent)
    {
        await _massEventRepository.DeleteAsync(massEvent);
        _logger.LogInformation("The mass event has been deleted successfully");
    }
}