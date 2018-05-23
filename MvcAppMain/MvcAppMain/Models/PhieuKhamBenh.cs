namespace MvcAppMain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuKhamBenh")]
    public partial class PhieuKhamBenh
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuKhamBenh()
        {        
            CT_PhieuKhamBenhs = new HashSet<CT_PhieuKhamBenh>();
            HoaDons = new HashSet<HoaDon>();
        }

        [Key]
        public int ID_PhieuKham { get; set; }

        public int IDPhieuTN { get; set; }
        public int ID_Benh { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayKham { get; set; }

        [StringLength(255)]
        public string TrieuChung { get; set; }
        public bool Deleted { get; set; }
        public virtual Benh Benh { get; set; }
        public virtual PhieuTiepNhan PhieuTiepNhan { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_PhieuKhamBenh> CT_PhieuKhamBenhs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}
