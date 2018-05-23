using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcAppMain.ViewModel
{
    public class PhieuKhamBenhViewModel
    {
        public int ID_PhieuKham { get; set; }
        public int IDPhieuTN { get; set; }
        public int ID_Benh { get; set; }
        public DateTime? NgayKham { get; set; }
        public string TrieuChung { get; set; }
        public string TenBenhNhan { get; set; }
        public string TenBenh { get; set; }
        public List<ChiTietPhieuViewModel> chiTietPhieuKhamBenh { get; set; }
    }
}