using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Compubuilt.Models
{
    public partial class compubuiltContext : DbContext
    {
        public compubuiltContext()
        {
        }

        public compubuiltContext(DbContextOptions<compubuiltContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AddressType> AddressTypes { get; set; } = null!;
        public virtual DbSet<Agreement> Agreements { get; set; } = null!;
        public virtual DbSet<AgreementType> AgreementTypes { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; } = null!;
        public virtual DbSet<Delivery> Deliveries { get; set; } = null!;
        public virtual DbSet<DeliveryStatusType> DeliveryStatusTypes { get; set; } = null!;
        public virtual DbSet<DeliveryType> DeliveryTypes { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderStatusType> OrderStatusTypes { get; set; } = null!;
        public virtual DbSet<ParameterType> ParameterTypes { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<PaymentStatusType> PaymentStatusTypes { get; set; } = null!;
        public virtual DbSet<PaymentType> PaymentTypes { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public virtual DbSet<ProductImage> ProductImages { get; set; } = null!;
        public virtual DbSet<ProductParameter> ProductParameters { get; set; } = null!;
        public virtual DbSet<ProductReview> ProductReviews { get; set; } = null!;
        public virtual DbSet<PromotionalCode> PromotionalCodes { get; set; } = null!;
        public virtual DbSet<PromotionalCodeType> PromotionalCodeTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("Server=BARTEK-WINDOWS\\SQLEXPRESS;Database=compubuilt;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddressType>(entity =>
            {
                entity.ToTable("AddressType", "Customer");

                entity.Property(e => e.AddressTypeName).HasMaxLength(50);
            });

            modelBuilder.Entity<Agreement>(entity =>
            {
                entity.ToTable("Agreements", "Customer");

                entity.HasOne(d => d.AgreementType)
                    .WithMany(p => p.Agreements)
                    .HasForeignKey(d => d.AgreementTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Agreements_AgreementTypes");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Agreements)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Agreements_Customers");
            });

            modelBuilder.Entity<AgreementType>(entity =>
            {
                entity.ToTable("AgreementTypes", "Customer");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Text)
                    .HasMaxLength(4000)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customers", "Customer");

                entity.Property(e => e.AzureAdsid)
                    .HasMaxLength(85)
                    .HasColumnName("AzureADSID");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(150)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmailAddress).HasMaxLength(150);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastModifiedBy)
                    .HasMaxLength(150)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.LastModifiedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            });

            modelBuilder.Entity<CustomerAddress>(entity =>
            {
                entity.ToTable("CustomerAddresses", "Customer");

                entity.Property(e => e.CityName).HasMaxLength(100);

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.StreetName).HasMaxLength(150);

                entity.Property(e => e.StreetNumber).HasMaxLength(10);

                entity.HasOne(d => d.AddressType)
                    .WithMany(p => p.CustomerAddresses)
                    .HasForeignKey(d => d.AddressTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerAddresses_AddressType");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerAddresses)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerAddresses_Customers");
            });

            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.ToTable("Deliveries", "Order");

                entity.HasOne(d => d.DeliveryStatusType)
                    .WithMany(p => p.Deliveries)
                    .HasForeignKey(d => d.DeliveryStatusTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Deliveries_DeliveryStatusTypes");

                entity.HasOne(d => d.DeliveryType)
                    .WithMany(p => p.Deliveries)
                    .HasForeignKey(d => d.DeliveryTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Deliveries_DeliveryTypes");
            });

            modelBuilder.Entity<DeliveryStatusType>(entity =>
            {
                entity.HasKey(e => e.DeliverStatusTypeId);

                entity.ToTable("DeliveryStatusTypes", "Order");

                entity.Property(e => e.DeliveryStatusName).HasMaxLength(50);
            });

            modelBuilder.Entity<DeliveryType>(entity =>
            {
                entity.ToTable("DeliveryTypes", "Order");

                entity.Property(e => e.DeliveryTypeName).HasMaxLength(50);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders", "Order");

                entity.Property(e => e.OrderNumber).HasMaxLength(50);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_CustomerAddresses");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Customers");

                entity.HasOne(d => d.Delivery)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.DeliveryId)
                    .HasConstraintName("FK_Orders_Deliveries");

                entity.HasOne(d => d.OrderStatusType)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderStatusTypeId)
                    .HasConstraintName("FK_Orders_OrderStatusTypes");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PaymentId)
                    .HasConstraintName("FK_Orders_Payments");

                entity.HasOne(d => d.PromotionalCode)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PromotionalCodeId)
                    .HasConstraintName("FK_Orders_PromotionalCodeTypes");
            });

            modelBuilder.Entity<OrderStatusType>(entity =>
            {
                entity.ToTable("OrderStatusTypes", "Order");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<ParameterType>(entity =>
            {
                entity.ToTable("ParameterType", "Product");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payments", "Payment");

                entity.HasOne(d => d.PaymentStatusType)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.PaymentStatusTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payments_PaymentStatusTypes");

                entity.HasOne(d => d.PaymentType)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.PaymentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payments_PaymentTypes");
            });

            modelBuilder.Entity<PaymentStatusType>(entity =>
            {
                entity.ToTable("PaymentStatusTypes", "Payment");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<PaymentType>(entity =>
            {
                entity.ToTable("PaymentTypes", "Payment");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products", "Product");

                entity.Property(e => e.Description).HasMaxLength(4000);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.ProductCategory)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_ProductCategories");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("ProductCategories", "Product");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.ToTable("ProductImages", "Product");

                entity.Property(e => e.ImageName).HasMaxLength(150);

                entity.Property(e => e.Url)
                    .HasMaxLength(2083)
                    .HasColumnName("URL");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductImages)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductImages_Products");
            });

            modelBuilder.Entity<ProductParameter>(entity =>
            {
                entity.ToTable("ProductParameter", "Product");

                entity.Property(e => e.Value).HasMaxLength(50);

                entity.HasOne(d => d.ParameterType)
                    .WithMany(p => p.ProductParameters)
                    .HasForeignKey(d => d.ParameterTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductParameter_ParameterType");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductParameters)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductParameter_Products");
            });

            modelBuilder.Entity<ProductReview>(entity =>
            {
                entity.ToTable("ProductReviews", "Product");

                entity.Property(e => e.ReviewText).HasMaxLength(2000);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ProductReviews)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductReviews_Customers");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductReviews)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductReviews_Products");
            });

            modelBuilder.Entity<PromotionalCode>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PromotionalCodes", "Order");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.PromotionalCodeType)
                    .WithMany()
                    .HasForeignKey(d => d.PromotionalCodeTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PromotionalCodes_PromotionalCodeTypes");
            });

            modelBuilder.Entity<PromotionalCodeType>(entity =>
            {
                entity.ToTable("PromotionalCodeTypes", "Order");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
