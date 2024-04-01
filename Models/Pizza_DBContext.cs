using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PizzaApi.Models
{
    public partial class Pizza_DBContext : DbContext
    {
        public Pizza_DBContext()
        {
        }

        public Pizza_DBContext(DbContextOptions<Pizza_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appuser> Appusers { get; set; } = null!;
        public virtual DbSet<Basket> Baskets { get; set; } = null!;
        public virtual DbSet<CategoryProduct> CategoryProducts { get; set; } = null!;
        public virtual DbSet<ExtrasBasket> ExtrasBaskets { get; set; } = null!;
        public virtual DbSet<Favorite> Favorites { get; set; } = null!;
        public virtual DbSet<Favoritebasket> Favoritebaskets { get; set; } = null!;
        public virtual DbSet<History> Histories { get; set; } = null!;
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<PizzaExtra> PizzaExtras { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductBasket> ProductBaskets { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Pizza_DB;Username=postgres;Password=1234");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appuser>(entity =>
            {
                entity.HasKey(e => e.IdAppuser)
                    .HasName("appuser_pkey");

                entity.ToTable("appuser");

                entity.HasIndex(e => e.Login, "appuser_login_key")
                    .IsUnique();

                entity.HasIndex(e => e.Mail, "appuser_mail_key")
                    .IsUnique();

                entity.HasIndex(e => e.Salt, "appuser_salt_key")
                    .IsUnique();

                entity.Property(e => e.IdAppuser).HasColumnName("id_appuser");

                entity.Property(e => e.Apppassword)
                    .HasMaxLength(50)
                    .HasColumnName("apppassword");

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.Bonus).HasColumnName("bonus");

                entity.Property(e => e.Isexists)
                    .HasColumnName("isexists")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Login)
                    .HasMaxLength(50)
                    .HasColumnName("login");

                entity.Property(e => e.Mail)
                    .HasMaxLength(50)
                    .HasColumnName("mail");

                entity.Property(e => e.RolesId).HasColumnName("roles_id");

                entity.Property(e => e.Salt)
                    .HasMaxLength(50)
                    .HasColumnName("salt");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Basket>(entity =>
            {
                entity.HasKey(e => e.IdBasket)
                    .HasName("basket_pkey");

                entity.ToTable("basket");

                entity.Property(e => e.IdBasket).HasColumnName("id_basket");

                entity.Property(e => e.AppuserId).HasColumnName("appuser_id");

                entity.Property(e => e.Isexists)
                    .HasColumnName("isexists")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Price).HasColumnName("price");
            });

            modelBuilder.Entity<CategoryProduct>(entity =>
                {
                    entity.HasKey(e => e.IdCategoryProduct)
                        .HasName("category_product_pkey");

                    entity.ToTable("category_product");

                    entity.HasIndex(e => e.NameCategory, "category_product_name_category_key")
                        .IsUnique();

                    entity.Property(e => e.IdCategoryProduct).HasColumnName("id_category_product");

                    entity.Property(e => e.Isexists)
                        .HasColumnName("isexists")
                        .HasDefaultValueSql("true");

                    entity.Property(e => e.NameCategory).HasColumnName("name_category");
                });

                modelBuilder.Entity<ExtrasBasket>(entity =>
                {
                    entity.HasKey(e => e.IdExtrasBasket)
                        .HasName("extras_basket_pkey");

                    entity.ToTable("extras_basket");

                    entity.Property(e => e.IdExtrasBasket).HasColumnName("id_extras_basket");

                    entity.Property(e => e.BasketId).HasColumnName("basket_id");

                    entity.Property(e => e.Isexists)
                        .HasColumnName("isexists")
                        .HasDefaultValueSql("true");

                    entity.Property(e => e.PizzaExtrasId).HasColumnName("pizza_extras_id");

                    entity.Property(e => e.Quantity).HasColumnName("quantity");
                });

                modelBuilder.Entity<Favorite>(entity =>
                {
                    entity.HasKey(e => e.IdFavorite)
                        .HasName("favorite_pkey");

                    entity.ToTable("favorite");

                    entity.Property(e => e.IdFavorite).HasColumnName("id_favorite");

                    entity.Property(e => e.AppuserId).HasColumnName("appuser_id");

                    entity.Property(e => e.Isexists)
                        .HasColumnName("isexists")
                        .HasDefaultValueSql("true");

                    entity.Property(e => e.ProductId).HasColumnName("product_id");
                });

                modelBuilder.Entity<Favoritebasket>(entity =>
                {
                    entity.HasKey(e => e.IdFavoritebasket)
                        .HasName("favoritebasket_pkey");

                    entity.ToTable("favoritebasket");

                    entity.Property(e => e.IdFavoritebasket).HasColumnName("id_favoritebasket");

                    entity.Property(e => e.BasketId).HasColumnName("basket_id");

                    entity.Property(e => e.Isexists)
                        .HasColumnName("isexists")
                        .HasDefaultValueSql("true");

                    entity.Property(e => e.ProductId).HasColumnName("product_id");
                });

                modelBuilder.Entity<History>(entity =>
                {
                    entity.HasKey(e => e.IdHistory)
                        .HasName("history_pkey");

                    entity.ToTable("history");

                    entity.Property(e => e.IdHistory).HasColumnName("id_history");

                    entity.Property(e => e.AppuserId).HasColumnName("appuser_id");

                    entity.Property(e => e.BasketId).HasColumnName("basket_id");

                    entity.Property(e => e.DataBuy).HasColumnName("data_buy");

                    entity.Property(e => e.DataHistory)
                        .HasColumnName("data_history")
                        .HasDefaultValueSql("CURRENT_DATE");

                    entity.Property(e => e.Isexists)
                        .HasColumnName("isexists")
                        .HasDefaultValueSql("true");

                    entity.Property(e => e.PriceHistory).HasColumnName("price_history");
                });

                modelBuilder.Entity<Image>(entity =>
                {
                    entity.HasKey(e => e.IdImage)
                        .HasName("image_pkey");

                    entity.ToTable("image");

                    entity.Property(e => e.IdImage).HasColumnName("id_image");

                    entity.Property(e => e.Isexists)
                        .HasColumnName("isexists")
                        .HasDefaultValueSql("true");

                    entity.Property(e => e.Linkimage).HasColumnName("linkimage");
                });

                modelBuilder.Entity<PizzaExtra>(entity =>
                {
                    entity.HasKey(e => e.IdPizzaExtras)
                        .HasName("pizza_extras_pkey");

                    entity.ToTable("pizza_extras");

                    entity.HasIndex(e => e.NameExtras, "pizza_extras_name_extras_key")
                        .IsUnique();

                    entity.Property(e => e.IdPizzaExtras).HasColumnName("id_pizza_extras");

                    entity.Property(e => e.ImageLink).HasColumnName("image_link");

                    entity.Property(e => e.Isexists)
                        .HasColumnName("isexists")
                        .HasDefaultValueSql("true");

                    entity.Property(e => e.NameExtras)
                        .HasMaxLength(255)
                        .HasColumnName("name_extras");

                    entity.Property(e => e.Price).HasColumnName("price");

                    entity.Property(e => e.Quantity).HasColumnName("quantity");
                });

                modelBuilder.Entity<Product>(entity =>
                {
                    entity.HasKey(e => e.IdProduct)
                        .HasName("product_pkey");

                    entity.ToTable("product");

                    entity.HasIndex(e => e.NameProduct, "product_name_product_key")
                        .IsUnique();

                    entity.Property(e => e.IdProduct).HasColumnName("id_product");

                    entity.Property(e => e.CategoryProductId).HasColumnName("category_product_id");

                    entity.Property(e => e.DescriptionProduct).HasColumnName("description_product");

                    entity.Property(e => e.ImageId).HasColumnName("image_id");

                    entity.Property(e => e.Isexists)
                        .HasColumnName("isexists")
                        .HasDefaultValueSql("true");

                    entity.Property(e => e.MiniDescriptionProduct)
                        .HasMaxLength(255)
                        .HasColumnName("mini_description_product");

                    entity.Property(e => e.NameProduct)
                        .HasMaxLength(100)
                        .HasColumnName("name_product");

                    entity.Property(e => e.Price).HasColumnName("price");

                    entity.Property(e => e.Quantity).HasColumnName("quantity");

                    entity.Property(e => e.Typef).HasColumnName("typef");
                });

                modelBuilder.Entity<ProductBasket>(entity =>
                {
                    entity.HasKey(e => e.IdProductCart)
                        .HasName("product_basket_pkey");

                    entity.ToTable("product_basket");

                    entity.Property(e => e.IdProductCart).HasColumnName("id_product_cart");

                    entity.Property(e => e.BasketId).HasColumnName("basket_id");

                    entity.Property(e => e.Isexists)
                        .HasColumnName("isexists")
                        .HasDefaultValueSql("true");
                });

                modelBuilder.Entity<Role>(entity =>
                {
                    entity.HasKey(e => e.IdRoles)
                        .HasName("roles_pkey");

                    entity.ToTable("roles");

                    entity.Property(e => e.IdRoles).HasColumnName("id_roles");

                    entity.Property(e => e.Isexists)
                        .HasColumnName("isexists")
                        .HasDefaultValueSql("true");

                    entity.Property(e => e.NameRoles).HasColumnName("name_roles");
                });

                OnModelCreatingPartial(modelBuilder);
            }
                

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
