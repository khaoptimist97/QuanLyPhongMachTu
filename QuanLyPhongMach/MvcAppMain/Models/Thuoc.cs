namespace MvcAppMain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Thuoc")]
    public partial class Thuoc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Thuoc()
        {
            CT_PhieuKhamBenhs = new HashSet<CT_PhieuKhamBenh>();
        }

        [Key]
        public int ID_Thuoc { get; set; }

       

        [StringLength(255)]
        public string TenThuoc { get; set; }
        public int SoLuong { get; set; }

        public int DonGia { get; set; }
        public int ID_DonVi { get; set; }
        public int ID_CachDung { get; set; }

        public bool Deleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_PhieuKhamBenh> CT_PhieuKhamBenhs { get; set; }

        public virtual DonVi DonVi { get; set; }
        public virtual CachDung CachDung { get; set; }
    }
}
