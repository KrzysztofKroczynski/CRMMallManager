create database MallManager collate Polish_CI_AS
go

grant connect on database :: MallManager to dbo
go

grant view any column encryption key definition, view any column master key definition on database :: MallManager to [public]
go

create table dbo.Appsetting
(
    Id    int      not null
        constraint Appsetting_pk
            primary key,
    [Key] nvarchar not null,
    Value nvarchar not null
)
go

create table dbo.AspNetRoles
(
    Id               nvarchar(450) not null
        constraint PK_AspNetRoles
            primary key,
    Name             nvarchar(256),
    NormalizedName   nvarchar(256),
    ConcurrencyStamp nvarchar(max)
)
go

create table dbo.AspNetRoleClaims
(
    Id         int identity
        constraint PK_AspNetRoleClaims
            primary key,
    RoleId     nvarchar(450) not null
        constraint FK_AspNetRoleClaims_AspNetRoles_RoleId
            references dbo.AspNetRoles
            on delete cascade,
    ClaimType  nvarchar(max),
    ClaimValue nvarchar(max)
)
go

create index IX_AspNetRoleClaims_RoleId
    on dbo.AspNetRoleClaims (RoleId)
go

create unique index RoleNameIndex
    on dbo.AspNetRoles (NormalizedName)
    where [NormalizedName] IS NOT NULL
go

create table dbo.AspNetUsers
(
    Id                   nvarchar(450) not null
        constraint PK_AspNetUsers
            primary key,
    UserName             nvarchar(256),
    NormalizedUserName   nvarchar(256),
    Email                nvarchar(256),
    NormalizedEmail      nvarchar(256),
    EmailConfirmed       bit           not null,
    PasswordHash         nvarchar(max),
    SecurityStamp        nvarchar(max),
    ConcurrencyStamp     nvarchar(max),
    PhoneNumber          nvarchar(max),
    PhoneNumberConfirmed bit           not null,
    TwoFactorEnabled     bit           not null,
    LockoutEnd           datetimeoffset,
    LockoutEnabled       bit           not null,
    AccessFailedCount    int           not null
)
go

create table dbo.AdditionalUserInfo
(
    AspNetUsers_Id nvarchar(450) not null
        constraint AdditionalUserInfo_pk
            primary key
        constraint AdditionalUserInfo_AspNetUsers
            references dbo.AspNetUsers,
    UserPhoto      image         not null
)
go

create table dbo.AspNetUserClaims
(
    Id         int identity
        constraint PK_AspNetUserClaims
            primary key,
    UserId     nvarchar(450) not null
        constraint FK_AspNetUserClaims_AspNetUsers_UserId
            references dbo.AspNetUsers
            on delete cascade,
    ClaimType  nvarchar(max),
    ClaimValue nvarchar(max)
)
go

create index IX_AspNetUserClaims_UserId
    on dbo.AspNetUserClaims (UserId)
go

create table dbo.AspNetUserLogins
(
    LoginProvider       nvarchar(450) not null,
    ProviderKey         nvarchar(450) not null,
    ProviderDisplayName nvarchar(max),
    UserId              nvarchar(450) not null
        constraint FK_AspNetUserLogins_AspNetUsers_UserId
            references dbo.AspNetUsers
            on delete cascade,
    constraint PK_AspNetUserLogins
        primary key (LoginProvider, ProviderKey)
)
go

create index IX_AspNetUserLogins_UserId
    on dbo.AspNetUserLogins (UserId)
go

create table dbo.AspNetUserRoles
(
    UserId nvarchar(450) not null
        constraint FK_AspNetUserRoles_AspNetUsers_UserId
            references dbo.AspNetUsers
            on delete cascade,
    RoleId nvarchar(450) not null
        constraint FK_AspNetUserRoles_AspNetRoles_RoleId
            references dbo.AspNetRoles
            on delete cascade,
    constraint PK_AspNetUserRoles
        primary key (UserId, RoleId)
)
go

create index IX_AspNetUserRoles_RoleId
    on dbo.AspNetUserRoles (RoleId)
go

create table dbo.AspNetUserTokens
(
    UserId        nvarchar(450) not null
        constraint FK_AspNetUserTokens_AspNetUsers_UserId
            references dbo.AspNetUsers
            on delete cascade,
    LoginProvider nvarchar(450) not null,
    Name          nvarchar(450) not null,
    Value         nvarchar(max),
    constraint PK_AspNetUserTokens
        primary key (UserId, LoginProvider, Name)
)
go

create index EmailIndex
    on dbo.AspNetUsers (NormalizedEmail)
go

create unique index UserNameIndex
    on dbo.AspNetUsers (NormalizedUserName)
    where [NormalizedUserName] IS NOT NULL
go

create table dbo.Company_size
(
    Id               int           not null
        constraint Company_size_pk
            primary key,
    Name             nvarchar(max) not null,
    Employment_level nvarchar(max) not null
)
go

create table dbo.Company
(
    ID               int           not null
        constraint Company_pk
            primary key,
    AspNetUsers_Id   nvarchar(450)
        constraint Company_AspNetUsers
            references dbo.AspNetUsers,
    Name             nvarchar(max) not null,
    NIP              nvarchar(max) not null,
    REGON            nvarchar(max) not null,
    Starting_capital money         not null,
    Company_size_id  int           not null
        constraint Company_Company_size
            references dbo.Company_size
)
go

create table dbo.Manager
(
    ID nvarchar(450) not null
        constraint Manager_pk
            primary key
        constraint Manager_AspNetUsers
            references dbo.AspNetUsers
)
go

create table dbo.Marketing_Campaign_Reach_Dict
(
    ID                   int   not null
        constraint Marketing_Campaign_Reach_Dict_pk
            primary key,
    Minimal_Hourly_Reach int,
    Maximal_Hourly_Reach int,
    Price                money not null
)
go

create table dbo.Person
(
    ID             int           not null
        constraint Person_pk
            primary key,
    AspNetUsers_Id nvarchar(450)
        constraint Person_AspNetUsers
            references dbo.AspNetUsers,
    Name           nvarchar(max) not null,
    Lastname       nvarchar(max) not null,
    DateOfBirth    date          not null,
    PESEL          nvarchar(max) not null,
    Second_name    nvarchar(max)
)
go

create table dbo.Retail_unit_purpose
(
    ID          int           not null
        constraint Retail_unit_purpose_pk
            primary key,
    Name        varchar(20)   not null,
    Description nvarchar(max) not null
)
go

create table dbo.Retail_unit
(
    ID                     int            not null
        constraint Retail_unit_pk
            primary key,
    Floor_number           int            not null,
    Local_number           int            not null,
    Local_surface_area     decimal(10, 2) not null,
    Retail_unit_purpose_ID int            not null
        constraint Retail_unit_Retail_unit_purpose
            references dbo.Retail_unit_purpose
)
go

create table dbo.Signup_status_dict
(
    ID   int           not null
        constraint Signup_status_dict_pk
            primary key,
    Name nvarchar(max) not null
)
go

create table dbo.Surface_class_dict
(
    ID              int            not null
        constraint Surface_class_dict_pk
            primary key,
    Name            nvarchar(20)   not null,
    Minimal_surface decimal(10, 2) not null,
    Maximum_surface decimal(10, 2) not null
)
go

create table dbo.System_dict
(
    ID   int           not null
        constraint System_dict_pk
            primary key,
    Name nvarchar(max) not null
)
go

create table dbo.System_access
(
    ID                    int           not null
        constraint System_access_pk
            primary key,
    AspNetUsers_Id        nvarchar(450) not null
        constraint Local_access_AspNetUsers
            references dbo.AspNetUsers,
    Signup_status_dict_Id int           not null
        constraint Local_access_Signup_status_dict
            references dbo.Signup_status_dict,
    Valid_until           datetime,
    System_dict_Id        int           not null
        constraint System_access_System_dict
            references dbo.System_dict,
    Assigned_Manager_ID   nvarchar(450) not null
        constraint System_access_Manager
            references dbo.Manager
)
go

create table dbo.Lease
(
    ID                                    int           not null
        constraint Lease_pk
            primary key,
    Start_date                            date          not null,
    Datetime_of_update                    datetime      not null,
    Deposit_amount                        money         not null,
    Banking_account_number_rent           nvarchar(max) not null,
    Banking_account_number_deposit_return nvarchar(max) not null,
    Bank_name_deposit_return              nvarchar(max) not null,
    End_date                              date,
    Reminder_date                         date,
    Monthly_rent_amount                   money,
    Retail_unit_Id                        int           not null
        constraint Lease_Retail_unit
            references dbo.Retail_unit,
    System_access_ID                      int           not null
        constraint Lease_System_access
            references dbo.System_access
)
go

create table dbo.Document
(
    ID               int            not null
        constraint Document_pk
            primary key,
    Document_name    nvarchar(max)  not null,
    Document_pdf     varbinary(max) not null,
    Lease_Lease_id   int            not null,
    Date_added       int            not null,
    Lease_Id         int            not null
        constraint Document_Lease
            references dbo.Lease,
    Main_document_id int
        constraint Document_Document
            references dbo.Document
)
go

create table dbo.Lease_application
(
    ID                    int           not null
        constraint Lease_application_pk
            primary key,
    Date_start            date          not null,
    Date_end              date          not null,
    Description           nvarchar(max) not null,
    Signup_status_dict_Id int           not null
        constraint Lease_application_Signup_status_dict
            references dbo.Signup_status_dict,
    System_access_ID      int           not null
        constraint Lease_application_System_access
            references dbo.System_access
)
go

create table dbo.Lease_application_tail_unit_purpose
(
    Lease_application_ID   int not null
        constraint Lease_application_tail_unit_purpose_Lease_application
            references dbo.Lease_application,
    Retail_unit_purpose_ID int not null
        constraint Lease_application_tail_unit_purpose_Retail_unit_purpose
            references dbo.Retail_unit_purpose,
    constraint Lease_application_tail_unit_purpose_pk
        primary key (Lease_application_ID, Retail_unit_purpose_ID)
)
go

create table dbo.Marketing_Campaign
(
    ID                               int           not null
        constraint Marketing_Campaign_pk
            primary key,
    Start_Date                       date          not null,
    End_Date                         date          not null,
    Marketing_Campaign_Reach_Dict_ID int           not null
        constraint Marketing_Campaign_Marketing_Campaign_Reach_Dict
            references dbo.Marketing_Campaign_Reach_Dict,
    Description                      nvarchar(max) not null,
    Regards_In_Mall                  bit default 1 not null,
    Is_Rerun                         bit           not null,
    On_Weekdays                      bit           not null,
    On_Weekends                      bit           not null,
    Person_ID                        int
        constraint Marketing_Campaign_Person
            references dbo.Person,
    Company_ID                       int
        constraint Marketing_Campaign_Company
            references dbo.Company,
    System_access_ID                 int           not null
        constraint Marketing_Campaign_System_access
            references dbo.System_access
)
go

create table dbo.Marketing_Material
(
    ID                    int            not null
        constraint Marketing_Material_pk
            primary key,
    Marketing_Campaign_ID int            not null
        constraint Marketing_Material_Marketing_Campaign
            references dbo.Marketing_Campaign,
    Name                  nvarchar(max)  not null,
    Content               varbinary(max) not null,
    Price_factor          money
)
go

create table dbo.Mass_event
(
    ID               int           not null
        constraint Mass_event_pk
            primary key,
    Location         varchar(20)   not null,
    Description      nvarchar(max) not null,
    Datetime_start   datetime      not null,
    Datetime_end     datetime      not null,
    Is_approved      bit,
    Date_added       datetime      not null,
    System_access_ID int           not null
        constraint Mass_event_System_access
            references dbo.System_access
)
go

create table dbo.Message
(
    ID               int           not null
        constraint Message_pk
            primary key,
    Content          nvarchar(max) not null,
    DateTime_added   datetime      not null,
    System_access_ID int           not null
        constraint Message_System_access
            references dbo.System_access,
    AspNetUsers_Id   nvarchar(450) not null
        constraint Message_AspNetUsers
            references dbo.AspNetUsers
)
go

create table dbo.Surface_class_dict_Surface_class_dict
(
    Surface_class_dict_ID int not null
        constraint Surface_class_dict_Surface_class_dict_Surface_class_dict
            references dbo.Surface_class_dict,
    Lease_application_ID  int not null
        constraint Surface_class_dict_Surface_class_dict_Lease_application
            references dbo.Lease_application,
    constraint Surface_class_dict_Surface_class_dict_pk
        primary key (Surface_class_dict_ID, Lease_application_ID)
)
go

create table dbo.__EFMigrationsHistory
(
    MigrationId    nvarchar(150) not null
        constraint PK___EFMigrationsHistory
            primary key,
    ProductVersion nvarchar(32)  not null
)
go


	CREATE FUNCTION dbo.fn_diagramobjects() 
	RETURNS int
	WITH EXECUTE AS N'dbo'
	AS
	BEGIN
		declare @id_upgraddiagrams		int
		declare @id_sysdiagrams			int
		declare @id_helpdiagrams		int
		declare @id_helpdiagramdefinition	int
		declare @id_creatediagram	int
		declare @id_renamediagram	int
		declare @id_alterdiagram 	int 
		declare @id_dropdiagram		int
		declare @InstalledObjects	int

		select @InstalledObjects = 0

		select 	@id_upgraddiagrams = object_id(N'dbo.sp_upgraddiagrams'),
			@id_sysdiagrams = object_id(N'dbo.sysdiagrams'),
			@id_helpdiagrams = object_id(N'dbo.sp_helpdiagrams'),
			@id_helpdiagramdefinition = object_id(N'dbo.sp_helpdiagramdefinition'),
			@id_creatediagram = object_id(N'dbo.sp_creatediagram'),
			@id_renamediagram = object_id(N'dbo.sp_renamediagram'),
			@id_alterdiagram = object_id(N'dbo.sp_alterdiagram'), 
			@id_dropdiagram = object_id(N'dbo.sp_dropdiagram')

		if @id_upgraddiagrams is not null
			select @InstalledObjects = @InstalledObjects + 1
		if @id_sysdiagrams is not null
			select @InstalledObjects = @InstalledObjects + 2
		if @id_helpdiagrams is not null
			select @InstalledObjects = @InstalledObjects + 4
		if @id_helpdiagramdefinition is not null
			select @InstalledObjects = @InstalledObjects + 8
		if @id_creatediagram is not null
			select @InstalledObjects = @InstalledObjects + 16
		if @id_renamediagram is not null
			select @InstalledObjects = @InstalledObjects + 32
		if @id_alterdiagram  is not null
			select @InstalledObjects = @InstalledObjects + 64
		if @id_dropdiagram is not null
			select @InstalledObjects = @InstalledObjects + 128
		
		return @InstalledObjects 
	END
go

exec sp_addextendedproperty 'microsoft_database_tools_support', 1, 'SCHEMA', 'dbo', 'FUNCTION', 'fn_diagramobjects'
go

deny execute on dbo.fn_diagramobjects to guest
go

grant execute on dbo.fn_diagramobjects to [public]
go


	CREATE PROCEDURE dbo.sp_alterdiagram
	(
		@diagramname 	sysname,
		@owner_id	int	= null,
		@version 	int,
		@definition 	varbinary(max)
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
	
		declare @theId 			int
		declare @retval 		int
		declare @IsDbo 			int
		
		declare @UIDFound 		int
		declare @DiagId			int
		declare @ShouldChangeUID	int
	
		if(@diagramname is null)
		begin
			RAISERROR ('Invalid ARG', 16, 1)
			return -1
		end
	
		execute as caller;
		select @theId = DATABASE_PRINCIPAL_ID();	 
		select @IsDbo = IS_MEMBER(N'db_owner'); 
		if(@owner_id is null)
			select @owner_id = @theId;
		revert;
	
		select @ShouldChangeUID = 0
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname 
		
		if(@DiagId IS NULL or (@IsDbo = 0 and @theId <> @UIDFound))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1);
			return -3
		end
	
		if(@IsDbo <> 0)
		begin
			if(@UIDFound is null or USER_NAME(@UIDFound) is null) -- invalid principal_id
			begin
				select @ShouldChangeUID = 1 ;
			end
		end

		-- update dds data			
		update dbo.sysdiagrams set definition = @definition where diagram_id = @DiagId ;

		-- change owner
		if(@ShouldChangeUID = 1)
			update dbo.sysdiagrams set principal_id = @theId where diagram_id = @DiagId ;

		-- update dds version
		if(@version is not null)
			update dbo.sysdiagrams set version = @version where diagram_id = @DiagId ;

		return 0
	END
go

exec sp_addextendedproperty 'microsoft_database_tools_support', 1, 'SCHEMA', 'dbo', 'PROCEDURE', 'sp_alterdiagram'
go

deny execute on dbo.sp_alterdiagram to guest
go

grant execute on dbo.sp_alterdiagram to [public]
go


	CREATE PROCEDURE dbo.sp_creatediagram
	(
		@diagramname 	sysname,
		@owner_id		int	= null, 	
		@version 		int,
		@definition 	varbinary(max)
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
	
		declare @theId int
		declare @retval int
		declare @IsDbo	int
		declare @userName sysname
		if(@version is null or @diagramname is null)
		begin
			RAISERROR (N'E_INVALIDARG', 16, 1);
			return -1
		end
	
		execute as caller;
		select @theId = DATABASE_PRINCIPAL_ID(); 
		select @IsDbo = IS_MEMBER(N'db_owner');
		revert; 
		
		if @owner_id is null
		begin
			select @owner_id = @theId;
		end
		else
		begin
			if @theId <> @owner_id
			begin
				if @IsDbo = 0
				begin
					RAISERROR (N'E_INVALIDARG', 16, 1);
					return -1
				end
				select @theId = @owner_id
			end
		end
		-- next 2 line only for test, will be removed after define name unique
		if EXISTS(select diagram_id from dbo.sysdiagrams where principal_id = @theId and name = @diagramname)
		begin
			RAISERROR ('The name is already used.', 16, 1);
			return -2
		end
	
		insert into dbo.sysdiagrams(name, principal_id , version, definition)
				VALUES(@diagramname, @theId, @version, @definition) ;
		
		select @retval = @@IDENTITY 
		return @retval
	END
go

exec sp_addextendedproperty 'microsoft_database_tools_support', 1, 'SCHEMA', 'dbo', 'PROCEDURE', 'sp_creatediagram'
go

deny execute on dbo.sp_creatediagram to guest
go

grant execute on dbo.sp_creatediagram to [public]
go


	CREATE PROCEDURE dbo.sp_dropdiagram
	(
		@diagramname 	sysname,
		@owner_id	int	= null
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
		declare @theId 			int
		declare @IsDbo 			int
		
		declare @UIDFound 		int
		declare @DiagId			int
	
		if(@diagramname is null)
		begin
			RAISERROR ('Invalid value', 16, 1);
			return -1
		end
	
		EXECUTE AS CALLER;
		select @theId = DATABASE_PRINCIPAL_ID();
		select @IsDbo = IS_MEMBER(N'db_owner'); 
		if(@owner_id is null)
			select @owner_id = @theId;
		REVERT; 
		
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname 
		if(@DiagId IS NULL or (@IsDbo = 0 and @UIDFound <> @theId))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1)
			return -3
		end
	
		delete from dbo.sysdiagrams where diagram_id = @DiagId;
	
		return 0;
	END
go

exec sp_addextendedproperty 'microsoft_database_tools_support', 1, 'SCHEMA', 'dbo', 'PROCEDURE', 'sp_dropdiagram'
go

deny execute on dbo.sp_dropdiagram to guest
go

grant execute on dbo.sp_dropdiagram to [public]
go


	CREATE PROCEDURE dbo.sp_helpdiagramdefinition
	(
		@diagramname 	sysname,
		@owner_id	int	= null 		
	)
	WITH EXECUTE AS N'dbo'
	AS
	BEGIN
		set nocount on

		declare @theId 		int
		declare @IsDbo 		int
		declare @DiagId		int
		declare @UIDFound	int
	
		if(@diagramname is null)
		begin
			RAISERROR (N'E_INVALIDARG', 16, 1);
			return -1
		end
	
		execute as caller;
		select @theId = DATABASE_PRINCIPAL_ID();
		select @IsDbo = IS_MEMBER(N'db_owner');
		if(@owner_id is null)
			select @owner_id = @theId;
		revert; 
	
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname;
		if(@DiagId IS NULL or (@IsDbo = 0 and @UIDFound <> @theId ))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1);
			return -3
		end

		select version, definition FROM dbo.sysdiagrams where diagram_id = @DiagId ; 
		return 0
	END
go

exec sp_addextendedproperty 'microsoft_database_tools_support', 1, 'SCHEMA', 'dbo', 'PROCEDURE',
     'sp_helpdiagramdefinition'
go

deny execute on dbo.sp_helpdiagramdefinition to guest
go

grant execute on dbo.sp_helpdiagramdefinition to [public]
go


	CREATE PROCEDURE dbo.sp_helpdiagrams
	(
		@diagramname sysname = NULL,
		@owner_id int = NULL
	)
	WITH EXECUTE AS N'dbo'
	AS
	BEGIN
		DECLARE @user sysname
		DECLARE @dboLogin bit
		EXECUTE AS CALLER;
			SET @user = USER_NAME();
			SET @dboLogin = CONVERT(bit,IS_MEMBER('db_owner'));
		REVERT;
		SELECT
			[Database] = DB_NAME(),
			[Name] = name,
			[ID] = diagram_id,
			[Owner] = USER_NAME(principal_id),
			[OwnerID] = principal_id
		FROM
			sysdiagrams
		WHERE
			(@dboLogin = 1 OR USER_NAME(principal_id) = @user) AND
			(@diagramname IS NULL OR name = @diagramname) AND
			(@owner_id IS NULL OR principal_id = @owner_id)
		ORDER BY
			4, 5, 1
	END
go

exec sp_addextendedproperty 'microsoft_database_tools_support', 1, 'SCHEMA', 'dbo', 'PROCEDURE', 'sp_helpdiagrams'
go

deny execute on dbo.sp_helpdiagrams to guest
go

grant execute on dbo.sp_helpdiagrams to [public]
go


	CREATE PROCEDURE dbo.sp_renamediagram
	(
		@diagramname 		sysname,
		@owner_id		int	= null,
		@new_diagramname	sysname
	
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
		declare @theId 			int
		declare @IsDbo 			int
		
		declare @UIDFound 		int
		declare @DiagId			int
		declare @DiagIdTarg		int
		declare @u_name			sysname
		if((@diagramname is null) or (@new_diagramname is null))
		begin
			RAISERROR ('Invalid value', 16, 1);
			return -1
		end
	
		EXECUTE AS CALLER;
		select @theId = DATABASE_PRINCIPAL_ID();
		select @IsDbo = IS_MEMBER(N'db_owner'); 
		if(@owner_id is null)
			select @owner_id = @theId;
		REVERT;
	
		select @u_name = USER_NAME(@owner_id)
	
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname 
		if(@DiagId IS NULL or (@IsDbo = 0 and @UIDFound <> @theId))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1)
			return -3
		end
	
		-- if((@u_name is not null) and (@new_diagramname = @diagramname))	-- nothing will change
		--	return 0;
	
		if(@u_name is null)
			select @DiagIdTarg = diagram_id from dbo.sysdiagrams where principal_id = @theId and name = @new_diagramname
		else
			select @DiagIdTarg = diagram_id from dbo.sysdiagrams where principal_id = @owner_id and name = @new_diagramname
	
		if((@DiagIdTarg is not null) and  @DiagId <> @DiagIdTarg)
		begin
			RAISERROR ('The name is already used.', 16, 1);
			return -2
		end		
	
		if(@u_name is null)
			update dbo.sysdiagrams set [name] = @new_diagramname, principal_id = @theId where diagram_id = @DiagId
		else
			update dbo.sysdiagrams set [name] = @new_diagramname where diagram_id = @DiagId
		return 0
	END
go

exec sp_addextendedproperty 'microsoft_database_tools_support', 1, 'SCHEMA', 'dbo', 'PROCEDURE', 'sp_renamediagram'
go

deny execute on dbo.sp_renamediagram to guest
go

grant execute on dbo.sp_renamediagram to [public]
go


	CREATE PROCEDURE dbo.sp_upgraddiagrams
	AS
	BEGIN
		IF OBJECT_ID(N'dbo.sysdiagrams') IS NOT NULL
			return 0;
	
		CREATE TABLE dbo.sysdiagrams
		(
			name sysname NOT NULL,
			principal_id int NOT NULL,	-- we may change it to varbinary(85)
			diagram_id int PRIMARY KEY IDENTITY,
			version int,
	
			definition varbinary(max)
			CONSTRAINT UK_principal_name UNIQUE
			(
				principal_id,
				name
			)
		);


		/* Add this if we need to have some form of extended properties for diagrams */
		/*
		IF OBJECT_ID(N'dbo.sysdiagram_properties') IS NULL
		BEGIN
			CREATE TABLE dbo.sysdiagram_properties
			(
				diagram_id int,
				name sysname,
				value varbinary(max) NOT NULL
			)
		END
		*/

		IF OBJECT_ID(N'dbo.dtproperties') IS NOT NULL
		begin
			insert into dbo.sysdiagrams
			(
				[name],
				[principal_id],
				[version],
				[definition]
			)
			select	 
				convert(sysname, dgnm.[uvalue]),
				DATABASE_PRINCIPAL_ID(N'dbo'),			-- will change to the sid of sa
				0,							-- zero for old format, dgdef.[version],
				dgdef.[lvalue]
			from dbo.[dtproperties] dgnm
				inner join dbo.[dtproperties] dggd on dggd.[property] = 'DtgSchemaGUID' and dggd.[objectid] = dgnm.[objectid]	
				inner join dbo.[dtproperties] dgdef on dgdef.[property] = 'DtgSchemaDATA' and dgdef.[objectid] = dgnm.[objectid]
				
			where dgnm.[property] = 'DtgSchemaNAME' and dggd.[uvalue] like N'_EA3E6268-D998-11CE-9454-00AA00A3F36E_' 
			return 2;
		end
		return 1;
	END
go

exec sp_addextendedproperty 'microsoft_database_tools_support', 1, 'SCHEMA', 'dbo', 'PROCEDURE', 'sp_upgraddiagrams'
go

