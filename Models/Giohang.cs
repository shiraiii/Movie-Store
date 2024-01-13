using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnMonHoc.Models
{
    public class Giohang
    {
        dbQLBanveDataContext data = new dbQLBanveDataContext();
        public int iMaphim { get; set; }
        public string sTenPhim { get; set; }
        public string sAnhbia { get; set; }
        public Double dDongia { get; set; }
        public int iSoLuong { get; set; }
        public Double dThanhtien
        {
            get { return iSoLuong * dDongia;  }
        }
        public Giohang(int Maphim)
        {
            iMaphim= Maphim;
            PHIM phim = data.PHIMs.Single(n => n.MAPHIM == iMaphim);
            sTenPhim = phim.TENPHIM;
            sAnhbia= phim.ANHBIA;
            dDongia =double.Parse(phim.GIABAN.ToString());
            iSoLuong = 1;

        }
    }
}