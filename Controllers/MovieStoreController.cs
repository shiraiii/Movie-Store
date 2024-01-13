using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnMonHoc.Models;

namespace DoAnMonHoc.Controllers
{
    public class MovieStoreController : Controller
    {
        dbQLBanveDataContext data = new dbQLBanveDataContext();
        private List<PHIM> Layphimmoi(int count1)
        {
            return data.PHIMs.OrderBy(i=>i.NGAYCAPNHAT).Take(count1).ToList();
        }
        public ActionResult Index()
        {
            var phimmoi = Layphimmoi(12);
            return View(phimmoi);
        }
        private List<PHIM> layThemPhim(int count2)
        {
            return data.PHIMs.OrderByDescending(i => i.NGAYCAPNHAT).Take(count2).ToList();
        }
        public ActionResult themPhim()
        {
            var themPhim = layThemPhim(9);
            return View(themPhim);
        }
     
        public ActionResult SPTheochude(int id)
        {
            var Phim = from s in data.PHIMs where s.MAPHIM == id select s;
            return View(Phim);
        }
        public ActionResult Details(int id)
        {
            var Phim = from s in data.PHIMs
                       where s.MAPHIM == id
                       select s;
            return View(Phim.Single());
        }
       
    }
}