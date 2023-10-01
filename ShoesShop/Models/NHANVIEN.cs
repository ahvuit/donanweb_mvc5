namespace ShoesShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("NHANVIEN")]
    public partial class NHANVIEN
    {
        public NHANVIEN()
        {
            HinhAnh = "Add_images.png";
        }
        [Key]
        public int MaNV { get; set; }

        [Required]
        [StringLength(50)]
        public string TenNhanVien { get; set; }

        public DateTime NgaySinh { get; set; }

        [Required]
        [StringLength(5)]
        public string GioiTinh { get; set; }

        [Required]
        [StringLength(12)]
        public string CMND { get; set; }

        [Required]
        [StringLength(30)]
        public string DiaChi { get; set; }

        [Required]
        [StringLength(11)]
        public string SDT { get; set; }

        [Column(TypeName = "text")]
        [DisplayName("Upload Image")]
        public string HinhAnh { get; set; }
        [NotMapped]
        public HttpPostedFileBase HinhAnhFile { get; set; }

        public int? IdAccount { get; set; }

        public virtual ACCOUNT ACCOUNT { get; set; }
    }
}
