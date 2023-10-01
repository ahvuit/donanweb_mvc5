namespace ShoesShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CHUCNANG")]
    public partial class CHUCNANG
    {
        public bool? QlyTaiKhoan { get; set; }

        public bool? QlySanPham { get; set; }

        public bool? QlySanPhamKM { get; set; }

        public bool? QlyDonHang { get; set; }

        public bool? ThongKe { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdAccount { get; set; }

        public virtual ACCOUNT ACCOUNT { get; set; }
    }
}
