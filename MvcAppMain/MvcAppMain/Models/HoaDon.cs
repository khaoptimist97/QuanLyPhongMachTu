namespace MvcAppMain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDon")]
    public partial class HoaDon
    {
        [Key]
        public int ID_HoaDon { get; set; }
        public int ID_PhieuKham { get; set; }

        public int TienKham { get; set; }

        public int TienThuoc { get; set; }

        public int TongTien { get; set; }

        [StringLength(255)]
        public string GhiChu { get; set; }
        public bool Deleted { get; set; }

        public virtual PhieuKhamBenh PhieuKhamBenh { get; set; }
    }
}
