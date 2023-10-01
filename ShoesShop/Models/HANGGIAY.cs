namespace ShoesShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("HANGGIAY")]
    public partial class HANGGIAY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HANGGIAY()
        {
            Logo = "Add_images.png";
            SANPHAMs = new HashSet<SANPHAM>();
        }

        [Key]
        public int MaHang { get; set; }

        [Required]
        [StringLength(50)]
        public string TenHang { get; set; }

        [Required]
        [StringLength(100)]
        public string ThongTin { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [DisplayName("Upload Image")]
        public string Logo { get; set; }
        [NotMapped]
        public HttpPostedFileBase LogoFile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}
