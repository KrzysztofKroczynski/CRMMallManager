using Microsoft.AspNetCore.Components;

namespace MallManager.Components.Forms.EventOrganizationForm;

public partial class EventOrganizationForm : ComponentBase
{
    private string? _description;
    private DateTime? _endDate;
    private string? _eventName;
    private string? _eventType;
    private int? _maxMembersCount;
    private int? _minMembersCount;
    private DateTime? _startDate;
}