namespace MvcAppMain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BK_PhieuKhamBenh
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_PhieuKham { get; set; }

        public int IDPhieuTN { get; set; }

        public int ID_Benh { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayKham { get; set; }

        [StringLength(255)]
        public string TrieuChung { get; set; }

    }
}
