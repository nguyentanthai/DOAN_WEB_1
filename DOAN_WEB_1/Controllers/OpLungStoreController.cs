using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Documents;
using DOAN_WEB_1.Models;

namespace DOAN_WEB_1.Controllers
{
    public class OpLungStoreController : Controller
    {
        // GET: OpLungStore
        DataQLWebIPDataContext data = new DataQLWebIPDataContext();
        private List<SANPHAM>SanPhamMoi(int count)
        {
            return data.SANPHAMs.OrderByDescending(a => a.MaSP).Take(count).ToList();
        }
        public ActionResult Index()
        {
            var SPMoi = SanPhamMoi(6);
            return View(SPMoi);
        }
        
        public ActionResult Details(int id)
        {
            var sanpham = from s in data.SANPHAMs
                          where s.MaSP == id
                          select s;
                         return View(sanpham.Single());
        }
    }
}