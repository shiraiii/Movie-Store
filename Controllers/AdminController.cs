using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnMonHoc.Models;
using PagedList;
using PagedList.Mvc;

namespace DoAnMonHoc.Controllers
{
    public class AdminController : Controller
    {
        dbQLBanveDataContext db = new dbQLBanveDataContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var tendn = collection["username"];
            var matkhau = collection["password"];
            if(String.IsNullOrEmpty(tendn) )
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if(String.IsNullOrEmpty(matkhau) )
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                Admin ad = db.Admins.SingleOrDefault(n => n.UserAdmin == tendn && n.PassAdmin == matkhau);
                if(ad != null)
                {
                    Session["Taikhoanadmin"] = ad;
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Remove("Taikhoanadmin");
            return RedirectToAction("Index", "Admin");
        }
        public ActionResult Phim(int ?page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            //return View(db.Product_Details.ToList());
            return View(db.PHIMs.ToList().OrderBy(n => n.MAPHIM).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Themmoiphim()
        {
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TENCHUDE),"MaCD","TenChude");
            ViewBag.QUOCGIA = new SelectList(db.PHIMs.ToList().OrderBy(n => n.QUOCGIA), "QUOCGIA", "QuocGia");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Themmoiphim(PHIM phim, HttpPostedFileBase fileUpload)
        {
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => db.CHUDEs), "MaCD", "TenChude");
            ViewBag.QUOCGIA = new SelectList(db.PHIMs.ToList().OrderBy(n => n.QUOCGIA), "QUOCGIA", "QuocGia");
            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/assets/img/"), fileName);
                    if (System.IO.File.Exists(path))
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    else
                    {
                        fileUpload.SaveAs(path);
                    }
                    phim.ANHBIA = fileName;
                    db.PHIMs.InsertOnSubmit(phim);
                    db.SubmitChanges();

                }
                return RedirectToAction("Phim");
            }

        }
        public ActionResult Chitietphim(int id)
        {
            PHIM phim = db.PHIMs.SingleOrDefault(n => n.MAPHIM == id);
            ViewBag.MaPhim = phim.MAPHIM;
            if (phim == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(phim);
        }
        [HttpGet]
        public ActionResult Xoaphim(int id)
        {
            PHIM phim = db.PHIMs.SingleOrDefault(n => n.MAPHIM == id);
            ViewBag.MaPhim= phim.MAPHIM;
            if (phim == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(phim);
        }
        [HttpPost, ActionName("Xoaphim")]
        public ActionResult Xacnhanxoa(int id)
        {
            PHIM phim = db.PHIMs.FirstOrDefault(n => n.MAPHIM == id);
            ViewBag.MaPhim = phim.MAPHIM;
            if (phim == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.PHIMs.DeleteOnSubmit(phim);
            db.SubmitChanges();
            return RedirectToAction("Phim");
        }
        [HttpGet]
        public ActionResult Suaphim(int id)
        {
            PHIM phim = db.PHIMs.SingleOrDefault(n => n.MAPHIM == id);
            if (phim == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.Maphim = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TENCHUDE), "MaCD", "TenChude");
            ViewBag.QUOCGIA = new SelectList(db.PHIMs.ToList().OrderBy(n => n.QUOCGIA), "QUOCGIA", "QuocGia");
            return View(phim);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Suaphim(PHIM phim, FormCollection form, HttpPostedFileBase fileupload)
        {
            var Phim = db.PHIMs.FirstOrDefault(n => n.MAPHIM == phim.MAPHIM);
            if (ModelState.IsValid)
            {
                if(fileupload != null)
                {
                    var fileName = Path.GetFileName(fileupload.FileName);
                    var path = Path.Combine(Server.MapPath("~/assets/img/"), fileName);
                    path = Path.Combine(Server.MapPath("~/assets/img/"), form["oldimgae"]);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                        phim.ANHBIA = fileName;
                        fileupload.SaveAs(path);

                    }
                }
                else
                {
                    phim.ANHBIA = form["oldimage"];
                }
                db.PHIMs.DeleteOnSubmit(Phim);
                db.PHIMs.InsertOnSubmit(phim);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Phim");
        }
        public ActionResult Taikhoan(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            //return View(db.Product_Details.ToList());
            return View(db.KHACHHANGs.ToList().OrderBy(n => n.MAKH).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult ThemmoiKH()
        {            
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemmoiKH(KHACHHANG khachhang)
        {
            if (ModelState.IsValid)
            {
                db.KHACHHANGs.InsertOnSubmit(khachhang);
                db.SubmitChanges();

            }
            return RedirectToAction("Taikhoan");
        }
        public ActionResult ChitietKH(int id)
        {
            KHACHHANG khachhang = db.KHACHHANGs.SingleOrDefault(n => n.MAKH== id);
            ViewBag.MaKH = khachhang.MAKH;
            if (khachhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(khachhang);
        }
        public ActionResult XoaKH(int id)
        {
            KHACHHANG khachhang= db.KHACHHANGs.SingleOrDefault(n => n.MAKH== id);
            ViewBag.MaKH = khachhang.MAKH;
            if (khachhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(khachhang);
        }
        [HttpPost, ActionName("XoaKH")]
        public ActionResult XacnhanxoaKH(int id)
        {
            KHACHHANG khachhang = db.KHACHHANGs.FirstOrDefault(n => n.MAKH == id);
            ViewBag.MaPhim = khachhang.MAKH;
            if (khachhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.KHACHHANGs.DeleteOnSubmit(khachhang);
            db.SubmitChanges();
            return RedirectToAction("Taikhoan");
        }
        [HttpGet]
        public ActionResult SuaKH(int id)
        {
            KHACHHANG khachhang = db.KHACHHANGs.SingleOrDefault(n => n.MAKH== id);
            if (khachhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(khachhang);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaKH(KHACHHANG khachhang, FormCollection form, HttpPostedFileBase fileupload)
        {
            var KH = db.KHACHHANGs.FirstOrDefault(n => n.MAKH == khachhang.MAKH);
            if (ModelState.IsValid)
            {
                db.KHACHHANGs.DeleteOnSubmit(KH);
                db.KHACHHANGs.InsertOnSubmit(khachhang);
                db.SubmitChanges();
            }
            ViewBag.MaKH = khachhang.MAKH;
            return RedirectToAction("Taikhoan");
        }
        public ActionResult Donhang(int? page)
        {
            
            

            int pageNumber = (page ?? 1);
            int pageSize = 7;
            //return View(db.Product_Details.ToList());
            return View(db.DONDATHANGs.ToList().OrderBy(n => n.MADONHANG).ToPagedList(pageNumber, pageSize));
            
        }
        [HttpGet]
        public ActionResult ThemDH()
        {
            ViewBag.MaKH = new SelectList(db.KHACHHANGs.ToList().OrderBy(n => n.HoTen), "MaKH", "Hoten");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemDH(DONDATHANG dondathang)
        {
            if (ModelState.IsValid)
            {
                db.DONDATHANGs.InsertOnSubmit(dondathang);
                db.SubmitChanges();

            }
            return RedirectToAction("Donhang");
        }
        public ActionResult ChitietDH(int id)
        {
            DONDATHANG dondathang= db.DONDATHANGs.SingleOrDefault(n => n.MADONHANG == id);
            ViewBag.MaDH = dondathang.MADONHANG;
            if (dondathang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dondathang);
        }
        public ActionResult XoaDH(int id)
        {
            DONDATHANG dondathang= db.DONDATHANGs.SingleOrDefault(n => n.MADONHANG == id);
            ViewBag.MaDH = dondathang.MADONHANG;
            if (dondathang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dondathang);
        }
        [HttpPost, ActionName("XoaDH")]
        public ActionResult XacnhanxoaDH(int id)
        {
            DONDATHANG dondathang= db.DONDATHANGs.FirstOrDefault(n => n.MADONHANG == id);
            ViewBag.MaDH = dondathang.MADONHANG;
            if (dondathang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.DONDATHANGs.DeleteOnSubmit(dondathang);
            db.SubmitChanges();
            return RedirectToAction("Donhang");
        }
        [HttpGet]
        public ActionResult SuaDH(int id)
        {
            DONDATHANG dondathang= db.DONDATHANGs.SingleOrDefault(n => n.MADONHANG == id);
            if (dondathang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaKH = new SelectList(db.KHACHHANGs.ToList().OrderBy(n => n.HoTen), "MaKH", "HoTen");
            return View(dondathang);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaDH(DONDATHANG dondatahang, FormCollection form, HttpPostedFileBase fileupload)
        {
            var DH = db.DONDATHANGs.FirstOrDefault(n => n.MADONHANG == dondatahang.MADONHANG);
            if (ModelState.IsValid)
            {
                db.DONDATHANGs.DeleteOnSubmit(DH);
                db.DONDATHANGs.InsertOnSubmit(dondatahang);
                db.SubmitChanges();
            }
            ViewBag.MaKH = new SelectList(db.KHACHHANGs.ToList().OrderBy(n => n.HoTen), "MaKH", "HoTen");
            ViewBag.MaDH = dondatahang.MADONHANG;
            return RedirectToAction("Donhang");
        }
    }
}