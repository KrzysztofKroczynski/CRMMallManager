using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MallManager.Data.Entities;

public partial class MallManagerContext : DbContext
{
    public MallManagerContext()
    {
    }

    public MallManagerContext(DbContextOptions<MallManagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdditionalUserInfo> AdditionalUserInfos { get; set; }

    public virtual DbSet<Appsetting> Appsettings { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<CompanySize> CompanySizes { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<Lease> Leases { get; set; }

    public virtual DbSet<LeaseApplication> LeaseApplications { get; set; }

    public virtual DbSet<Manager> Managers { get; set; }

    public virtual DbSet<MarketingCampaign> MarketingCampaigns { get; set; }

    public virtual DbSet<MarketingCampaignReachDict> MarketingCampaignReachDicts { get; set; }

    public virtual DbSet<MarketingMaterial> MarketingMaterials { get; set; }

    public virtual DbSet<MassEvent> MassEvents { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<RetailUnit> RetailUnits { get; set; }

    public virtual DbSet<RetailUnitPurpose> RetailUnitPurposes { get; set; }

    public virtual DbSet<SignupStatusDict> SignupStatusDicts { get; set; }

    public virtual DbSet<SurfaceClassDict> SurfaceClassDicts { get; set; }

    public virtual DbSet<SystemAccess> SystemAccesses { get; set; }

    public virtual DbSet<SystemDict> SystemDicts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdditionalUserInfo>(entity =>
        {
            entity.HasKey(e => e.AspNetUsersId).HasName("AdditionalUserInfo_pk");

            entity.ToTable("AdditionalUserInfo");

            entity.Property(e => e.AspNetUsersId).HasColumnName("AspNetUsers_Id");
            entity.Property(e => e.UserPhoto).HasColumnType("image");

            entity.HasOne(d => d.AspNetUsers).WithOne(p => p.AdditionalUserInfo)
                .HasForeignKey<AdditionalUserInfo>(d => d.AspNetUsersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AdditionalUserInfo_AspNetUsers");
        });

        modelBuilder.Entity<Appsetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Appsetting_pk");

            entity.ToTable("Appsetting");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Key).HasMaxLength(1);
            entity.Property(e => e.Value).HasMaxLength(1);
        });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Company_pk");

            entity.ToTable("Company");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AspNetUsersId)
                .HasMaxLength(450)
                .HasColumnName("AspNetUsers_Id");
            entity.Property(e => e.CompanySizeId).HasColumnName("Company_size_id");
            entity.Property(e => e.Name).HasMaxLength(1);
            entity.Property(e => e.Nip).HasColumnName("NIP");
            entity.Property(e => e.Regon)
                .HasMaxLength(1)
                .HasColumnName("REGON");
            entity.Property(e => e.StartingCapital)
                .HasColumnType("money")
                .HasColumnName("Starting_capital");

            entity.HasOne(d => d.AspNetUsers).WithMany(p => p.Companies)
                .HasForeignKey(d => d.AspNetUsersId)
                .HasConstraintName("Company_AspNetUsers");

            entity.HasOne(d => d.CompanySize).WithMany(p => p.Companies)
                .HasForeignKey(d => d.CompanySizeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Company_Company_size");
        });

        modelBuilder.Entity<CompanySize>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Company_size_pk");

            entity.ToTable("Company_size");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.EmploymentLevel)
                .HasMaxLength(1)
                .HasColumnName("Employment_level");
            entity.Property(e => e.Name).HasMaxLength(1);
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Document_pk");

            entity.ToTable("Document");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.DateAdded).HasColumnName("Date_added");
            entity.Property(e => e.DocumentName)
                .HasMaxLength(1)
                .HasColumnName("Document_name");
            entity.Property(e => e.DocumentPdf).HasColumnName("Document_pdf");
            entity.Property(e => e.LeaseId).HasColumnName("Lease_Id");
            entity.Property(e => e.LeaseLeaseId).HasColumnName("Lease_Lease_id");
            entity.Property(e => e.MainDocumentId).HasColumnName("Main_document_id");

            entity.HasOne(d => d.Lease).WithMany(p => p.Documents)
                .HasForeignKey(d => d.LeaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Document_Lease");

            entity.HasOne(d => d.MainDocument).WithMany(p => p.InverseMainDocument)
                .HasForeignKey(d => d.MainDocumentId)
                .HasConstraintName("Document_Document");
        });

        modelBuilder.Entity<Lease>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Lease_pk");

            entity.ToTable("Lease");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.BankNameDepositReturn)
                .HasMaxLength(1)
                .HasColumnName("Bank_name_deposit_return");
            entity.Property(e => e.BankingAccountNumberDepositReturn)
                .HasMaxLength(1)
                .HasColumnName("Banking_account_number_deposit_return");
            entity.Property(e => e.BankingAccountNumberRent)
                .HasMaxLength(1)
                .HasColumnName("Banking_account_number_rent");
            entity.Property(e => e.DatetimeOfUpdate)
                .HasColumnType("datetime")
                .HasColumnName("Datetime_of_update");
            entity.Property(e => e.DepositAmount)
                .HasColumnType("money")
                .HasColumnName("Deposit_amount");
            entity.Property(e => e.EndDate).HasColumnName("End_date");
            entity.Property(e => e.MonthlyRentAmount)
                .HasColumnType("money")
                .HasColumnName("Monthly_rent_amount");
            entity.Property(e => e.ReminderDate).HasColumnName("Reminder_date");
            entity.Property(e => e.RetailUnitId).HasColumnName("Retail_unit_Id");
            entity.Property(e => e.StartDate).HasColumnName("Start_date");
            entity.Property(e => e.SystemAccessAspNetUsersId)
                .HasMaxLength(450)
                .HasColumnName("System_access_AspNetUsers_Id");

            entity.HasOne(d => d.RetailUnit).WithMany(p => p.Leases)
                .HasForeignKey(d => d.RetailUnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Lease_Retail_unit");

            entity.HasOne(d => d.SystemAccessAspNetUsers).WithMany(p => p.Leases)
                .HasForeignKey(d => d.SystemAccessAspNetUsersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Lease_System_access");
        });

        modelBuilder.Entity<LeaseApplication>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Lease_application_pk");

            entity.ToTable("Lease_application");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.DateEnd).HasColumnName("Date_end");
            entity.Property(e => e.DateStart).HasColumnName("Date_start");
            entity.Property(e => e.Description).HasMaxLength(1);
            entity.Property(e => e.LeaseAccessAspNetUsersId)
                .HasMaxLength(450)
                .HasColumnName("Lease_access_AspNetUsers_Id");
            entity.Property(e => e.SignupStatusDictId).HasColumnName("Signup_status_dict_Id");

            entity.HasOne(d => d.LeaseAccessAspNetUsers).WithMany(p => p.LeaseApplications)
                .HasForeignKey(d => d.LeaseAccessAspNetUsersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Table_24_Lease_access");

            entity.HasOne(d => d.SignupStatusDict).WithMany(p => p.LeaseApplications)
                .HasForeignKey(d => d.SignupStatusDictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Lease_application_Signup_status_dict");

            entity.HasMany(d => d.RetailUnitPurposes).WithMany(p => p.LeaseApplications)
                .UsingEntity<Dictionary<string, object>>(
                    "LeaseApplicationTailUnitPurpose",
                    r => r.HasOne<RetailUnitPurpose>().WithMany()
                        .HasForeignKey("RetailUnitPurposeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Table_27_Retail_unit_purpose"),
                    l => l.HasOne<LeaseApplication>().WithMany()
                        .HasForeignKey("LeaseApplicationId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Table_27_Lease_application"),
                    j =>
                    {
                        j.HasKey("LeaseApplicationId", "RetailUnitPurposeId").HasName("Lease_application_tail_unit_purpose_pk");
                        j.ToTable("Lease_application_tail_unit_purpose");
                        j.IndexerProperty<int>("LeaseApplicationId").HasColumnName("Lease_application_ID");
                        j.IndexerProperty<int>("RetailUnitPurposeId").HasColumnName("Retail_unit_purpose_ID");
                    });

            entity.HasMany(d => d.SurfaceClassDicts).WithMany(p => p.LeaseApplications)
                .UsingEntity<Dictionary<string, object>>(
                    "SurfaceClassDictSurfaceClassDict",
                    r => r.HasOne<SurfaceClassDict>().WithMany()
                        .HasForeignKey("SurfaceClassDictId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Table_28_Surface_class_dict"),
                    l => l.HasOne<LeaseApplication>().WithMany()
                        .HasForeignKey("LeaseApplicationId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Table_28_Lease_application"),
                    j =>
                    {
                        j.HasKey("LeaseApplicationId", "SurfaceClassDictId").HasName("Surface_class_dict_Surface_class_dict_pk");
                        j.ToTable("Surface_class_dict_Surface_class_dict");
                        j.IndexerProperty<int>("LeaseApplicationId").HasColumnName("Lease_application_ID");
                        j.IndexerProperty<int>("SurfaceClassDictId").HasColumnName("Surface_class_dict_ID");
                    });
        });

        modelBuilder.Entity<Manager>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Manager_pk");

            entity.ToTable("Manager");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Manager)
                .HasForeignKey<Manager>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Manager_AspNetUsers");
        });

        modelBuilder.Entity<MarketingCampaign>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Marketing_Campaign_pk");

            entity.ToTable("Marketing_Campaign");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CompanyId).HasColumnName("Company_ID");
            entity.Property(e => e.Description).HasMaxLength(1);
            entity.Property(e => e.EndDate).HasColumnName("End_Date");
            entity.Property(e => e.IsRerun).HasColumnName("Is_Rerun");
            entity.Property(e => e.MarketingCampaignReachDictId).HasColumnName("Marketing_Campaign_Reach_Dict_ID");
            entity.Property(e => e.OnWeekdays).HasColumnName("On_Weekdays");
            entity.Property(e => e.OnWeekends).HasColumnName("On_Weekends");
            entity.Property(e => e.PersonId).HasColumnName("Person_ID");
            entity.Property(e => e.RegardsInMall)
                .HasDefaultValue(true)
                .HasColumnName("Regards_In_Mall");
            entity.Property(e => e.StartDate).HasColumnName("Start_Date");
            entity.Property(e => e.SystemAccessAspNetUsersId)
                .HasMaxLength(450)
                .HasColumnName("System_access_AspNetUsers_Id");

            entity.HasOne(d => d.Company).WithMany(p => p.MarketingCampaigns)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("Marketing_Campaign_Company");

            entity.HasOne(d => d.MarketingCampaignReachDict).WithMany(p => p.MarketingCampaigns)
                .HasForeignKey(d => d.MarketingCampaignReachDictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Marketing_Campaign_Marketing_Campaign_Reach_Dict");

            entity.HasOne(d => d.Person).WithMany(p => p.MarketingCampaigns)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("Marketing_Campaign_Person");

            entity.HasOne(d => d.SystemAccessAspNetUsers).WithMany(p => p.MarketingCampaigns)
                .HasForeignKey(d => d.SystemAccessAspNetUsersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Marketing_Campaign_System_access");
        });

        modelBuilder.Entity<MarketingCampaignReachDict>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Marketing_Campaign_Reach_Dict_pk");

            entity.ToTable("Marketing_Campaign_Reach_Dict");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.MaximalHourlyReach).HasColumnName("Maximal_Hourly_Reach");
            entity.Property(e => e.MinimalHourlyReach).HasColumnName("Minimal_Hourly_Reach");
            entity.Property(e => e.Price).HasColumnType("money");
        });

        modelBuilder.Entity<MarketingMaterial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Marketing_Material_pk");

            entity.ToTable("Marketing_Material");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.MarketingCampaignId).HasColumnName("Marketing_Campaign_ID");
            entity.Property(e => e.Name).HasMaxLength(1);
            entity.Property(e => e.PriceFactor)
                .HasColumnType("money")
                .HasColumnName("Price_factor");

            entity.HasOne(d => d.MarketingCampaign).WithMany(p => p.MarketingMaterials)
                .HasForeignKey(d => d.MarketingCampaignId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Marketing_Material_Marketing_Campaign");
        });

        modelBuilder.Entity<MassEvent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Mass_event_pk");

            entity.ToTable("Mass_event");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.DateAdded)
                .HasColumnType("datetime")
                .HasColumnName("Date_added");
            entity.Property(e => e.DatetimeEnd)
                .HasColumnType("datetime")
                .HasColumnName("Datetime_end");
            entity.Property(e => e.DatetimeStart)
                .HasColumnType("datetime")
                .HasColumnName("Datetime_start");
            entity.Property(e => e.Description).HasMaxLength(1);
            entity.Property(e => e.IsApproved).HasColumnName("Is_approved");
            entity.Property(e => e.Location)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SystemAccessAspNetUsersId)
                .HasMaxLength(450)
                .HasColumnName("System_access_AspNetUsers_Id");

            entity.HasOne(d => d.SystemAccessAspNetUsers).WithMany(p => p.MassEvents)
                .HasForeignKey(d => d.SystemAccessAspNetUsersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Mass_event_System_access");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Message_pk");

            entity.ToTable("Message");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Content).HasMaxLength(1);
            entity.Property(e => e.DateTimeAdded)
                .HasColumnType("datetime")
                .HasColumnName("DateTime_added");
            entity.Property(e => e.SystemAccessAspNetUsersId)
                .HasMaxLength(450)
                .HasColumnName("System_access_AspNetUsers_Id");

            entity.HasOne(d => d.SystemAccessAspNetUsers).WithMany(p => p.Messages)
                .HasForeignKey(d => d.SystemAccessAspNetUsersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Message_System_access");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Person_pk");

            entity.ToTable("Person");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AspNetUsersId)
                .HasMaxLength(450)
                .HasColumnName("AspNetUsers_Id");
            entity.Property(e => e.Lastname).HasMaxLength(1);
            entity.Property(e => e.Name).HasMaxLength(1);
            entity.Property(e => e.Pesel)
                .HasMaxLength(1)
                .HasColumnName("PESEL");
            entity.Property(e => e.SecondName)
                .HasMaxLength(1)
                .HasColumnName("Second_name");

            entity.HasOne(d => d.AspNetUsers).WithMany(p => p.People)
                .HasForeignKey(d => d.AspNetUsersId)
                .HasConstraintName("Person_AspNetUsers");
        });

        modelBuilder.Entity<RetailUnit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Retail_unit_pk");

            entity.ToTable("Retail_unit");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.FloorNumber).HasColumnName("Floor_number");
            entity.Property(e => e.LocalNumber).HasColumnName("Local_number");
            entity.Property(e => e.LocalSurfaceArea)
                .HasColumnType("decimal(20, 2)")
                .HasColumnName("Local_surface_area");
            entity.Property(e => e.RetailUnitPurposeId).HasColumnName("Retail_unit_purpose_ID");

            entity.HasOne(d => d.RetailUnitPurpose).WithMany(p => p.RetailUnits)
                .HasForeignKey(d => d.RetailUnitPurposeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Retail_unit_Retail_unit_purpose");
        });

        modelBuilder.Entity<RetailUnitPurpose>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Retail_unit_purpose_pk");

            entity.ToTable("Retail_unit_purpose");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(1);
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SignupStatusDict>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Signup_status_dict_pk");

            entity.ToTable("Signup_status_dict");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(1);
        });

        modelBuilder.Entity<SurfaceClassDict>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Surface_class_dict_pk");

            entity.ToTable("Surface_class_dict");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.MaximumSurface)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Maximum_surface");
            entity.Property(e => e.MinimalSurface)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Minimal_surface");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SystemAccess>(entity =>
        {
            entity.HasKey(e => e.AspNetUsersId).HasName("System_access_pk");

            entity.ToTable("System_access");

            entity.Property(e => e.AspNetUsersId).HasColumnName("AspNetUsers_Id");
            entity.Property(e => e.AssignedManagerId)
                .HasMaxLength(450)
                .HasColumnName("Assigned_Manager_ID");
            entity.Property(e => e.SignupStatusDictId).HasColumnName("Signup_status_dict_Id");
            entity.Property(e => e.SystemDictId).HasColumnName("System_dict_Id");
            entity.Property(e => e.ValidUntil)
                .HasColumnType("datetime")
                .HasColumnName("Valid_until");

            entity.HasOne(d => d.AspNetUsers).WithOne(p => p.SystemAccess)
                .HasForeignKey<SystemAccess>(d => d.AspNetUsersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Local_access_AspNetUsers");

            entity.HasOne(d => d.AssignedManager).WithMany(p => p.SystemAccesses)
                .HasForeignKey(d => d.AssignedManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("System_access_Manager");

            entity.HasOne(d => d.SignupStatusDict).WithMany(p => p.SystemAccesses)
                .HasForeignKey(d => d.SignupStatusDictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Local_access_Signup_status_dict");

            entity.HasOne(d => d.SystemDict).WithMany(p => p.SystemAccesses)
                .HasForeignKey(d => d.SystemDictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("System_access_System_dict");
        });

        modelBuilder.Entity<SystemDict>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("System_dict_pk");

            entity.ToTable("System_dict");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(1);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
