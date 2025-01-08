//using System;
//using System.Collections.Generic;
//using Microsoft.EntityFrameworkCore;

//namespace DAL.Domain;

//public partial class E_PSOContext : DbContext
//{
//    public E_PSOContext(DbContextOptions<E_PSOContext> options)
//        : base(options)
//    {
//    }

//    public virtual DbSet<ApplicationLog> ApplicationLogs { get; set; }

//    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

//    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

//    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

//    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

//    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

//    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

//    public virtual DbSet<Con_AccountApiAuthorization> Con_AccountApiAuthorizations { get; set; }

//    public virtual DbSet<Con_Channel> Con_Channels { get; set; }

//    public virtual DbSet<Con_ChannelAccountDetail> Con_ChannelAccountDetails { get; set; }

//    public virtual DbSet<Con_ChannelType> Con_ChannelTypes { get; set; }

//    public virtual DbSet<Con_CostProfile> Con_CostProfiles { get; set; }

//    public virtual DbSet<Con_CostProfileDetail> Con_CostProfileDetails { get; set; }

//    public virtual DbSet<Con_CostProfileDetailsSlabPolicy> Con_CostProfileDetailsSlabPolicies { get; set; }

//    public virtual DbSet<Con_FeeProfile> Con_FeeProfiles { get; set; }

//    public virtual DbSet<Con_FeeProfileDetail> Con_FeeProfileDetails { get; set; }

//    public virtual DbSet<Con_FeeProfileDetailsSlabPolicy> Con_FeeProfileDetailsSlabPolicies { get; set; }

//    public virtual DbSet<Con_MerchantPortalAuthorization> Con_MerchantPortalAuthorizations { get; set; }

//    public virtual DbSet<Con_PaymentAcceptProfile> Con_PaymentAcceptProfiles { get; set; }

//    public virtual DbSet<Con_PaymentAcceptProfileDetail> Con_PaymentAcceptProfileDetails { get; set; }

//    public virtual DbSet<Gen_AccountInformation> Gen_AccountInformations { get; set; }

//    public virtual DbSet<Gen_BankInformartion> Gen_BankInformartions { get; set; }

//    public virtual DbSet<IdentityRequestResponse> IdentityRequestResponses { get; set; }

//    public virtual DbSet<MenuSetup> MenuSetups { get; set; }

//    public virtual DbSet<OpenIddictApplication> OpenIddictApplications { get; set; }

//    public virtual DbSet<OpenIddictAuthorization> OpenIddictAuthorizations { get; set; }

//    public virtual DbSet<OpenIddictScope> OpenIddictScopes { get; set; }

//    public virtual DbSet<OpenIddictToken> OpenIddictTokens { get; set; }

//    public virtual DbSet<RSAEncryptDataDuplicationCheck> RSAEncryptDataDuplicationChecks { get; set; }

//    public virtual DbSet<RoleWiseMenuPermission> RoleWiseMenuPermissions { get; set; }

//    public virtual DbSet<Tran_CheckoutInfo> Tran_CheckoutInfos { get; set; }

//    public virtual DbSet<Tran_DailyBalance> Tran_DailyBalances { get; set; }

//    public virtual DbSet<Tran_DayClosingInformation> Tran_DayClosingInformations { get; set; }

//    public virtual DbSet<Tran_Journal> Tran_Journals { get; set; }

//    public virtual DbSet<Tran_LimitDayUserWise> Tran_LimitDayUserWises { get; set; }

//    public virtual DbSet<Tran_LimitMonthUserWise> Tran_LimitMonthUserWises { get; set; }

//    public virtual DbSet<Tran_LimitPackage> Tran_LimitPackages { get; set; }

//    public virtual DbSet<Tran_LimitPackageDetail> Tran_LimitPackageDetails { get; set; }

//    public virtual DbSet<Tran_StackHoldersInformation> Tran_StackHoldersInformations { get; set; }

//    public virtual DbSet<Tran_TransactionInformation> Tran_TransactionInformations { get; set; }

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<ApplicationLog>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK_Log");

//            entity.ToTable("ApplicationLog");

//            entity.Property(e => e.BankCode)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.CreatedBy)
//                .HasMaxLength(100)
//                .IsUnicode(false);
//            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
//            entity.Property(e => e.ReferenceId)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//            entity.Property(e => e.Request).HasColumnType("text");
//            entity.Property(e => e.Response).HasColumnType("text");
//            entity.Property(e => e.Service)
//                .HasMaxLength(100)
//                .IsUnicode(false);
//            entity.Property(e => e.Status)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.TransactionId)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//            entity.Property(e => e.UpdatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
//        });

//        modelBuilder.Entity<AspNetRole>(entity =>
//        {
//            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
//                .IsUnique()
//                .HasFilter("([NormalizedName] IS NOT NULL)");

//            entity.Property(e => e.Name).HasMaxLength(256);
//            entity.Property(e => e.NormalizedName).HasMaxLength(256);
//        });

//        modelBuilder.Entity<AspNetRoleClaim>(entity =>
//        {
//            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

//            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
//        });

//        modelBuilder.Entity<AspNetUser>(entity =>
//        {
//            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

//            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
//                .IsUnique()
//                .HasFilter("([NormalizedUserName] IS NOT NULL)");

//            entity.Property(e => e.Email).HasMaxLength(256);
//            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
//            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
//            entity.Property(e => e.UserName).HasMaxLength(256);

//            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
//                .UsingEntity<Dictionary<string, object>>(
//                    "AspNetUserRole",
//                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
//                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
//                    j =>
//                    {
//                        j.HasKey("UserId", "RoleId");
//                        j.ToTable("AspNetUserRoles");
//                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
//                    });
//        });

//        modelBuilder.Entity<AspNetUserClaim>(entity =>
//        {
//            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

//            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
//        });

//        modelBuilder.Entity<AspNetUserLogin>(entity =>
//        {
//            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

//            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

//            entity.Property(e => e.LoginProvider).HasMaxLength(85);
//            entity.Property(e => e.ProviderKey).HasMaxLength(85);

//            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
//        });

//        modelBuilder.Entity<AspNetUserToken>(entity =>
//        {
//            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

//            entity.Property(e => e.LoginProvider).HasMaxLength(85);
//            entity.Property(e => e.Name).HasMaxLength(85);

//            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
//        });

//        modelBuilder.Entity<Con_AccountApiAuthorization>(entity =>
//        {
//            entity.ToTable("Con_AccountApiAuthorization");

//            entity.Property(e => e.ClientId)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//            entity.Property(e => e.ClientSecret)
//                .HasMaxLength(500)
//                .IsUnicode(false);
//            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
//            entity.Property(e => e.CreatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.LastLoginDate).HasColumnType("datetime");
//            entity.Property(e => e.LastUpdatedAt).HasColumnType("datetime");
//            entity.Property(e => e.LastUpdatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//        });

//        modelBuilder.Entity<Con_Channel>(entity =>
//        {
//            entity.HasKey(e => e.ChannelId).HasName("PK_ConChennel");

//            entity.ToTable("Con_Channel");

//            entity.Property(e => e.ActiveEndDate).HasColumnType("datetime");
//            entity.Property(e => e.ActiveStartDate).HasColumnType("datetime");
//            entity.Property(e => e.ChannelEnum)
//                .HasMaxLength(100)
//                .IsUnicode(false);
//            entity.Property(e => e.ChannelName)
//                .HasMaxLength(100)
//                .IsUnicode(false);
//            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
//            entity.Property(e => e.CreatedBy)
//                .HasMaxLength(100)
//                .IsUnicode(false);
//            entity.Property(e => e.LastUpdatedAt).HasColumnType("datetime");
//            entity.Property(e => e.LastUpdatedBy)
//                .HasMaxLength(100)
//                .IsUnicode(false);
//        });

//        modelBuilder.Entity<Con_ChannelAccountDetail>(entity =>
//        {
//            entity.HasKey(e => e.ChannelAccountDetailId).HasName("PK_ConMerchantCategoryCode");

//            entity.ToTable("Con_ChannelAccountDetail");

//            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
//            entity.Property(e => e.CreatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.Description)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//            entity.Property(e => e.LastUpdatedAt).HasColumnType("datetime");
//            entity.Property(e => e.LastUpdatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.MCC)
//                .HasMaxLength(100)
//                .IsUnicode(false);
//            entity.Property(e => e.MID)
//                .HasMaxLength(100)
//                .IsUnicode(false);
//            entity.Property(e => e.Provider)
//                .HasMaxLength(100)
//                .IsUnicode(false);
//        });

//        modelBuilder.Entity<Con_ChannelType>(entity =>
//        {
//            entity.HasKey(e => e.ChannelTypeId);

//            entity.ToTable("Con_ChannelType");

//            entity.Property(e => e.AccountType)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.BankCode)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.ChannelTypeName)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
//            entity.Property(e => e.CreatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.LastUpdatedAt).HasColumnType("datetime");
//            entity.Property(e => e.LastUpdatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.Logo)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//        });

//        modelBuilder.Entity<Con_CostProfile>(entity =>
//        {
//            entity.HasKey(e => e.CostProfileId).HasName("PK_ConCostProfile");

//            entity.ToTable("Con_CostProfile");

//            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
//            entity.Property(e => e.CreatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.LastUpdatedAt).HasColumnType("datetime");
//            entity.Property(e => e.LastUpdatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.ProfileName)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//        });

//        modelBuilder.Entity<Con_CostProfileDetail>(entity =>
//        {
//            entity.HasKey(e => e.CostProfileDetailId).HasName("PK_ConCostProfileDetails");

//            entity.Property(e => e.CostAmount).HasColumnType("decimal(18, 4)");
//            entity.Property(e => e.CostAmountFixed).HasColumnType("decimal(18, 4)");
//            entity.Property(e => e.CostAmountInPercentage).HasColumnType("decimal(18, 4)");
//            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
//            entity.Property(e => e.CreatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.EndDate).HasColumnType("datetime");
//            entity.Property(e => e.LastUpdatedAt).HasColumnType("datetime");
//            entity.Property(e => e.LastUpdatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.MCC)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.StartDate).HasColumnType("datetime");
//            entity.Property(e => e.StaticMaximumCostAmount).HasColumnType("decimal(18, 4)");
//            entity.Property(e => e.VATDiductPercentOnCostAmount).HasColumnType("decimal(18, 4)");
//        });

//        modelBuilder.Entity<Con_CostProfileDetailsSlabPolicy>(entity =>
//        {
//            entity.HasKey(e => e.CostProfileDetailSlabPolicyId).HasName("PK_Tran_TransactionCostSlabPolicy");

//            entity.ToTable("Con_CostProfileDetailsSlabPolicy");

//            entity.Property(e => e.CostAmountFixed).HasColumnType("numeric(18, 4)");
//            entity.Property(e => e.CostAmountInPercentage).HasColumnType("numeric(18, 4)");
//            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
//            entity.Property(e => e.CreatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.Description)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//            entity.Property(e => e.LastUpdatedAt).HasColumnType("datetime");
//            entity.Property(e => e.LastUpdatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.MaxAmount).HasColumnType("numeric(18, 4)");
//            entity.Property(e => e.MinAmount).HasColumnType("decimal(18, 4)");
//            entity.Property(e => e.SegmentAmount).HasColumnType("numeric(18, 4)");
//            entity.Property(e => e.StaticMaximumCostAmount).HasColumnType("numeric(18, 4)");
//        });

//        modelBuilder.Entity<Con_FeeProfile>(entity =>
//        {
//            entity.HasKey(e => e.FeeProfileId).HasName("PK_ConFeeProfile");

//            entity.ToTable("Con_FeeProfile");

//            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
//            entity.Property(e => e.CreatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.LastUpdatedAt).HasColumnType("datetime");
//            entity.Property(e => e.LastUpdatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.ProfileName)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//        });

//        modelBuilder.Entity<Con_FeeProfileDetail>(entity =>
//        {
//            entity.HasKey(e => e.FeeProfileDetailsId).HasName("PK_ConFeeProfileDetails");

//            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
//            entity.Property(e => e.CreatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.EndDate).HasColumnType("datetime");
//            entity.Property(e => e.FeeAmount).HasColumnType("decimal(18, 4)");
//            entity.Property(e => e.FeeAmountFixed).HasColumnType("decimal(18, 4)");
//            entity.Property(e => e.FeeAmountInPercentage).HasColumnType("decimal(18, 4)");
//            entity.Property(e => e.LastUpdatedAt).HasColumnType("datetime");
//            entity.Property(e => e.LastUpdatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.MCC)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.StartDate).HasColumnType("datetime");
//            entity.Property(e => e.StaticMaximumFeeAmount).HasColumnType("decimal(18, 4)");
//            entity.Property(e => e.VATDiductPercentOnFeeAmount).HasColumnType("decimal(18, 4)");
//        });

//        modelBuilder.Entity<Con_FeeProfileDetailsSlabPolicy>(entity =>
//        {
//            entity.HasKey(e => e.FeeProfileDetailsSlabPolicyId).HasName("PK_Tran_TransactionFeeSlabPolicy");

//            entity.ToTable("Con_FeeProfileDetailsSlabPolicy");

//            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
//            entity.Property(e => e.CreatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.Description)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//            entity.Property(e => e.FeeAmountFixed).HasColumnType("numeric(18, 4)");
//            entity.Property(e => e.FeeAmountInPercentage).HasColumnType("numeric(18, 4)");
//            entity.Property(e => e.LastUpdatedAt).HasColumnType("datetime");
//            entity.Property(e => e.LastUpdatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.MaxAmount).HasColumnType("numeric(18, 4)");
//            entity.Property(e => e.MinAmount).HasColumnType("decimal(18, 4)");
//            entity.Property(e => e.SegmentAmount).HasColumnType("numeric(18, 4)");
//            entity.Property(e => e.StaticMaximumFeeAmount).HasColumnType("numeric(18, 4)");
//        });

//        modelBuilder.Entity<Con_MerchantPortalAuthorization>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK_MerchantPortalAuthorization");

//            entity.ToTable("Con_MerchantPortalAuthorization");

//            entity.Property(e => e.AccessToken)
//                .HasMaxLength(300)
//                .IsUnicode(false);
//            entity.Property(e => e.AccessTokenDate).HasColumnType("datetime");
//            entity.Property(e => e.ClientId)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//            entity.Property(e => e.ClientSecret)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//            entity.Property(e => e.FullName)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//            entity.Property(e => e.LastLoginDate).HasColumnType("datetime");
//            entity.Property(e => e.RefreshToken)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//            entity.Property(e => e.RefreshTokenDate).HasColumnType("datetime");
//        });

//        modelBuilder.Entity<Con_PaymentAcceptProfile>(entity =>
//        {
//            entity.HasKey(e => e.PaymentAcceptProfileId);

//            entity.ToTable("Con_PaymentAcceptProfile");

//            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
//            entity.Property(e => e.CreatedBy)
//                .HasMaxLength(100)
//                .IsUnicode(false);
//            entity.Property(e => e.LastUpdatedAt).HasColumnType("datetime");
//            entity.Property(e => e.LastUpdatedBy)
//                .HasMaxLength(100)
//                .IsUnicode(false);
//            entity.Property(e => e.ProfileName)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//        });

//        modelBuilder.Entity<Con_PaymentAcceptProfileDetail>(entity =>
//        {
//            entity.HasKey(e => e.PaymentAcceptProfileDetailsId);

//            entity.ToTable("Con_PaymentAcceptProfileDetail");

//            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
//            entity.Property(e => e.CreatedBy)
//                .HasMaxLength(100)
//                .IsUnicode(false);
//            entity.Property(e => e.LastUpdatedAt).HasColumnType("datetime");
//            entity.Property(e => e.LastUpdatedBy)
//                .HasMaxLength(100)
//                .IsUnicode(false);
//        });

//        modelBuilder.Entity<Gen_AccountInformation>(entity =>
//        {
//            entity.HasKey(e => e.AccountId).HasName("PK_AccountInformation");

//            entity.ToTable("Gen_AccountInformation");

//            entity.Property(e => e.Address1).HasMaxLength(200);
//            entity.Property(e => e.Address2).HasMaxLength(200);
//            entity.Property(e => e.City)
//                .HasMaxLength(10)
//                .IsUnicode(false);
//            entity.Property(e => e.ClientSecrect)
//                .HasMaxLength(500)
//                .IsUnicode(false);
//            entity.Property(e => e.CompanyAddress1)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//            entity.Property(e => e.CompanyAddress2)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//            entity.Property(e => e.CompanyDistrict)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.CompanyEmail)
//                .HasMaxLength(100)
//                .IsUnicode(false);
//            entity.Property(e => e.CompanyFax)
//                .HasMaxLength(100)
//                .IsUnicode(false);
//            entity.Property(e => e.CompanyLogo)
//                .HasMaxLength(100)
//                .IsUnicode(false);
//            entity.Property(e => e.CompanyName)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//            entity.Property(e => e.CompanyPhone)
//                .HasMaxLength(100)
//                .IsUnicode(false);
//            entity.Property(e => e.CompanyPostalCode)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.CompanyShortName)
//                .HasMaxLength(100)
//                .IsUnicode(false);
//            entity.Property(e => e.CompanyThana)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.CompanyType)
//                .HasMaxLength(100)
//                .IsUnicode(false);
//            entity.Property(e => e.CompanyWebsite)
//                .HasMaxLength(100)
//                .IsUnicode(false);
//            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
//            entity.Property(e => e.CreatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
//            entity.Property(e => e.Designation)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.Email).HasMaxLength(256);
//            entity.Property(e => e.FathersName)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//            entity.Property(e => e.FullName)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//            entity.Property(e => e.Gender)
//                .HasMaxLength(10)
//                .IsUnicode(false);
//            entity.Property(e => e.KycImage)
//                .HasMaxLength(20)
//                .IsUnicode(false);
//            entity.Property(e => e.LastPasswordChangedDate).HasColumnType("datetime");
//            entity.Property(e => e.LastUpdatedAt).HasColumnType("datetime");
//            entity.Property(e => e.LastUpdatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.Latitude)
//                .HasMaxLength(10)
//                .IsUnicode(false);
//            entity.Property(e => e.Longitude)
//                .HasMaxLength(10)
//                .IsUnicode(false);
//            entity.Property(e => e.MothersName)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//            entity.Property(e => e.NationalId)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.NationalIdBack)
//                .HasMaxLength(20)
//                .IsUnicode(false);
//            entity.Property(e => e.NationalIdFront)
//                .HasMaxLength(20)
//                .IsUnicode(false);
//            entity.Property(e => e.NidNumber)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.NidPIN)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.Occupation)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.Organization)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.PasswordHash)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//            entity.Property(e => e.PhoneNumber)
//                .HasMaxLength(15)
//                .IsUnicode(false);
//            entity.Property(e => e.PoliceStation)
//                .HasMaxLength(10)
//                .IsUnicode(false);
//            entity.Property(e => e.PostalCode)
//                .HasMaxLength(10)
//                .IsUnicode(false);
//            entity.Property(e => e.ProductsOrServicesDetails)
//                .HasMaxLength(100)
//                .IsUnicode(false);
//            entity.Property(e => e.ProfileImage)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.SecondaryEmail)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.SecondaryMobile)
//                .HasMaxLength(15)
//                .IsUnicode(false);
//            entity.Property(e => e.Status)
//                .HasMaxLength(20)
//                .IsUnicode(false);
//            entity.Property(e => e.Title)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//            entity.Property(e => e.UserName).HasMaxLength(256);
//        });

//        modelBuilder.Entity<Gen_BankInformartion>(entity =>
//        {
//            entity.ToTable("Gen_BankInformartion");

//            entity.Property(e => e.BankAccountName)
//                .HasMaxLength(300)
//                .IsUnicode(false);
//            entity.Property(e => e.BankAccountNo)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.BankName)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//            entity.Property(e => e.Branch)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
//            entity.Property(e => e.CreatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.LastUpdatedAt).HasColumnType("datetime");
//            entity.Property(e => e.LastUpdatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.RoutingNo)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//        });

//        modelBuilder.Entity<IdentityRequestResponse>(entity =>
//        {
//            entity.ToTable("IdentityRequestResponse");
//        });

//        modelBuilder.Entity<MenuSetup>(entity =>
//        {
//            entity.ToTable("MenuSetup");
//        });

//        modelBuilder.Entity<OpenIddictApplication>(entity =>
//        {
//            entity.HasIndex(e => e.ClientId, "IX_OpenIddictApplications_ClientId")
//                .IsUnique()
//                .HasFilter("([ClientId] IS NOT NULL)");

//            entity.Property(e => e.ClientId).HasMaxLength(100);
//            entity.Property(e => e.ConcurrencyToken).HasMaxLength(50);
//            entity.Property(e => e.ConsentType).HasMaxLength(50);
//            entity.Property(e => e.Type).HasMaxLength(50);
//        });

//        modelBuilder.Entity<OpenIddictAuthorization>(entity =>
//        {
//            entity.HasIndex(e => new { e.ApplicationId, e.Status, e.Subject, e.Type }, "IX_OpenIddictAuthorizations_ApplicationId_Status_Subject_Type");

//            entity.Property(e => e.ConcurrencyToken).HasMaxLength(50);
//            entity.Property(e => e.Status).HasMaxLength(50);
//            entity.Property(e => e.Subject).HasMaxLength(400);
//            entity.Property(e => e.Type).HasMaxLength(50);

//            entity.HasOne(d => d.Application).WithMany(p => p.OpenIddictAuthorizations).HasForeignKey(d => d.ApplicationId);
//        });

//        modelBuilder.Entity<OpenIddictScope>(entity =>
//        {
//            entity.HasIndex(e => e.Name, "IX_OpenIddictScopes_Name")
//                .IsUnique()
//                .HasFilter("([Name] IS NOT NULL)");

//            entity.Property(e => e.ConcurrencyToken).HasMaxLength(50);
//            entity.Property(e => e.Name).HasMaxLength(200);
//        });

//        modelBuilder.Entity<OpenIddictToken>(entity =>
//        {
//            entity.HasIndex(e => new { e.ApplicationId, e.Status, e.Subject, e.Type }, "IX_OpenIddictTokens_ApplicationId_Status_Subject_Type");

//            entity.HasIndex(e => e.AuthorizationId, "IX_OpenIddictTokens_AuthorizationId");

//            entity.HasIndex(e => e.ReferenceId, "IX_OpenIddictTokens_ReferenceId")
//                .IsUnique()
//                .HasFilter("([ReferenceId] IS NOT NULL)");

//            entity.Property(e => e.ConcurrencyToken).HasMaxLength(50);
//            entity.Property(e => e.ReferenceId).HasMaxLength(100);
//            entity.Property(e => e.Status).HasMaxLength(50);
//            entity.Property(e => e.Subject).HasMaxLength(400);
//            entity.Property(e => e.Type).HasMaxLength(50);

//            entity.HasOne(d => d.Application).WithMany(p => p.OpenIddictTokens).HasForeignKey(d => d.ApplicationId);

//            entity.HasOne(d => d.Authorization).WithMany(p => p.OpenIddictTokens).HasForeignKey(d => d.AuthorizationId);
//        });

//        modelBuilder.Entity<RSAEncryptDataDuplicationCheck>(entity =>
//        {
//            entity.ToTable("RSAEncryptDataDuplicationCheck");
//        });

//        modelBuilder.Entity<RoleWiseMenuPermission>(entity =>
//        {
//            entity.ToTable("RoleWiseMenuPermission");
//        });

//        modelBuilder.Entity<Tran_CheckoutInfo>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK_CheckoutInfo");

//            entity.ToTable("Tran_CheckoutInfo");

//            entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");
//            entity.Property(e => e.BillerName)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.BillerPhone)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.BillingAddress)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//            entity.Property(e => e.CancelReturnUrl)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//            entity.Property(e => e.ChannelType)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.ClientId)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
//            entity.Property(e => e.CreatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.Currency)
//                .HasMaxLength(5)
//                .IsUnicode(false);
//            entity.Property(e => e.FailReturnUrl)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//            entity.Property(e => e.Identifier)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//            entity.Property(e => e.LastUpdatedAt).HasColumnType("datetime");
//            entity.Property(e => e.LastUpdatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.OrderId)
//                .HasMaxLength(100)
//                .IsUnicode(false);
//            entity.Property(e => e.ReferenceId)
//                .HasMaxLength(100)
//                .IsUnicode(false);
//            entity.Property(e => e.Status)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.SuccessReturnUrl)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//            entity.Property(e => e.Trace)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//            entity.Property(e => e.TransactionId)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//        });

//        modelBuilder.Entity<Tran_DailyBalance>(entity =>
//        {
//            entity.HasKey(e => e.DaillyBalanceId).HasName("PK_TrnDailyBalance");

//            entity.ToTable("Tran_DailyBalance");

//            entity.Property(e => e.AccountName)
//                .HasMaxLength(100)
//                .IsUnicode(false);
//            entity.Property(e => e.ClosingBalance).HasColumnType("decimal(18, 4)");
//            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
//            entity.Property(e => e.CreatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.Credit).HasColumnType("decimal(18, 4)");
//            entity.Property(e => e.Debit).HasColumnType("decimal(18, 4)");
//            entity.Property(e => e.LastUpdatedAt).HasColumnType("datetime");
//            entity.Property(e => e.LastUpdatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.OpeningBalance).HasColumnType("decimal(18, 4)");
//        });

//        modelBuilder.Entity<Tran_DayClosingInformation>(entity =>
//        {
//            entity.HasKey(e => e.TrnDayClosingInformationId).HasName("PK_TrnDayClosingInformations");

//            entity.Property(e => e.ClosedDate).HasColumnType("datetime");
//            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
//            entity.Property(e => e.CreatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.LastUpdatedAt).HasColumnType("datetime");
//            entity.Property(e => e.LastUpdatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//        });

//        modelBuilder.Entity<Tran_Journal>(entity =>
//        {
//            entity.HasKey(e => e.JournalID).HasName("PK_TrnJournal");

//            entity.ToTable("Tran_Journal");

//            entity.Property(e => e.AccountName)
//                .HasMaxLength(100)
//                .IsUnicode(false);
//            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
//            entity.Property(e => e.CreatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.Credit)
//                .HasDefaultValue(0.0m)
//                .HasColumnType("decimal(18, 4)");
//            entity.Property(e => e.Debit)
//                .HasDefaultValue(0.0m)
//                .HasColumnType("decimal(18, 4)");
//            entity.Property(e => e.Description)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//            entity.Property(e => e.LastUpdatedAt).HasColumnType("datetime");
//            entity.Property(e => e.LastUpdatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.TransactionDateTime).HasColumnType("datetime");
//            entity.Property(e => e.TransactionId)
//                .HasMaxLength(15)
//                .IsUnicode(false);
//            entity.Property(e => e.TransactionType)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//        });

//        modelBuilder.Entity<Tran_LimitDayUserWise>(entity =>
//        {
//            entity.HasKey(e => e.LimitDayUserWiseId).HasName("PK_Tran_LimitDayUserWises");

//            entity.ToTable("Tran_LimitDayUserWise");

//            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
//            entity.Property(e => e.CreatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.LastTransactionId).HasMaxLength(50);
//            entity.Property(e => e.LastUpdatedAt).HasColumnType("datetime");
//            entity.Property(e => e.LastUpdatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.TransactionAmountPerDay).HasColumnType("decimal(18, 4)");
//            entity.Property(e => e.TransactionType)
//                .HasMaxLength(100)
//                .IsUnicode(false);
//        });

//        modelBuilder.Entity<Tran_LimitMonthUserWise>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK_Tran_LimitMonthUserWises");

//            entity.ToTable("Tran_LimitMonthUserWise");

//            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
//            entity.Property(e => e.CreatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.LastUpdatedAt).HasColumnType("datetime");
//            entity.Property(e => e.LastUpdatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.TransactionAmountPerMonth).HasColumnType("decimal(18, 4)");
//        });

//        modelBuilder.Entity<Tran_LimitPackage>(entity =>
//        {
//            entity.HasNoKey();

//            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
//            entity.Property(e => e.CreatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.LastUpdatedAt).HasColumnType("datetime");
//            entity.Property(e => e.LastUpdatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.LimitPackageId).ValueGeneratedOnAdd();
//            entity.Property(e => e.PackageName)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//        });

//        modelBuilder.Entity<Tran_LimitPackageDetail>(entity =>
//        {
//            entity.HasKey(e => e.LimitPackageDetailId).HasName("PK_Tran_LimitPackageDetails");

//            entity.ToTable("Tran_LimitPackageDetail");

//            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
//            entity.Property(e => e.CreatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.LastUpdatedAt).HasColumnType("datetime");
//            entity.Property(e => e.LastUpdatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.MaxTransactionAmount).HasColumnType("decimal(18, 4)");
//            entity.Property(e => e.MaxTransactionAmountPerDay).HasColumnType("decimal(18, 4)");
//            entity.Property(e => e.MaxTransactionAmountPerMonth).HasColumnType("decimal(18, 4)");
//            entity.Property(e => e.MinTransactionAmount).HasColumnType("decimal(18, 4)");
//        });

//        modelBuilder.Entity<Tran_StackHoldersInformation>(entity =>
//        {
//            entity.HasKey(e => e.StackHoldersInformationId);

//            entity.ToTable("Tran_StackHoldersInformation");

//            entity.Property(e => e.BankCode)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
//            entity.Property(e => e.CreatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.Description)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.EndDate).HasColumnType("datetime");
//            entity.Property(e => e.LastUpdatedAt).HasColumnType("datetime");
//            entity.Property(e => e.LastUpdatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.StackHolderType)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.StakeholderName)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.StartDate).HasColumnType("datetime");
//        });

//        modelBuilder.Entity<Tran_TransactionInformation>(entity =>
//        {
//            entity.HasKey(e => e.TransactionInformationId);

//            entity.ToTable("Tran_TransactionInformation");

//            entity.Property(e => e.BankCode)
//                .HasMaxLength(100)
//                .IsUnicode(false);
//            entity.Property(e => e.BankStan)
//                .HasMaxLength(6)
//                .IsUnicode(false);
//            entity.Property(e => e.Channel)
//                .HasMaxLength(100)
//                .IsUnicode(false);
//            entity.Property(e => e.ChannelTypeName)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//            entity.Property(e => e.ChannelUniqueTransactionId)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//            entity.Property(e => e.Cost).HasColumnType("decimal(18, 4)");
//            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
//            entity.Property(e => e.CreatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.DownstremReferenceId)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//            entity.Property(e => e.FeeAmount).HasColumnType("decimal(18, 4)");
//            entity.Property(e => e.FeePayee)
//                .HasMaxLength(10)
//                .IsUnicode(false);
//            entity.Property(e => e.FeeVatAmount).HasColumnType("decimal(18, 4)");
//            entity.Property(e => e.GatewayRoute)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.Identifier)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//            entity.Property(e => e.LastUpdatedAt).HasColumnType("datetime");
//            entity.Property(e => e.LastUpdatedBy)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.QErrorMessage)
//                .HasMaxLength(300)
//                .IsUnicode(false);
//            entity.Property(e => e.ReferenceTransactionId)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//            entity.Property(e => e.RequestedForSettlementDate).HasColumnType("datetime");
//            entity.Property(e => e.SettlementDate).HasColumnType("datetime");
//            entity.Property(e => e.Trace)
//                .HasMaxLength(300)
//                .IsUnicode(false);
//            entity.Property(e => e.TransactinPurpose)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//            entity.Property(e => e.TransactionAmount).HasColumnType("decimal(18, 4)");
//            entity.Property(e => e.TransactionCompletionDateTime).HasColumnType("datetime");
//            entity.Property(e => e.TransactionCurrencyCode)
//                .HasMaxLength(5)
//                .IsUnicode(false);
//            entity.Property(e => e.TransactionId)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//            entity.Property(e => e.TransactionInnitiationDateTime).HasColumnType("datetime");
//            entity.Property(e => e.TransactionOrderId)
//                .HasMaxLength(200)
//                .IsUnicode(false);
//            entity.Property(e => e.TransactionStatus)
//                .HasMaxLength(100)
//                .IsUnicode(false);
//            entity.Property(e => e.TransactionType)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//        });

//        OnModelCreatingPartial(modelBuilder);
//    }

//    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//}
