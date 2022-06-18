using DOAN_WEB_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOAN_WEB_1.Controllers
{
    public class IphoneStoreController : Controller
    {
        // GET: IphoneStore
        dbIphoneDataContext data = new dbIphoneDataContext();

        public ActionResult Index()
        {
            var iphonemoi = LayIphoneMoi(7);
            return View(iphonemoi);
        }
        private List<SanPham> LayIphoneMoi(int count)
        {
            return data.SanPhams.OrderByDescending(a => a.MaSP).Take(count).ToList();
         }

    }
}