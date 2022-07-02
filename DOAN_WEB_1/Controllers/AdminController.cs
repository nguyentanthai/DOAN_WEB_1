using DOAN_WEB_1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Data.Entity;
namespace DOAN_WEB_1.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        DataQLWebIPDataContext db = new DataQLWebIPDataContext();

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult SanPham(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            //return View(db.SACHes.ToList());
            return View(db.SANPHAMs.ToList().OrderBy(n => n.MaSP).ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var tendn = collection["username"];
            var matkhau = collection["password"];
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
                Admin ad = db.Admins.SingleOrDefault(n => n.UserAdmin == tendn && n.PassAdmin == matkhau);
                if (ad != null)
                    if (ad != null)
                    {
                        Session["Taikhoanadmin"] = ad;
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                        ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }

        [HttpGet]
        public ActionResult ThemMoiSanPham()
        {


            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoiSanPham(SANPHAM sanpham, HttpPostedFileBase fileUpload)
        {
            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            //Them vao CSDL
            else
            {
                if (ModelState.IsValid)
                {
                    //Luu ten fie, luu y bo sung thu vien using System.IO;
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    //Luu duong dan cua file
                    var path = Path.Combine(Server.MapPath("~/Content/images"), fileName);
                    //Kiem tra hình anh ton tai chua?
                    if (System.IO.File.Exists(path))
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    else
                    {
                        //Luu hinh anh vao duong dan
                        fileUpload.SaveAs(path);
                    }
                    sanpham.HinhAnh = fileName;
                    //Luu vao CSDL
                    db.SANPHAMs.InsertOnSubmit(sanpham);
                    db.SubmitChanges();
                }
                return RedirectToAction("SanPham");
            }

        }
        public ActionResult Chitietsanpham(int id)
        {
            //Lay ra doi tuong sach theo ma
            SANPHAM sach = db.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            ViewBag.Masach = sach.MaSP;
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        [HttpGet]
        public ActionResult XoaSanPham(int id)
        {
            //Lay ra doi tuong sach can xoa theo ma
            SANPHAM sanpham = db.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            ViewBag.Masach = sanpham.MaSP;
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanpham);
        }
        [HttpPost, ActionName("XoaSanPham")]
        public ActionResult Xacnhanxoa(int id)
        {
            //Lay ra doi tuong sach can xoa theo ma
            SANPHAM sanpham = db.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            ViewBag.Masach = sanpham.MaSP;
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.SANPHAMs.DeleteOnSubmit(sanpham);
            db.SubmitChanges();
            return RedirectToAction("SanPham");
        }
        [HttpGet]
        public ActionResult SuaSanPham(int id)
        {
            //Lay ra doi tuong sach theo ma
            SANPHAM sanpham = db.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            ViewBag.Masach = sanpham.MaSP;
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanpham);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaSanPham(SANPHAM sanpham, HttpPostedFileBase fileUpload)
        {
           
            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            //Them vao CSDL
            else
            {
                if (ModelState.IsValid)
                {
                    
                    var fileName = Path.GetFileName(fileUpload.FileName);
                  
                    var path = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                    //Kiem tra hình anh ton tai chua?
                    if (System.IO.File.Exists(path))
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    else
                    {
                        //Luu hinh anh vao duong dan
                        fileUpload.SaveAs(path);
                    }
                    sanpham.HinhAnh = fileName;
                    //Luu vao CSDL   
                    UpdateModel(sanpham);
                    db.SubmitChanges();

                }
                return RedirectToAction("SanPham");
            }
        }
    }
}
