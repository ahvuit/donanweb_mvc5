using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ShoesShop.Models
{
    public partial class DBContextModel : DbContext
    {
        public DBContextModel()
            : base("name=DBContextModel")
        {
        }

        public virtual DbSet<ACCOUNT> ACCOUNTs { get; set; }
        public virtual DbSet<BANGSIZE> BANGSIZEs { get; set; }
        public virtual DbSet<CHITIETDOANHTHUTHANG> CHITIETDOANHTHUTHANGs { get; set; }
        public virtual DbSet<CHITIETKHUYENMAI> CHITIETKHUYENMAIs { get; set; }
        public virtual DbSet<CHUCNANG> CHUCNANGs { get; set; }
        public virtual DbSet<DANHMUC> DANHMUCs { get; set; }
        public virtual DbSet<DOANHTHUNAM> DOANHTHUNAMs { get; set; }
        public virtual DbSet<DOANHTHUTHANG> DOANHTHUTHANGs { get; set; }
        public virtual DbSet<DONDATHANG> DONDATHANGs { get; set; }
        public virtual DbSet<HANGGIAY> HANGGIAYs { get; set; }
        public virtual DbSet<KHUYENMAI> KHUYENMAIs { get; set; }
        public virtual DbSet<NHANVIEN> NHANVIENs { get; set; }
        public virtual DbSet<QUYEN> QUYENs { get; set; }
        public virtual DbSet<SANPHAM> SANPHAMs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TINHTRANG> TINHTRANGs { get; set; }
        public virtual DbSet<CHITIETDONHANG> CHITIETDONHANGs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ACCOUNT>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<ACCOUNT>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<ACCOUNT>()
                .HasOptional(e => e.CHUCNANG)
                .WithRequired(e => e.ACCOUNT);

            modelBuilder.Entity<CHITIETDOANHTHUTHANG>()
                .Property(e => e.Thang)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CHITIETDOANHTHUTHANG>()
                .Property(e => e.DoanhThu)
                .HasPrecision(18, 0);

            modelBuilder.Entity<CHITIETDOANHTHUTHANG>()
                .Property(e => e.Nam)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CHITIETKHUYENMAI>()
                .Property(e => e.GiaKM)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DANHMUC>()
                .HasMany(e => e.SANPHAMs)
                .WithRequired(e => e.DANHMUC)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DOANHTHUNAM>()
                .Property(e => e.Nam)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DOANHTHUNAM>()
                .Property(e => e.DoanhThu)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DOANHTHUNAM>()
                .HasMany(e => e.DOANHTHUTHANGs)
                .WithRequired(e => e.DOANHTHUNAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DOANHTHUTHANG>()
                .Property(e => e.Thang)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DOANHTHUTHANG>()
                .Property(e => e.Nam)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DOANHTHUTHANG>()
                .Property(e => e.DoanhThu)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DOANHTHUTHANG>()
                .HasMany(e => e.CHITIETDOANHTHUTHANGs)
                .WithRequired(e => e.DOANHTHUTHANG)
                .HasForeignKey(e => new { e.Thang, e.Nam })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DONDATHANG>()
                .Property(e => e.SDT)
                .IsFixedLength();

            modelBuilder.Entity<DONDATHANG>()
                .Property(e => e.Email)
                .IsFixedLength();

            modelBuilder.Entity<DONDATHANG>()
                .Property(e => e.TongTien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HANGGIAY>()
                .Property(e => e.Logo)
                .IsUnicode(false);

            modelBuilder.Entity<HANGGIAY>()
                .HasMany(e => e.SANPHAMs)
                .WithRequired(e => e.HANGGIAY)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KHUYENMAI>()
                .HasMany(e => e.CHITIETKHUYENMAIs)
                .WithRequired(e => e.KHUYENMAI)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NHANVIEN>()
                .Property(e => e.CMND)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NHANVIEN>()
                .Property(e => e.SDT)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NHANVIEN>()
                .Property(e => e.HinhAnh)
                .IsUnicode(false);

            modelBuilder.Entity<QUYEN>()
                .Property(e => e.TenQuyen)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SANPHAM>()
                .Property(e => e.HinhAnh)
                .IsUnicode(false);

            modelBuilder.Entity<SANPHAM>()
                .HasMany(e => e.BANGSIZEs)
                .WithRequired(e => e.SANPHAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SANPHAM>()
                .HasMany(e => e.CHITIETDOANHTHUTHANGs)
                .WithRequired(e => e.SANPHAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SANPHAM>()
                .HasMany(e => e.CHITIETKHUYENMAIs)
                .WithRequired(e => e.SANPHAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CHITIETDONHANG>()
                .Property(e => e.Size)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CHITIETDONHANG>()
                .Property(e => e.DonGia)
                .HasPrecision(18, 0);

            modelBuilder.Entity<CHITIETDONHANG>()
                .Property(e => e.TongTien)
                .HasPrecision(18, 0);
        }
    }
}
