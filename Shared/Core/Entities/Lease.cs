namespace Shared.Core.Entities;

public sealed partial class Lease
{
    public int Id { get; set; }

    public DateOnly StartDate { get; set; }

    public DateTime DatetimeOfUpdate { get; set; }

    public decimal DepositAmount { get; set; }

    public string BankingAccountNumberRent { get; set; } = null!;

    public string BankingAccountNumberDepositReturn { get; set; } = null!;

    public string BankNameDepositReturn { get; set; } = null!;

    public DateOnly? EndDate { get; set; }

    public DateOnly? ReminderDate { get; set; }

    public decimal? MonthlyRentAmount { get; set; }

    public int RetailUnitId { get; set; }

    public int SystemAccessId { get; set; }

    public ICollection<Document> Documents { get; set; } = new List<Document>();

    public RetailUnit RetailUnit { get; set; } = null!;

    public SystemAccess SystemAccess { get; set; } = null!;
}
