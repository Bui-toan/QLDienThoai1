using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace QLDienThoai.Models;

public partial class QldienThoaiContext : IdentityDbContext<AppUser>
{
	public QldienThoaiContext()
	{
	}

	public QldienThoaiContext(DbContextOptions<QldienThoaiContext> options)
		: base(options)
	{
	}

	public virtual DbSet<Brands> Brands { get; set; }

	public virtual DbSet<Categories> Categories { get; set; }
	public virtual DbSet<Contact> Contacts { get; set; }

	public virtual DbSet<Order> Orders { get; set; }

	public virtual DbSet<OrderDetails> OrderDetails { get; set; }

	public virtual DbSet<SanPham> SanPhams { get; set; }

	public virtual DbSet<AppUser> Users { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
		=> optionsBuilder.UseSqlServer("Data Source=DESKTOP-HO543EC\\TOAN1;Initial Catalog=QLDienThoai11;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
		{
			entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
		});

		modelBuilder.Entity<IdentityUserRole<string>>(entity =>
		{
			entity.HasKey(e => new { e.UserId, e.RoleId });
		});

		modelBuilder.Entity<IdentityUserToken<string>>(entity =>
		{
			entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
		});

		modelBuilder.Entity<Brands>(entity =>
		{
			entity.HasKey(e => e.BrandId).HasName("PK__Brands__DAD4F05E90ADE922");

			entity.Property(e => e.BrandId).ValueGeneratedNever();
			entity.Property(e => e.Description).HasMaxLength(255);
			entity.Property(e => e.Name).HasMaxLength(100);
			entity.Property(e => e.Slug).HasMaxLength(100);
			entity.Property(e => e.Status).HasMaxLength(50);
		});

		modelBuilder.Entity<Categories>(entity =>
		{
			entity.HasKey(e => e.CategoriesId).HasName("PK__Categori__EFF907B09A8AD06F");

			entity.Property(e => e.CategoriesId)
				.ValueGeneratedNever()
				.HasColumnName("CategoriesID");
			entity.Property(e => e.Description).HasMaxLength(255);
			entity.Property(e => e.Name).HasMaxLength(100);
			entity.Property(e => e.Slug).HasMaxLength(100);
			entity.Property(e => e.Status).HasMaxLength(50);
		});
		modelBuilder.Entity<Contact>(entity =>
		{
			entity.HasKey(e => e.Name).HasName("PK__Contact__737584F73D4DD07B");
			entity.Property(e => e.Map).HasMaxLength(400);
			entity.Property(e => e.Email).HasMaxLength(400);
			entity.Property(e => e.Phone).HasMaxLength(400);
			entity.Property(e => e.Description).HasMaxLength(400);
			entity.Property(e => e.LogoImg).HasMaxLength(400);
		});

		modelBuilder.Entity<Order>(entity =>
		{
			entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BCF7EAABD66");

			entity.Property(e => e.OrderId).ValueGeneratedNever();
			entity.Property(e => e.CreateDate).HasColumnType("datetime");
		});

		modelBuilder.Entity<OrderDetails>(entity =>
		{
			entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__D3B9D36C34CF5106");

			entity.ToTable("OrderDetail");

			entity.Property(e => e.OrderDetailId).ValueGeneratedNever();
			entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

			entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
				.HasForeignKey(d => d.OrderDetailId)
				.HasConstraintName("FK__OrderDeta__Order__6A30C649");

			entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
				.HasForeignKey(d => d.ProductId)
				.HasConstraintName("FK__OrderDeta__Produ__6B24EA82");
		});

		modelBuilder.Entity<SanPham>(entity =>
		{
			entity.HasKey(e => e.IdSanPham).HasName("PK__SanPham__EC21BD235D20C44B");

			entity.ToTable("SanPham");

			entity.Property(e => e.IdSanPham)
				.ValueGeneratedNever()
				.HasColumnName("IdSanPham");
			entity.Property(e => e.CategoriesId).HasColumnName("CategoriesID");
			entity.Property(e => e.Description).HasMaxLength(255);
			entity.Property(e => e.Images).HasMaxLength(255);
			entity.Property(e => e.Name).HasMaxLength(100);
			entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
			entity.Property(e => e.Slug).HasMaxLength(100);

			entity.HasOne(d => d.Brand).WithMany(p => p.SanPhams)
				.HasForeignKey(d => d.BrandId)
				.HasConstraintName("FK__SanPham__BrandId__619B8048");

			entity.HasOne(d => d.Categories).WithMany(p => p.SanPhams)
				.HasForeignKey(d => d.CategoriesId)
				.HasConstraintName("FK__SanPham__Categor__60A75C0F");
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
