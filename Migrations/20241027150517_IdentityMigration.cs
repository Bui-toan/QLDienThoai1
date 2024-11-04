using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLDienThoai.Migrations
{
	/// <inheritdoc />
	public partial class IdentityMigration : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "AspNetRoles",
				columns: table => new
				{
					Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
					Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetRoles", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUsers",
				columns: table => new
				{
					Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
					Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
					UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
					PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
					SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
					ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
					PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
					PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
					TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
					LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
					LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
					AccessFailedCount = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUsers", x => x.Id);
				});

			//migrationBuilder.CreateTable(
			//    name: "Brands",
			//    columns: table => new
			//    {
			//        BrandId = table.Column<int>(type: "int", nullable: false),
			//        Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
			//        Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
			//        Slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
			//        Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
			//    },
			//    constraints: table =>
			//    {
			//        table.PrimaryKey("PK__Brands__DAD4F05E90ADE922", x => x.BrandId);
			//    });

			//migrationBuilder.CreateTable(
			//    name: "Categories",
			//    columns: table => new
			//    {
			//        CategoriesID = table.Column<int>(type: "int", nullable: false),
			//        Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
			//        Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
			//        Slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
			//        Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
			//    },
			//    constraints: table =>
			//    {
			//        table.PrimaryKey("PK__Categori__EFF907B09A8AD06F", x => x.CategoriesID);
			//    });

			//migrationBuilder.CreateTable(
			//    name: "Roles",
			//    columns: table => new
			//    {
			//        RoleId = table.Column<int>(type: "int", nullable: false),
			//        Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
			//        NormalizedName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
			//        ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
			//    },
			//    constraints: table =>
			//    {
			//        table.PrimaryKey("PK__Roles__8AFACE1AC644E28F", x => x.RoleId);
			//    });

			//migrationBuilder.CreateTable(
			//    name: "Users",
			//    columns: table => new
			//    {
			//        UserId = table.Column<int>(type: "int", nullable: false),
			//        Occupation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
			//        UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
			//        NormalizedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
			//        Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
			//        NormalizedEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
			//        EmailConfirmed = table.Column<bool>(type: "bit", nullable: true),
			//        PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
			//        SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
			//        PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
			//    },
			//    constraints: table =>
			//    {
			//        table.PrimaryKey("PK__Users__1788CC4CED85607A", x => x.UserId);
			//    });

			migrationBuilder.CreateTable(
				name: "AspNetRoleClaims",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
					ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
					ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
					table.ForeignKey(
						name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
						column: x => x.RoleId,
						principalTable: "AspNetRoles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserClaims",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
					ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
					ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
					table.ForeignKey(
						name: "FK_AspNetUserClaims_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserLogins",
				columns: table => new
				{
					LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
					ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
					ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
					UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
					table.ForeignKey(
						name: "FK_AspNetUserLogins_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserRoles",
				columns: table => new
				{
					UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
					RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
					table.ForeignKey(
						name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
						column: x => x.RoleId,
						principalTable: "AspNetRoles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_AspNetUserRoles_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserTokens",
				columns: table => new
				{
					UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
					LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
					Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
					Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
					table.ForeignKey(
						name: "FK_AspNetUserTokens_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			//migrationBuilder.CreateTable(
			//    name: "SanPham",
			//    columns: table => new
			//    {
			//        ID_Ban_Pham = table.Column<int>(type: "int", nullable: false),
			//        Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
			//        Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
			//        Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
			//        BrandId = table.Column<int>(type: "int", nullable: false),
			//        Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
			//        Images = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
			//        CategoriesID = table.Column<int>(type: "int", nullable: false)
			//    },
			//    constraints: table =>
			//    {
			//        table.PrimaryKey("PK__SanPham__EC21BD235D20C44B", x => x.ID_Ban_Pham);
			//        table.ForeignKey(
			//            name: "FK__SanPham__BrandId__619B8048",
			//            column: x => x.BrandId,
			//            principalTable: "Brands",
			//            principalColumn: "BrandId",
			//            onDelete: ReferentialAction.Cascade);
			//        table.ForeignKey(
			//            name: "FK__SanPham__Categor__60A75C0F",
			//            column: x => x.CategoriesID,
			//            principalTable: "Categories",
			//            principalColumn: "CategoriesID",
			//            onDelete: ReferentialAction.Cascade);
			//    });

			//migrationBuilder.CreateTable(
			//    name: "RoleClaims",
			//    columns: table => new
			//    {
			//        RoleClaimId = table.Column<int>(type: "int", nullable: false),
			//        RoleId = table.Column<int>(type: "int", nullable: true),
			//        ClaimType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
			//        ClaimValue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
			//    },
			//    constraints: table =>
			//    {
			//        table.PrimaryKey("PK__RoleClai__BB90E9563F926E20", x => x.RoleClaimId);
			//        table.ForeignKey(
			//            name: "FK__RoleClaim__RoleI__5441852A",
			//            column: x => x.RoleId,
			//            principalTable: "Roles",
			//            principalColumn: "RoleId");
			//    });

			//migrationBuilder.CreateTable(
			//    name: "Orders",
			//    columns: table => new
			//    {
			//        OrderId = table.Column<int>(type: "int", nullable: false),
			//        UserId = table.Column<int>(type: "int", nullable: true),
			//        CreateDate = table.Column<DateTime>(type: "datetime", nullable: true)
			//    },
			//    constraints: table =>
			//    {
			//        table.PrimaryKey("PK__Orders__C3905BCF7EAABD66", x => x.OrderId);
			//        table.ForeignKey(
			//            name: "FK__Orders__UserId__6754599E",
			//            column: x => x.UserId,
			//            principalTable: "Users",
			//            principalColumn: "UserId");
			//    });

			//migrationBuilder.CreateTable(
			//    name: "UserClaim",
			//    columns: table => new
			//    {
			//        ClaimId = table.Column<int>(type: "int", nullable: false),
			//        UserId = table.Column<int>(type: "int", nullable: true),
			//        ClaimType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
			//        ClaimValue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
			//    },
			//    constraints: table =>
			//    {
			//        table.PrimaryKey("PK__UserClai__EF2E139B44E11E5F", x => x.ClaimId);
			//        table.ForeignKey(
			//            name: "FK__UserClaim__UserI__5165187F",
			//            column: x => x.UserId,
			//            principalTable: "Users",
			//            principalColumn: "UserId");
			//    });

			//migrationBuilder.CreateTable(
			//    name: "UserLogin",
			//    columns: table => new
			//    {
			//        LoginProvider = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
			//        UserId = table.Column<int>(type: "int", nullable: false),
			//        ProviderDisplayName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
			//    },
			//    constraints: table =>
			//    {
			//        table.PrimaryKey("PK__UserLogi__028A9453A679B493", x => new { x.LoginProvider, x.UserId });
			//        table.ForeignKey(
			//            name: "FK__UserLogin__UserI__571DF1D5",
			//            column: x => x.UserId,
			//            principalTable: "Users",
			//            principalColumn: "UserId");
			//    });

			//migrationBuilder.CreateTable(
			//    name: "UserRoles",
			//    columns: table => new
			//    {
			//        UserId = table.Column<int>(type: "int", nullable: false),
			//        RoleId = table.Column<int>(type: "int", nullable: false)
			//    },
			//    constraints: table =>
			//    {
			//        table.PrimaryKey("PK__UserRole__AF2760AD994C81E4", x => new { x.UserId, x.RoleId });
			//        table.ForeignKey(
			//            name: "FK__UserRoles__RoleI__4E88ABD4",
			//            column: x => x.RoleId,
			//            principalTable: "Roles",
			//            principalColumn: "RoleId");
			//        table.ForeignKey(
			//            name: "FK__UserRoles__UserI__4D94879B",
			//            column: x => x.UserId,
			//            principalTable: "Users",
			//            principalColumn: "UserId");
			//    });

			//migrationBuilder.CreateTable(
			//    name: "UserToken",
			//    columns: table => new
			//    {
			//        UserId = table.Column<int>(type: "int", nullable: false),
			//        LoginProvider = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
			//        Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
			//        Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
			//    },
			//    constraints: table =>
			//    {
			//        table.PrimaryKey("PK__UserToke__8CC498412461FDBC", x => new { x.UserId, x.LoginProvider, x.Name });
			//        table.ForeignKey(
			//            name: "FK__UserToken__UserI__59FA5E80",
			//            column: x => x.UserId,
			//            principalTable: "Users",
			//            principalColumn: "UserId");
			//    });

			//migrationBuilder.CreateTable(
			//    name: "DanhGia",
			//    columns: table => new
			//    {
			//        ID_DanhGia = table.Column<int>(type: "int", nullable: false),
			//        ID_SanPham = table.Column<int>(type: "int", nullable: true),
			//        Comment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
			//        Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
			//        Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
			//        Status = table.Column<int>(type: "int", nullable: true)
			//    },
			//    constraints: table =>
			//    {
			//        table.PrimaryKey("PK__DanhGia__6C898AE12411B9D5", x => x.ID_DanhGia);
			//        table.ForeignKey(
			//            name: "FK__DanhGia__ID_SanP__6477ECF3",
			//            column: x => x.ID_SanPham,
			//            principalTable: "SanPham",
			//            principalColumn: "ID_Ban_Pham");
			//    });

			//migrationBuilder.CreateTable(
			//    name: "OrderDetail",
			//    columns: table => new
			//    {
			//        OrderDetailId = table.Column<int>(type: "int", nullable: false),
			//        OrderId = table.Column<int>(type: "int", nullable: true),
			//        ProductId = table.Column<int>(type: "int", nullable: true),
			//        Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
			//        Quantity = table.Column<int>(type: "int", nullable: true)
			//    },
			//    constraints: table =>
			//    {
			//        table.PrimaryKey("PK__OrderDet__D3B9D36C34CF5106", x => x.OrderDetailId);
			//        table.ForeignKey(
			//            name: "FK__OrderDeta__Order__6A30C649",
			//            column: x => x.OrderId,
			//            principalTable: "Orders",
			//            principalColumn: "OrderId");
			//        table.ForeignKey(
			//            name: "FK__OrderDeta__Produ__6B24EA82",
			//            column: x => x.ProductId,
			//            principalTable: "SanPham",
			//            principalColumn: "ID_Ban_Pham");
			//    });

			migrationBuilder.CreateIndex(
				name: "IX_AspNetRoleClaims_RoleId",
				table: "AspNetRoleClaims",
				column: "RoleId");

			migrationBuilder.CreateIndex(
				name: "RoleNameIndex",
				table: "AspNetRoles",
				column: "NormalizedName",
				unique: true,
				filter: "[NormalizedName] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUserClaims_UserId",
				table: "AspNetUserClaims",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUserLogins_UserId",
				table: "AspNetUserLogins",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUserRoles_RoleId",
				table: "AspNetUserRoles",
				column: "RoleId");

			migrationBuilder.CreateIndex(
				name: "EmailIndex",
				table: "AspNetUsers",
				column: "NormalizedEmail");

			migrationBuilder.CreateIndex(
				name: "UserNameIndex",
				table: "AspNetUsers",
				column: "NormalizedUserName",
				unique: true,
				filter: "[NormalizedUserName] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "IX_DanhGia_ID_SanPham",
				table: "DanhGia",
				column: "IdSanPham");

			migrationBuilder.CreateIndex(
				name: "IX_OrderDetail_OrderId",
				table: "OrderDetail",
				column: "OrderId");

			migrationBuilder.CreateIndex(
				name: "IX_OrderDetail_ProductId",
				table: "OrderDetail",
				column: "ProductId");

			migrationBuilder.CreateIndex(
				name: "IX_Orders_UserId",
				table: "Orders",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_RoleClaims_RoleId",
				table: "RoleClaims",
				column: "RoleId");

			migrationBuilder.CreateIndex(
				name: "IX_SanPham_BrandId",
				table: "SanPham",
				column: "BrandId");

			migrationBuilder.CreateIndex(
				name: "IX_SanPham_CategoriesID",
				table: "SanPham",
				column: "CategoriesID");

			migrationBuilder.CreateIndex(
				name: "IX_UserClaim_UserId",
				table: "UserClaim",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_UserLogin_UserId",
				table: "UserLogin",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_UserRoles_RoleId",
				table: "UserRoles",
				column: "RoleId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "AspNetRoleClaims");

			migrationBuilder.DropTable(
				name: "AspNetUserClaims");

			migrationBuilder.DropTable(
				name: "AspNetUserLogins");

			migrationBuilder.DropTable(
				name: "AspNetUserRoles");

			migrationBuilder.DropTable(
				name: "AspNetUserTokens");

			migrationBuilder.DropTable(
				name: "DanhGia");

			migrationBuilder.DropTable(
				name: "OrderDetail");

			migrationBuilder.DropTable(
				name: "RoleClaims");

			migrationBuilder.DropTable(
				name: "UserClaim");

			migrationBuilder.DropTable(
				name: "UserLogin");

			migrationBuilder.DropTable(
				name: "UserRoles");

			migrationBuilder.DropTable(
				name: "UserToken");

			migrationBuilder.DropTable(
				name: "AspNetRoles");

			migrationBuilder.DropTable(
				name: "AspNetUsers");

			migrationBuilder.DropTable(
				name: "Orders");

			migrationBuilder.DropTable(
				name: "SanPham");

			migrationBuilder.DropTable(
				name: "Roles");

			migrationBuilder.DropTable(
				name: "Users");

			migrationBuilder.DropTable(
				name: "Brands");

			migrationBuilder.DropTable(
				name: "Categories");
		}
	}
}
