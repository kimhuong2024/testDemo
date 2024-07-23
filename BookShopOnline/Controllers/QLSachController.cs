using BookShopOnline.Models;
using BookShopOnline.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace BookShopOnline.Controllers
{
    public class QLSachController : Controller
    {
        // GET: QLSach
        private ModelBookShop _context = new ModelBookShop();
        //public ActionResult Index()
        //{
        //    //var lstBooks = _context.Saches.Where(x=>x.Moi == 1).OrderByDescending(t => t.GiaBan).ToList();
        //    var lstBooks = (from bk in _context.Saches
        //                    where bk.Moi == 1
        //                    orderby bk.GiaBan descending
        //                    select bk
        //                    ).ToList();

        //    int sluong = lstBooks.Count();
        //    ViewBag.soluong = sluong;
        //    return View(lstBooks);
        //}

        public ActionResult Index(int? page)
        {
            // số sp trên trang
            int pageSize = 9;
            // số trang
            int pageNumber = (page ?? 1);

            //var lstBooks = _context.Saches.Where(x=>x.Moi == 1).OrderByDescending(t => t.GiaBan).ToList();
            var lstBooks = (from bk in _context.Saches
                            where bk.Moi == 1
                            orderby bk.GiaBan descending
                            select bk
                            ).ToPagedList(pageNumber,pageSize);

            int sluong = lstBooks.Count();
            ViewBag.soluong = sluong;
            return View(lstBooks);
        }

        public ActionResult GetBooksByTopic(int maCD)
        {
            var lstBooks = _context.Saches.Where(x => x.MaChuDe == maCD).OrderByDescending(p => p.GiaBan).ToList();
            ViewBag.soluong = lstBooks.Count();
            return View(lstBooks);
        }

        public ActionResult DetailBook(int maSach)
        {
            //var book = _context.Saches.Where(x => x.MaSach == maSach).FirstOrDefault();
            var book = (from s in _context.Saches
                        join cd in _context.ChuDes on s.MaChuDe equals cd.MaChuDe
                        join nxb in _context.NhaXuatBans on s.MaNXB equals nxb.MaNXB
                        where s.MaSach == maSach
                        select new SachViewModel()
                        {
                            TenSach = s.TenSach,
                            GiaBan = s.GiaBan,
                            MoTa = s.MoTa,
                            SoLuongCon = s.SoLuongTon,
                            NgayNhap = s.NgayCapNhat,
                            AnhBia = s.AnhBia,
                            TenCD = cd.TenChuDe,
                            TenNXB = nxb.TenNXB
                        }).FirstOrDefault();
            if(book == null)
            {
                ViewBag.message = "Sách này không tồn tại";
                return RedirectToAction("Index", "QLSach");
            }

            return View(book);
        }

public ActionResult DetailBook1()
        {            

            return View();
        }
    }
	
	public ActionResult DetailBook2()
        {            

            return View();
        }
		
		public ActionResult DetailBook3()
        {            

            return View();
        }
    }
}