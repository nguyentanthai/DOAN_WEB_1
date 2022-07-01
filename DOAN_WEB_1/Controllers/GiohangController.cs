using DOAN_WEB_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOAN_WEB_1.Controllers
{
    public class GiohangController : Controller
    {
        // GET: GioHang
        DataQLWebIPDataContext data = new DataQLWebIPDataContext();
        // GET: Giohang
        public List<Giohang> Laygiohang()
        {
            List<Giohang> listGioHang = Session["Giohang"] as List<Giohang>;
            if (listGioHang == null)
            {
                listGioHang = new List<Giohang>();
                Session["Giohang"] = listGioHang;
            }
            return listGioHang;
        }
        public ActionResult ThemGioHang(int iMaSP, string strURL)
        {
            List<Giohang> listGioHang = Laygiohang();
            Giohang sanpham = listGioHang.Find(n => n.iMaSP == iMaSP);
            if (sanpham == null)
            {
                sanpham = new Giohang(iMaSP);
                listGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoLuong++;
                return Redirect(strURL);
            }
        }
        public ActionResult GioHang()
        {
            List<Giohang> listGioHang = Laygiohang();
            if (listGioHang.Count == 0)
            {
                return RedirectToAction("Index", "OpLungStore");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(listGioHang);
        }
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<Giohang> listGioHang = Session["GioHang"] as List<Giohang>;
            if (listGioHang != null)
            {
                iTongSoLuong = listGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        private double TongTien()
        {
            double iTongTien = 0;
            List<Giohang> listGioHang = Session["GioHang"] as List<Giohang>;
            if (listGioHang != null)
            {
                iTongTien = listGioHang.Sum(n => n.dThanhTien);
            }
            return iTongTien;
        }

        public ActionResult GioHangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView();
        }
        public ActionResult XoaGioHang(int iMaSP)
        {
            List<Giohang> listGioHang = Laygiohang();
            Giohang sanpham = listGioHang.SingleOrDefault(n => n.iMaSP == iMaSP);
            if (sanpham != null)
            {
                listGioHang.RemoveAll(n => n.iMaSP == iMaSP);
                return RedirectToAction("GioHang");
            }
            if (listGioHang.Count == 0)
            {
                return RedirectToAction("Index", "OpLungStore");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult CapnhatGioHang(int iMaSP, FormCollection f)
        {
            List<Giohang> listGioHang = Laygiohang();
            Giohang sanpham = listGioHang.SingleOrDefault(n => n.iMaSP == iMaSP);
            if (sanpham != null)
            {
                sanpham.iSoLuong = int.Parse(f["textSoLuong"].ToString());
            }
            return RedirectToAction("Giohang");
        }
        public ActionResult XoaTatcaGioHang()
        {
            List<Giohang> listGioHang = Laygiohang();
            listGioHang.Clear();
            return RedirectToAction("Index", "OpLungStore");
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "Nguoidung");
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "OpLungStore");
            }
            List<Giohang> listGioHang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(listGioHang);
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection collection)
        {
            DONDATHANG ddh = new DONDATHANG();
            KHACHHANG kh = (KHACHHANG)Session["TaiKhoan"];
            List<Giohang> gh = Laygiohang();
            ddh.MaKH = kh.MaKH;
            ddh.NgayDH = DateTime.Now;
            
            ddh.TinhTrangGH = false;
            ddh.Dathanhtoan = false;
            data.DONDATHANGs.InsertOnSubmit(ddh);
            data.SubmitChanges();
            foreach (var item in gh)
            {
                CTDATHANG ctdh = new CTDATHANG();
                ctdh.MaDH = ddh.MaDH;
                ctdh.MaSP = item.iMaSP;
                ctdh.SoLuong = item.iSoLuong;
                ctdh.DonGia = (decimal)item.dDonGia;
                data.CTDATHANGs.InsertOnSubmit(ctdh);
            }
            data.SubmitChanges();
            Session["GioHang"] = null;
            return RedirectToAction("Xacnhandonhang", "Giohang");
        }
        public ActionResult Xacnhandonhang()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}
