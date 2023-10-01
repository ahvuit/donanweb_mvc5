namespace ShoesShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BANGSIZE")]
    public partial class BANGSIZE
    {
        [Key]
        public int MaSize { get; set; }

        public int MaSP { get; set; }

        public int? s38 { get; set; }

        public int? s39 { get; set; }

        public int? s40 { get; set; }

        public int? s41 { get; set; }

        public int? s42 { get; set; }

        [Column("s42.5")]
        public int? s42_5 { get; set; }

        public int? s43 { get; set; }

        public int? s44 { get; set; }

        public int? s45 { get; set; }

        public int? s46 { get; set; }

        public int? s47 { get; set; }

        public int? s48 { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
    }
}
