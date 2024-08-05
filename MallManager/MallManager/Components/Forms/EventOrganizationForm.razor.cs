using Microsoft.AspNetCore.Components;

namespace MallManager.Components.Forms;

public partial class EventOrganizationForm : ComponentBase
{
    private string? _eventName;
    private DateTime? _startDate;
    private DateTime? _endDate;
    private string? _eventType;
    private string? _description;
    private int? _minMembersCount;
    private int? _maxMembersCount;
}