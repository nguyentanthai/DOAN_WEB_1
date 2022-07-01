using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOAN_WEB_1.Models;

namespace DOAN_WEB_1.Controllers
{
    public class TimKiemController : Controller
    {
        // GET: TimKiem
        DataQLWebIPDataContext db = new DataQLWebIPDataContext();
        public ActionResult KQTimKiem( string sTuKhoa)
        {
            var lstSP = db.SANPHAMs.Where(n => n.TenSP.Contains(sTuKhoa));
            return View(lstSP.OrderBy(n =>n.TenSP));
        }
    }
}