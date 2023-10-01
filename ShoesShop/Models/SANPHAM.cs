namespace ShoesShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("SANPHAM")]
    public partial class SANPHAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SANPHAM()
        {
            size = "0";
            HinhAnh = "Add_images.png";
            BANGSIZEs = new HashSet<BANGSIZE>();
            CHITIETDOANHTHUTHANGs = new HashSet<CHITIETDOANHTHUTHANG>();
            CHITIETKHUYENMAIs = new HashSet<CHITIETKHUYENMAI>();
            CHITIETDONHANGs = new HashSet<CHITIETDONHANG>();
        }
        public string size;
        [Key]
        public int MaSP { get; set; }

        public int MaHang { get; set; }

        [Required]
        [StringLength(30)]
        public string TenSanPham { get; set; }

        public DateTime? NgayCapNhat { get; set; }

        public int GiaBan { get; set; }

        [Column(TypeName = "text")]
        [DisplayName("Upload Image")]
        public string HinhAnh { get; set; }
        [NotMapped]
        public HttpPostedFileBase HinhAnhFile { get; set; }
        public int? SoLuongTon { get; set; }

        public int? LuotXem { get; set; }

        public int? LuotBinhChon { get; set; }

        public int? SoLuongMua { get; set; }

        public bool? Moi { get; set; }

        public int MaDanhMuc { get; set; }

        public string MoTa { get; set; }

        [StringLength(150)]
        public string UpdateBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BANGSIZE> BANGSIZEs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETDOANHTHUTHANG> CHITIETDOANHTHUTHANGs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETKHUYENMAI> CHITIETKHUYENMAIs { get; set; }

        public virtual DANHMUC DANHMUC { get; set; }

        public virtual HANGGIAY HANGGIAY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETDONHANG> CHITIETDONHANGs { get; set; }
    }
}
