using BookShopOnline.Areas.Admin.Data;
using BookShopOnline.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShopOnline.Areas.Admin.Controllers
{
    public class QLSachAdminController : Controller
    {
        // GET: Admin/QLSachAdmin
        private ModelBookShop _context = new ModelBookShop();
        public ActionResult Index()
        {
            var lstBook = (from s in _context.Saches
                          join cd in _context.ChuDes on s.MaChuDe equals cd.MaChuDe into cdtemp
                          join nxb in _context.NhaXuatBans on s.MaNXB equals nxb.MaNXB into nxbtemp
                          from cdf in cdtemp.DefaultIfEmpty()
                          from nxbf in nxbtemp.DefaultIfEmpty()
                          orderby s.NgayCapNhat descending
                          select new SachDisplayVM()
                          {
                              MaSach = s.MaSach,
                              TenSach = s.TenSach,
                              GiaBan = s.GiaBan,
                              NgayCapNhat = s.NgayCapNhat,
                              TenChuDe = cdf != null ? cdf.TenChuDe : "",
                              TenNhaXuatBan = nxbf != null ? nxbf.TenNXB : "",
                              AnhBia = s.AnhBia,
                              SoLuongTon = s.SoLuongTon
                          }).ToList();
            ViewBag.message = lstBook.Count();
            return View(lstBook);
        }

        [HttpGet]
        public ActionResult AddBook()
        {
            var lstChuDe = _context.ChuDes.OrderBy(x => x.TenChuDe).ToList();
            var lstNXB = _context.NhaXuatBans.OrderBy(x => x.TenNXB).ToList();

            ViewBag.MaChuDe = new SelectList(lstChuDe, "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(lstNXB, "MaNXB", "TenNXB");
            return View();
        }

        [HttpPost]
        public ActionResult AddBook(SachVM formData, HttpPostedFileBase fileUpload)
        {
            var itemNew = new Sach();
            itemNew.TenSach = formData.TenSach;
            itemNew.GiaBan = formData.GiaBan;
            itemNew.MoTa = formData.MoTa;
            itemNew.SoLuongTon = formData.SoLuongTon;
            itemNew.MaChuDe = formData.MaChuDe;
            itemNew.MaNXB = formData.MaNXB;
            //itemNew.NgayCapNhat = DateTime.Now;
            itemNew.NgayCapNhat = formData.NgayCapNhat;
            itemNew.Moi = 1;
            //itemNew.AnhBia = "";

            // get filename
            var fileName = System.IO.Path.GetFileName(fileUpload.FileName);
            //get path
            var path = Path.Combine(Server.MapPath("~/Images/Image_Books/"), fileName);

            // ktra image exist
            if (System.IO.File.Exists(path))
            {
                ViewBag.message = "Ảnh này đã tồn tại!";
            }
            else
            {
                fileUpload.SaveAs(path);
            }

            //lưu file name vào DB ảnh bìa
            itemNew.AnhBia = fileName;

            _context.Saches.Add(itemNew);
            _context.SaveChanges();

            return RedirectToAction("Index", "QLSachAdmin");
        }

        [HttpGet]
        public ActionResult DeleteBook(int id)
        {
            var item = _context.Saches.Where(x => x.MaSach == id).Select(x => new SachVM()
            {
                MaSach = x.MaSach,
                TenSach = x.TenSach,
            }).FirstOrDefault();
            if(item == null)
            {
                RedirectToAction("Index", "QLSachAdmin");
            }
            return View(item);
        }

        [HttpPost, ActionName("DeleteBook")]
        public ActionResult ConfirmDeleteBook(int id)
        {
            var item = _context.Saches.Where(x => x.MaSach == id).FirstOrDefault();
            if (item == null)
            {
                RedirectToAction("Index", "QLSachAdmin");
            }

            _context.Saches.Remove(item);

            _context.SaveChanges();

            return RedirectToAction("Index", "QLSachAdmin");
        }
    }
}