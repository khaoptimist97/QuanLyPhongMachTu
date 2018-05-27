namespace MvcAppMain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoSoBenhNhan")]
    public partial class HoSoBenhNhan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoSoBenhNhan()
        {
            PhieuTiepNhans = new HashSet<PhieuTiepNhan>();
        }

        [Key]
        public int ID_BenhNhan { get; set; }

        [StringLength(255)]
        public string HoTen { get; set; }

        public bool GioiTinh { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NamSinh { get; set; }

        [StringLength(255)]
        public string DiaChi { get; set; }
        public bool Deleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuTiepNhan> PhieuTiepNhans { get; set; }
    }
}
