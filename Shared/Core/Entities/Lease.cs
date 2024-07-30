using System;
using System.Collections.Generic;

namespace MallManager.Data.Entities;

public partial class Lease
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

    public string SystemAccessAspNetUsersId { get; set; } = null!;

    public int RetailUnitId { get; set; }

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual RetailUnit RetailUnit { get; set; } = null!;

    public virtual SystemAccess SystemAccessAspNetUsers { get; set; } = null!;
}
