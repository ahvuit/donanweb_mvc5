namespace ShoesShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CHITIETKHUYENMAI")]
    public partial class CHITIETKHUYENMAI
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaKhuyenMai { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaSP { get; set; }

        public decimal GiaKM { get; set; }
        [StringLength(150)]
        public string UpdateBy { get; set; }

        public virtual KHUYENMAI KHUYENMAI { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
    }
}
