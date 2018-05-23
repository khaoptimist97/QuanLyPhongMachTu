namespace MvcAppMain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CachDung")]
    public partial class CachDung
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CachDung()
        {
            Thuocs = new HashSet<Thuoc>();
        }

        [Key]
        public int ID_CachDung { get; set; }

        [StringLength(255)]
        public string TenCachDung { get; set; }

        public virtual ICollection<Thuoc> Thuocs { get; set; }
        public bool Deleted { get; set; }
    }
}
