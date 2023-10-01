namespace ShoesShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ACCOUNT",
                c => new
                    {
                        IdAccount = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 30, unicode: false),
                        Password = c.String(storeType: "text", unicode: false),
                        MaQuyen = c.Int(),
                    })
                .PrimaryKey(t => t.IdAccount)
                .ForeignKey("dbo.QUYEN", t => t.MaQuyen)
                .Index(t => t.MaQuyen);
            
            CreateTable(
                "dbo.CHUCNANG",
                c => new
                    {
                        IdAccount = c.Int(nullable: false),
                        QlyTaiKhoan = c.Boolean(),
                        QlySanPham = c.Boolean(),
                        QlySanPhamKM = c.Boolean(),
                        QlyDonHang = c.Boolean(),
                        ThongKe = c.Boolean(),
                    })
                .PrimaryKey(t => t.IdAccount)
                .ForeignKey("dbo.ACCOUNT", t => t.IdAccount)
                .Index(t => t.IdAccount);
            
            CreateTable(
                "dbo.DONDATHANG",
                c => new
                    {
                        MaDDH = c.Int(nullable: false, identity: true),
                        NgayDat = c.DateTime(),
                        MaTT = c.Int(),
                        NgayGiao = c.DateTime(),
                        Hoten = c.String(nullable: false, maxLength: 100),
                        DiaChi = c.String(nullable: false, maxLength: 100),
                        SDT = c.String(nullable: false, maxLength: 15, fixedLength: true),
                        Email = c.String(nullable: false, maxLength: 50, fixedLength: true),
                        GhiChu = c.String(maxLength: 100),
                        TongTien = c.Decimal(nullable: false, precision: 18, scale: 0),
                        IdAccount = c.Int(),
                        ThanhToan = c.Boolean(),
                    })
                .PrimaryKey(t => t.MaDDH)
                .ForeignKey("dbo.ACCOUNT", t => t.IdAccount)
                .ForeignKey("dbo.TINHTRANG", t => t.MaTT)
                .Index(t => t.MaTT)
                .Index(t => t.IdAccount);
            
            CreateTable(
                "dbo.CHITIETDONHANG",
                c => new
                    {
                        MaDDH = c.Int(nullable: false),
                        MaSP = c.Int(nullable: false),
                        SoLuong = c.Int(nullable: false),
                        Size = c.String(nullable: false),
                        DonGia = c.Decimal(nullable: false, precision: 18, scale: 0),
                        TongTien = c.Decimal(nullable: false, precision: 18, scale: 0),
                    })
                .PrimaryKey(t => new { t.MaDDH})
                .ForeignKey("dbo.SANPHAM", t => t.MaSP)
                .ForeignKey("dbo.DONDATHANG", t => t.MaDDH)
                .Index(t => t.MaDDH)
                .Index(t => t.MaSP);
            
            CreateTable(
                "dbo.SANPHAM",
                c => new
                    {
                        MaSP = c.Int(nullable: false, identity: true),
                        MaHang = c.Int(nullable: false),
                        TenSanPham = c.String(nullable: false, maxLength: 30),
                        NgayCapNhat = c.DateTime(),
                        GiaBan = c.Int(nullable: false),
                        HinhAnh = c.String(unicode: false, storeType: "text"),
                        SoLuongTon = c.Int(),
                        LuotXem = c.Int(),
                        LuotBinhChon = c.Int(),
                        SoLuongMua = c.Int(),
                        Moi = c.Boolean(),
                        MaDanhMuc = c.Int(nullable: false),
                        MoTa = c.String(maxLength: int.MaxValue)
                })
                .PrimaryKey(t => t.MaSP)
                .ForeignKey("dbo.DANHMUC", t => t.MaDanhMuc)
                .ForeignKey("dbo.HANGGIAY", t => t.MaHang)
                .Index(t => t.MaHang)
                .Index(t => t.MaDanhMuc);
            
            CreateTable(
                "dbo.CHITIETDOANHTHUTHANG",
                c => new
                    {
                        MaSP = c.Int(nullable: false),
                        Thang = c.String(nullable: false, maxLength: 3, fixedLength: true, unicode: false),
                        Nam = c.String(nullable: false, maxLength: 5, fixedLength: true, unicode: false),
                        SoLuongBan = c.Int(nullable: false),
                        DoanhThu = c.Decimal(nullable: false, precision: 18, scale: 0),
                    })
                .PrimaryKey(t => new { t.MaSP, t.Thang, t.Nam })
                .ForeignKey("dbo.DOANHTHUTHANG", t => new { t.Thang, t.Nam })
                .ForeignKey("dbo.SANPHAM", t => t.MaSP)
                .Index(t => t.MaSP)
                .Index(t => new { t.Thang, t.Nam });
            
            CreateTable(
                "dbo.DOANHTHUTHANG",
                c => new
                    {
                        Thang = c.String(nullable: false, maxLength: 3, fixedLength: true, unicode: false),
                        Nam = c.String(nullable: false, maxLength: 5, fixedLength: true, unicode: false),
                        SoLuongGiayBan = c.Int(nullable: false),
                        DoanhThu = c.Decimal(nullable: false, precision: 18, scale: 0),
                    })
                .PrimaryKey(t => new { t.Thang, t.Nam })
                .ForeignKey("dbo.DOANHTHUNAM", t => t.Nam)
                .Index(t => t.Nam);
            
            CreateTable(
                "dbo.DOANHTHUNAM",
                c => new
                    {
                        Nam = c.String(nullable: false, maxLength: 5, fixedLength: true, unicode: false),
                        DoanhThu = c.Decimal(nullable: false, precision: 18, scale: 0),
                    })
                .PrimaryKey(t => t.Nam);
            
            CreateTable(
                "dbo.CHITIETKHUYENMAI",
                c => new
                    {
                        MaKhuyenMai = c.Int(nullable: false),
                        MaSP = c.Int(nullable: false),
                        GiaKM = c.Decimal(nullable: false, precision: 18, scale: 0),
                    })
                .PrimaryKey(t => new { t.MaKhuyenMai, t.MaSP })
                .ForeignKey("dbo.KHUYENMAI", t => t.MaKhuyenMai)
                .ForeignKey("dbo.SANPHAM", t => t.MaSP)
                .Index(t => t.MaKhuyenMai)
                .Index(t => t.MaSP);
            
            CreateTable(
                "dbo.KHUYENMAI",
                c => new
                    {
                        MaKhuyenMai = c.Int(nullable: false, identity: true),
                        NgayBatDau = c.DateTime(),
                        NgayKetThuc = c.DateTime(),
                        TenKhuyenMai = c.String(maxLength: 30),
                        NgayTao = c.DateTime(),
                        NoiDungKM = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.MaKhuyenMai);
            
            CreateTable(
                "dbo.DANHMUC",
                c => new
                    {
                        MaDanhMuc = c.Int(nullable: false, identity: true),
                        TenDanhMuc = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.MaDanhMuc);
            
            CreateTable(
                "dbo.HANGGIAY",
                c => new
                    {
                        MaHang = c.Int(nullable: false, identity: true),
                        TenHang = c.String(nullable: false, maxLength: 50),
                        ThongTin = c.String(nullable: false, maxLength: 100),
                        Logo = c.String(nullable: false, unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => t.MaHang);
            
            CreateTable(
                "dbo.TINHTRANG",
                c => new
                    {
                        MaTT = c.Int(nullable: false, identity: true),
                        TinhTrangGiao = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.MaTT);
            
            CreateTable(
                "dbo.NHANVIEN",
                c => new
                    {
                        MaNV = c.Int(nullable: false, identity: true),
                        TenNhanVien = c.String(nullable: false, maxLength: 50),
                        NgaySinh = c.DateTime(nullable: false),
                        GioiTinh = c.String(nullable: false, maxLength: 5, fixedLength: true),
                        CMND = c.String(nullable: false, maxLength: 12, fixedLength: true, unicode: false),
                        DiaChi = c.String(nullable: false, maxLength: 30),
                        SDT = c.String(nullable: false, maxLength: 11, fixedLength: true, unicode: false),
                        IdAccount = c.Int(),
                        HinhAnh = c.String(unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => t.MaNV)
                .ForeignKey("dbo.ACCOUNT", t => t.IdAccount)
                .Index(t => t.IdAccount);
            
            CreateTable(
                "dbo.QUYEN",
                c => new
                    {
                        MaQuyen = c.Int(nullable: false, identity: true),
                        TenQuyen = c.String(nullable: false, maxLength: 6, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.MaQuyen);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ACCOUNT", "MaQuyen", "dbo.QUYEN");
            DropForeignKey("dbo.NHANVIEN", "IdAccount", "dbo.ACCOUNT");
            DropForeignKey("dbo.DONDATHANG", "MaTT", "dbo.TINHTRANG");
            DropForeignKey("dbo.CHITIETDONHANG", "MaDDH", "dbo.DONDATHANG");
            DropForeignKey("dbo.SANPHAM", "MaHang", "dbo.HANGGIAY");
            DropForeignKey("dbo.SANPHAM", "MaDanhMuc", "dbo.DANHMUC");
            DropForeignKey("dbo.CHITIETKHUYENMAI", "MaSP", "dbo.SANPHAM");
            DropForeignKey("dbo.CHITIETKHUYENMAI", "MaKhuyenMai", "dbo.KHUYENMAI");
            DropForeignKey("dbo.CHITIETDONHANG", "MaSP", "dbo.SANPHAM");
            DropForeignKey("dbo.CHITIETDOANHTHUTHANG", "MaSP", "dbo.SANPHAM");
            DropForeignKey("dbo.DOANHTHUTHANG", "Nam", "dbo.DOANHTHUNAM");
            DropForeignKey("dbo.CHITIETDOANHTHUTHANG", new[] { "Thang", "Nam" }, "dbo.DOANHTHUTHANG");
            DropForeignKey("dbo.DONDATHANG", "IdAccount", "dbo.ACCOUNT");
            DropForeignKey("dbo.CHUCNANG", "IdAccount", "dbo.ACCOUNT");
            DropIndex("dbo.NHANVIEN", new[] { "IdAccount" });
            DropIndex("dbo.CHITIETKHUYENMAI", new[] { "MaSP" });
            DropIndex("dbo.CHITIETKHUYENMAI", new[] { "MaKhuyenMai" });
            DropIndex("dbo.DOANHTHUTHANG", new[] { "Nam" });
            DropIndex("dbo.CHITIETDOANHTHUTHANG", new[] { "Thang", "Nam" });
            DropIndex("dbo.CHITIETDOANHTHUTHANG", new[] { "MaSP" });
            DropIndex("dbo.SANPHAM", new[] { "MaDanhMuc" });
            DropIndex("dbo.SANPHAM", new[] { "MaHang" });
            DropIndex("dbo.CHITIETDONHANG", new[] { "MaSP" });
            DropIndex("dbo.CHITIETDONHANG", new[] { "MaDDH" });
            DropIndex("dbo.DONDATHANG", new[] { "IdAccount" });
            DropIndex("dbo.DONDATHANG", new[] { "MaTT" });
            DropIndex("dbo.CHUCNANG", new[] { "IdAccount" });
            DropIndex("dbo.ACCOUNT", new[] { "MaQuyen" });
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.QUYEN");
            DropTable("dbo.NHANVIEN");
            DropTable("dbo.TINHTRANG");
            DropTable("dbo.HANGGIAY");
            DropTable("dbo.DANHMUC");
            DropTable("dbo.KHUYENMAI");
            DropTable("dbo.CHITIETKHUYENMAI");
            DropTable("dbo.DOANHTHUNAM");
            DropTable("dbo.DOANHTHUTHANG");
            DropTable("dbo.CHITIETDOANHTHUTHANG");
            DropTable("dbo.SANPHAM");
            DropTable("dbo.CHITIETDONHANG");
            DropTable("dbo.DONDATHANG");
            DropTable("dbo.CHUCNANG");
            DropTable("dbo.ACCOUNT");
        }
    }
}
