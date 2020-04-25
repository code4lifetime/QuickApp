using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QuickApp.SQLDAL.Models
{
    public partial class QuickappContext 
    {
        public QuickappContext()
        {
        }

        public QuickappContext(DbContextOptions<QuickappContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppCustomer> AppCustomers { get; set; }
        public virtual DbSet<AppOrder> AppOrders { get; set; }
        public virtual DbSet<AppOrderDetail> AppOrderDetails { get; set; }
        public virtual DbSet<AppProduct> AppProducts { get; set; }
        public virtual DbSet<AppProductCategory> AppProductCategories { get; set; }
        //public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        //public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        //public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        //public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        //public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        //public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        //public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<Email> Emails { get; set; }
        public virtual DbSet<EmailAttachment> EmailAttachments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<AppCustomer>(entity =>
            {
                entity.HasIndex(e => e.Name);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<AppOrder>(entity =>
            {
                entity.HasIndex(e => e.CashierId);

                entity.HasIndex(e => e.CustomerId);

                entity.Property(e => e.Comments).HasMaxLength(500);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.Cashier)
                    .WithMany(p => p.AppOrders)
                    .HasForeignKey(d => d.CashierId);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.AppOrders)
                    .HasForeignKey(d => d.CustomerId);
            });

            modelBuilder.Entity<AppOrderDetail>(entity =>
            {
                entity.HasIndex(e => e.OrderId);

                entity.HasIndex(e => e.ProductId);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.AppOrderDetails)
                    .HasForeignKey(d => d.OrderId);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.AppOrderDetails)
                    .HasForeignKey(d => d.ProductId);
            });

            modelBuilder.Entity<AppProduct>(entity =>
            {
                entity.HasIndex(e => e.Name);

                entity.HasIndex(e => e.ParentId);

                entity.HasIndex(e => e.ProductCategoryId);

                entity.Property(e => e.BuyingPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Icon)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SellingPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId);

                entity.HasOne(d => d.ProductCategory)
                    .WithMany(p => p.AppProducts)
                    .HasForeignKey(d => d.ProductCategoryId);
            });

            modelBuilder.Entity<AppProductCategory>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            //modelBuilder.Entity<AspNetRole>(entity =>
            //{
            //    entity.HasIndex(e => e.NormalizedName)
            //        .HasName("RoleNameIndex")
            //        .IsUnique()
            //        .HasFilter("([NormalizedName] IS NOT NULL)");

            //    entity.Property(e => e.Id).ValueGeneratedNever();

            //    entity.Property(e => e.Name).HasMaxLength(256);

            //    entity.Property(e => e.NormalizedName).HasMaxLength(256);
            //});

            //modelBuilder.Entity<AspNetRoleClaim>(entity =>
            //{
            //    entity.HasIndex(e => e.RoleId);

            //    entity.Property(e => e.RoleId).IsRequired();

            //    entity.HasOne(d => d.Role)
            //        .WithMany(p => p.AspNetRoleClaims)
            //        .HasForeignKey(d => d.RoleId);
            //});

            //modelBuilder.Entity<AspNetUser>(entity =>
            //{
            //    entity.HasIndex(e => e.NormalizedEmail)
            //        .HasName("EmailIndex");

            //    entity.HasIndex(e => e.NormalizedUserName)
            //        .HasName("UserNameIndex")
            //        .IsUnique()
            //        .HasFilter("([NormalizedUserName] IS NOT NULL)");

            //    entity.Property(e => e.Id).ValueGeneratedNever();

            //    entity.Property(e => e.Email).HasMaxLength(256);

            //    entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

            //    entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

            //    entity.Property(e => e.UserName).HasMaxLength(256);
            //});

            //modelBuilder.Entity<AspNetUserClaim>(entity =>
            //{
            //    entity.HasIndex(e => e.UserId);

            //    entity.Property(e => e.UserId).IsRequired();

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserClaims)
            //        .HasForeignKey(d => d.UserId);
            //});

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Email>(entity =>
            {
                entity.ToTable("Email");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmailContent).IsRequired();

                entity.Property(e => e.FromEmailId).IsRequired();

                entity.Property(e => e.FromName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Subject).IsRequired();

                entity.Property(e => e.ToEmailId).IsRequired();
            });

            modelBuilder.Entity<EmailAttachment>(entity =>
            {
                entity.ToTable("EmailAttachment");

                entity.Property(e => e.AttachmentName).IsRequired();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }




        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}