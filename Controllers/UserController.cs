using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using DoAnMonHoc.Models;

namespace DoAnMonHoc.Controllers
{
    public class NguoidungController : Controller
    {
        dbQLBanveDataContext data = new dbQLBanveDataContext();
        // GET: Nguoidung
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Dangky()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangky(FormCollection collection, KHACHHANG Kh)
        {
            var hoten = collection["HotenKH"];
            var tendn = collection["TenDN"];

            var matkhau = collection["Matkhau"];

            var matkhaunhaplai = collection["Matkhaunhaplai"];

            var diachi = collection["Diachi"];

            var email = collection["Email"];

            var dienthoai = collection["Dienthoai"];
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ tên khách hàng không được để trống";
            }
            else if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Phải nhập mật khẩu";
            }
            else if (String.IsNullOrEmpty(matkhaunhaplai))
            {
                ViewData["Loi4"] = "Phải nhập lại mật khẩu";
            }
            else if (!matkhaunhaplai.Equals(matkhau))
            {
                ViewData["Loi10"] = "Phải trùng với mật khẩu";
            }
            else if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi5"] = "Email không được bỏ trống";
            }
            else if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi6"] = "Phải nhập điện thoại";
            }
            else
            {
                Kh.HoTen = hoten;
                Kh.Taikhoan = tendn;
                Kh.MATKHAU = matkhau;
                Kh.EMAIL = email;
                Kh.DIACHIKH = diachi;
                Kh.DIENTHOAIKH = dienthoai;
                data.KHACHHANGs.InsertOnSubmit(Kh);
                data.SubmitChanges();
                return RedirectToAction("Dangnhap");
            }
            return this.Dangky();
        }
        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangnhap(FormCollection collection)
        {
            // Gán các giá trị người dùng nhập liệu cho các biến
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                //Gần giá trị cho đối tượng được tạo mới (kh)
                Admin ad = data.Admins.SingleOrDefault(n => n.UserAdmin == tendn && n.PassAdmin == matkhau);
                if (ad != null)
                {
                    Session["Taikhoanadmin"] = ad;
                    return RedirectToAction("Index", "Admin");
                }
                KHACHHANG Kh = data.KHACHHANGs.SingleOrDefault(n => n.Taikhoan == tendn && n.MATKHAU == matkhau);
                if (Kh != null)
                {
                    ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["TaiKhoan"] = Kh;
                    Session["Hoten"] = Kh.HoTen;
                    return RedirectToAction("Index", "MovieStore");

                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return this.Dangnhap();


        }
    }
}