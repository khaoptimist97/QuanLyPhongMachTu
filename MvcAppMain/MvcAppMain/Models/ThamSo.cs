namespace MvcAppMain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThamSo")]
    public partial class ThamSo
    {
        [Key]
        public int ID_ThamSo { get; set; }

        public int? GiaTri { get; set; }

        [StringLength(255)]
        public string GhiChu { get; set; }
    }
}
