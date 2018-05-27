namespace MvcAppMain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuTiepNhan")]
    public partial class PhieuTiepNhan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuTiepNhan()
        {
            PhieuKhamBenhs = new HashSet<PhieuKhamBenh>();
        }           
        [Key]
        public int IDPhieuTN { get; set; }
        [Display(Name = "Tên bệnh nhân")]
        public int ID_BenhNhan { get; set; }
        
        [DataType(DataType.DateTime)]
        [Display(Name = "Ngày tiếp nhận")]
        public DateTime NgayTiepNhan { get; set; }
        public bool Deleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuKhamBenh> PhieuKhamBenhs { get; set; }

        public virtual HoSoBenhNhan HoSoBenhNhan { get; set; }
    }
}
