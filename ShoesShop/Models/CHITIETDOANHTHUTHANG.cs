namespace ShoesShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CHITIETDOANHTHUTHANG")]
    public partial class CHITIETDOANHTHUTHANG
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaSP { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string Thang { get; set; }

        public int SoLuongBan { get; set; }

        public decimal DoanhThu { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(5)]
        public string Nam { get; set; }

        public virtual DOANHTHUTHANG DOANHTHUTHANG { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
    }
}
