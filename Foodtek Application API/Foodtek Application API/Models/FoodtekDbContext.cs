using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Foodtek_Application_API.Models;

public partial class FoodtekDbContext : DbContext
{
    public FoodtekDbContext()
    {
    }

    public FoodtekDbContext(DbContextOptions<FoodtekDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<DiscountOffer> DiscountOffers { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<ItemOption> ItemOptions { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-N91Q09L\\SQLEXPRESS;Initial Catalog=FoodtekDb;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Address__3214EC07978BA197");

            entity.ToTable("Address");

            entity.Property(e => e.AdditionalDetails)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ApartmentNumber).HasMaxLength(20);
            entity.Property(e => e.BuildingName).HasMaxLength(100);
            entity.Property(e => e.BuildingNumber).HasMaxLength(20);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Floor)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Latitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.Province)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Region)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsersId).HasColumnName("Users_id");

            entity.HasOne(d => d.Users).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.UsersId)
                .HasConstraintName("FK__Address__Users_i__08B54D69");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cart__3214EC07323032F2");

            entity.ToTable("Cart");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ItemId).HasColumnName("Item_id");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsersId).HasColumnName("Users_id");

            entity.HasOne(d => d.Item).WithMany(p => p.Carts)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK__Cart__Item_id__55F4C372");

            entity.HasOne(d => d.Users).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UsersId)
                .HasConstraintName("FK__Cart__Users_id__55009F39");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC07E5E4D749");

            entity.ToTable("Category");

            entity.HasIndex(e => e.NameEn, "UQ__Category__EE1C774FEE8E3674").IsUnique();

            entity.HasIndex(e => e.NameAr, "UQ__Category__EE1CD24C82DC8DF3").IsUnique();

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.NameAr)
                .HasMaxLength(50)
                .HasColumnName("NameAR");
            entity.Property(e => e.NameEn)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NameEN");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsersId).HasColumnName("Users_id");

            entity.HasOne(d => d.Users).WithMany(p => p.Categories)
                .HasForeignKey(d => d.UsersId)
                .HasConstraintName("FK__Category__Users___693CA210");

            entity.HasMany(d => d.DiscountOffers).WithMany(p => p.Categories)
                .UsingEntity<Dictionary<string, object>>(
                    "CategoryDiscount",
                    r => r.HasOne<DiscountOffer>().WithMany()
                        .HasForeignKey("DiscountOffersId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Category___Disco__690797E6"),
                    l => l.HasOne<Category>().WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Category___Categ__681373AD"),
                    j =>
                    {
                        j.HasKey("CategoryId", "DiscountOffersId").HasName("PK__Category__3DD9E657A362AA16");
                        j.ToTable("Category_Discount");
                        j.IndexerProperty<int>("CategoryId").HasColumnName("Category_id");
                        j.IndexerProperty<int>("DiscountOffersId").HasColumnName("Discount&offers_id");
                    });
        });

        modelBuilder.Entity<DiscountOffer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Discount__3214EC07DD33D874");

            entity.ToTable("Discount&offers");

            entity.HasIndex(e => e.TitleAr, "UQ__Discount__754270FF0EE9D8EE").IsUnique();

            entity.HasIndex(e => e.TitleEn, "UQ__Discount__754390764D230C18").IsUnique();

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DescriptionAr)
                .HasMaxLength(255)
                .HasColumnName("DescriptionAR");
            entity.Property(e => e.DescriptionEn)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("DescriptionEN");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.Image).IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ItemId).HasColumnName("Item_id");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.TitleAr)
                .HasMaxLength(50)
                .HasColumnName("TitleAR");
            entity.Property(e => e.TitleEn)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TitleEN");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsersId).HasColumnName("Users_id");

            entity.HasOne(d => d.Item).WithMany(p => p.DiscountOffers)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK__Discount&__Item___22751F6C");

            entity.HasOne(d => d.UsersNavigation).WithMany(p => p.DiscountOffersNavigation)
                .HasForeignKey(d => d.UsersId)
                .HasConstraintName("FK__Discount&__Users__2180FB33");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Favorite__3214EC0771048E04");

            entity.ToTable("Favorite");

            entity.HasIndex(e => new { e.UsersId, e.ItemId }, "UQ__Favorite__38906D7EFBB4ED0A").IsUnique();

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ItemId).HasColumnName("Item_id");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsersId).HasColumnName("Users_id");

            entity.HasOne(d => d.Item).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Favorite__Item_i__489AC854");

            entity.HasOne(d => d.Users).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.UsersId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Favorite__Users___47A6A41B");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Item__3214EC076D2CFDA2");

            entity.ToTable("Item");

            entity.HasIndex(e => e.NameEn, "UQ__Item__EE1C774F61461035").IsUnique();

            entity.HasIndex(e => e.NameAr, "UQ__Item__EE1CD24C7507AFCC").IsUnique();

            entity.Property(e => e.CategoryId).HasColumnName("Category_id");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DescriptionAr)
                .HasMaxLength(255)
                .HasColumnName("DescriptionAR");
            entity.Property(e => e.DescriptionEn)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("DescriptionEN");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.NameAr)
                .HasMaxLength(50)
                .HasColumnName("NameAR");
            entity.Property(e => e.NameEn)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NameEN");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsersId).HasColumnName("Users_id");

            entity.HasOne(d => d.Category).WithMany(p => p.Items)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Item__Category_i__778AC167");

            entity.HasOne(d => d.UsersNavigation).WithMany(p => p.ItemsNavigation)
                .HasForeignKey(d => d.UsersId)
                .HasConstraintName("FK__Item__Users_id__76969D2E");
        });

        modelBuilder.Entity<ItemOption>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ItemOpti__3214EC07039352E4");

            entity.ToTable("ItemOption");

            entity.HasIndex(e => e.NameEn, "UQ__ItemOpti__EE1C774FF03677EA").IsUnique();

            entity.HasIndex(e => e.NameAr, "UQ__ItemOpti__EE1CD24C0BF0CE4D").IsUnique();

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ItemId).HasColumnName("Item_id");
            entity.Property(e => e.NameAr)
                .HasMaxLength(50)
                .HasColumnName("NameAR");
            entity.Property(e => e.NameEn)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NameEN");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsersId).HasColumnName("Users_id");

            entity.HasOne(d => d.Item).WithMany(p => p.ItemOptions)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK__ItemOptio__Item___02FC7413");

            entity.HasOne(d => d.Users).WithMany(p => p.ItemOptions)
                .HasForeignKey(d => d.UsersId)
                .HasConstraintName("FK__ItemOptio__Users__02084FDA");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3214EC07C6CDF7FC");

            entity.Property(e => e.Content)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DiscountOffersId).HasColumnName("Discount&offers_id");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsRead).HasDefaultValue(true);
            entity.Property(e => e.NotificationType)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsersId).HasColumnName("Users_id");

            entity.HasOne(d => d.DiscountOffers).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.DiscountOffersId)
                .HasConstraintName("FK__Notificat__Disco__2A164134");

            entity.HasOne(d => d.Users).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UsersId)
                .HasConstraintName("FK__Notificat__Users__29221CFB");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__3214EC0725424BAA");

            entity.ToTable("Order");

            entity.Property(e => e.AddressId).HasColumnName("Address_id");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsersId).HasColumnName("Users_id");

            entity.HasOne(d => d.Address).WithMany(p => p.Orders)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK__Order__Address_i__10566F31");

            entity.HasOne(d => d.Users).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UsersId)
                .HasConstraintName("FK__Order__Users_id__0F624AF8");

            entity.HasMany(d => d.Items).WithMany(p => p.Orders)
                .UsingEntity<Dictionary<string, object>>(
                    "OrderItem",
                    r => r.HasOne<Item>().WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Order_Ite__Item___65370702"),
                    l => l.HasOne<Order>().WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Order_Ite__Order__6442E2C9"),
                    j =>
                    {
                        j.HasKey("OrderId", "ItemId").HasName("PK__Order_It__2204C469BD1AA3A3");
                        j.ToTable("Order_Item");
                        j.IndexerProperty<int>("OrderId").HasColumnName("Order_id");
                        j.IndexerProperty<int>("ItemId").HasColumnName("Item_id");
                    });
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payment__3214EC07071B4B93");

            entity.ToTable("Payment");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.OrderId).HasColumnName("Order_id");
            entity.Property(e => e.PaymentMethodsId).HasColumnName("PaymentMethods_id");
            entity.Property(e => e.PaymentStatues)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsersId).HasColumnName("Users_id");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Payment__Order_i__40058253");

            entity.HasOne(d => d.PaymentMethods).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PaymentMethodsId)
                .HasConstraintName("FK__Payment__Payment__40F9A68C");

            entity.HasOne(d => d.Users).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UsersId)
                .HasConstraintName("FK__Payment__Users_i__3F115E1A");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PaymentM__3214EC07A0B58E5B");

            entity.HasIndex(e => e.Name, "UQ__PaymentM__737584F6EE085B0A").IsUnique();

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PaymentType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reviews__3214EC079C5CF3FE");

            entity.Property(e => e.Comment)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ItemId).HasColumnName("Item_id");
            entity.Property(e => e.OrderId).HasColumnName("Order_id");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsersId).HasColumnName("Users_id");

            entity.HasOne(d => d.Item).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK__Reviews__Item_id__32AB8735");

            entity.HasOne(d => d.Order).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Reviews__Order_i__31B762FC");

            entity.HasOne(d => d.Users).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UsersId)
                .HasConstraintName("FK__Reviews__Users_i__30C33EC3");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC071108D8E6");

            entity.ToTable("Role");

            entity.HasIndex(e => e.NameEn, "UQ__Role__EE1C774FAC944D5E").IsUnique();

            entity.HasIndex(e => e.NameAr, "UQ__Role__EE1CD24CDA7D3ABA").IsUnique();

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.NameAr)
                .HasMaxLength(50)
                .HasColumnName("NameAR");
            entity.Property(e => e.NameEn)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NameEN");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ticket__3213E83FA3236B11");

            entity.ToTable("Ticket");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActionType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("action_type");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creation_date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.RefundAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("refund_amount");
            entity.Property(e => e.RefundExpirationDate).HasColumnName("refund_expiration_date");
            entity.Property(e => e.ResolvedAt)
                .HasColumnType("datetime")
                .HasColumnName("resolved_at");
            entity.Property(e => e.ResolvedByAdminId).HasColumnName("resolved_by_admin_id");
            entity.Property(e => e.Response).HasColumnName("response");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("type");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Client).WithMany(p => p.TicketClients)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK__Ticket__client_i__4E53A1AA");

            entity.HasOne(d => d.ResolvedByAdmin).WithMany(p => p.TicketResolvedByAdmins)
                .HasForeignKey(d => d.ResolvedByAdminId)
                .HasConstraintName("FK__Ticket__resolved__4F47C5E3");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC078332916D");

            entity.HasIndex(e => e.PhoneNumber, "UQ__Users__85FB4E38E23801C7").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105342B19824A").IsUnique();

            entity.Property(e => e.BirthOfDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsLogedIn).HasDefaultValue(false);
            entity.Property(e => e.IsVerified).HasDefaultValue(false);
            entity.Property(e => e.JoinDate).HasColumnType("datetime");
            entity.Property(e => e.LastLoginTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Otpcode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("OTPCode");
            entity.Property(e => e.Otpexpiry)
                .HasColumnType("datetime")
                .HasColumnName("OTPExpiry");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ProfileImage)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("Role_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__Role_id__5EBF139D");

            entity.HasMany(d => d.CategoriesNavigation).WithMany(p => p.UsersNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "ClientCategory",
                    r => r.HasOne<Category>().WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Client_Ca__Categ__59C55456"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Client_Ca__Users__58D1301D"),
                    j =>
                    {
                        j.HasKey("UsersId", "CategoryId").HasName("PK__Client_C__9DB005565959DA42");
                        j.ToTable("Client_Category");
                        j.IndexerProperty<int>("UsersId").HasColumnName("Users_id");
                        j.IndexerProperty<int>("CategoryId").HasColumnName("Category_id");
                    });

            entity.HasMany(d => d.DiscountOffers).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "ClientDiscount",
                    r => r.HasOne<DiscountOffer>().WithMany()
                        .HasForeignKey("DiscountOffersId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Client_Di__Disco__6166761E"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Client_Di__Users__607251E5"),
                    j =>
                    {
                        j.HasKey("UsersId", "DiscountOffersId").HasName("PK__Client_D__BB004A24CF310DDC");
                        j.ToTable("Client_Discount");
                        j.IndexerProperty<int>("UsersId").HasColumnName("Users_id");
                        j.IndexerProperty<int>("DiscountOffersId").HasColumnName("Discount&offers_id");
                    });

            entity.HasMany(d => d.Items).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "ClientItem",
                    r => r.HasOne<Item>().WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Client_It__Item___5D95E53A"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Client_It__Users__5CA1C101"),
                    j =>
                    {
                        j.HasKey("UsersId", "ItemId").HasName("PK__Client_I__38906D7FC0336B84");
                        j.ToTable("Client_Item");
                        j.IndexerProperty<int>("UsersId").HasColumnName("Users_id");
                        j.IndexerProperty<int>("ItemId").HasColumnName("Item_id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
