namespace ShoesShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CHITIETDONHANG")]
    public partial class CHITIETDONHANG
    {
        public int? MaDDH { get; set; }

        public int? MaSP { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SoLuong { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(7)]
        public string Size { get; set; }

        [Key]
        [Column(Order = 2)]
        public decimal DonGia { get; set; }

        [Key]
        [Column(Order = 3)]
        public decimal TongTien { get; set; }

        public virtual DONDATHANG DONDATHANG { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
    }
}
