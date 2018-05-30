namespace MvcAppMain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BK_HoaDon
    {
        [Key]
        public int ID_HoaDon { get; set; }
        public int ID_PhieuKham { get; set; }

        public int TienKham { get; set; }

        public int TienThuoc { get; set; }

        public int TongTien { get; set; }
    }
}
