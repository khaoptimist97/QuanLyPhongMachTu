namespace MvcAppMain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Benh")]
    public partial class Benh
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Benh()
        {
            PhieuKhamBenhs = new HashSet<PhieuKhamBenh>();
        }

        [Key]
        public int ID_Benh { get; set; }


        [StringLength(255)]
        public string TenBenh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuKhamBenh> PhieuKhamBenhs { get; set; }
    }
}
