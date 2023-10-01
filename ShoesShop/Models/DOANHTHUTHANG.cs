namespace ShoesShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DOANHTHUTHANG")]
    public partial class DOANHTHUTHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DOANHTHUTHANG()
        {
            CHITIETDOANHTHUTHANGs = new HashSet<CHITIETDOANHTHUTHANG>();
        }

        [Key]
        [Column(Order = 0)]
        [StringLength(3)]
        public string Thang { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string Nam { get; set; }

        public int SoLuongGiayBan { get; set; }

        public decimal DoanhThu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETDOANHTHUTHANG> CHITIETDOANHTHUTHANGs { get; set; }

        public virtual DOANHTHUNAM DOANHTHUNAM { get; set; }
    }
}
