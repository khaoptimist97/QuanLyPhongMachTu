namespace MvcAppMain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CT_PhieuKhamBenh
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_PhieuKham { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_Thuoc { get; set; }

        public int SoLuongThuocLay { get; set; }
        public int DonGiaBan { get; set; }
        public int ThanhTien { get; set; }
        public bool Deleted { get; set; }

        public virtual PhieuKhamBenh PhieuKhamBenh { get; set; }

        public virtual Thuoc Thuoc { get; set; }
    }
}
