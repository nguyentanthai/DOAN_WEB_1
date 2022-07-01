using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOAN_WEB_1.Models
{
    public class Giohang
    {
        DataQLWebIPDataContext db = new DataQLWebIPDataContext();
        public int iMaSP { get; set; }
        public string sTenSP { get; set; }
        public string sHinhAnh { get; set; }
        public Double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public Double dThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }
        public Giohang(int MaSP)
        {
            iMaSP = MaSP;
            SANPHAM sanpham = db.SANPHAMs.Single(n => n.MaSP == iMaSP);
            sTenSP = sanpham.TenSP;
            sHinhAnh = sanpham.HinhAnh;
            dDonGia = double.Parse(sanpham.GiaBan.ToString());
            iSoLuong = 1;
        }
    }
}