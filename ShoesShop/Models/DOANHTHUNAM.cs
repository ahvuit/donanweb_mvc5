namespace ShoesShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DOANHTHUNAM")]
    public partial class DOANHTHUNAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DOANHTHUNAM()
        {
            DOANHTHUTHANGs = new HashSet<DOANHTHUTHANG>();
        }

        [Key]
        [StringLength(5)]
        public string Nam { get; set; }

        public decimal DoanhThu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DOANHTHUTHANG> DOANHTHUTHANGs { get; set; }
    }
}
