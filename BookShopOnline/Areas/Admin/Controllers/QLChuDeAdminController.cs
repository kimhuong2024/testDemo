using BookShopOnline.Areas.Admin.Data;
using BookShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShopOnline.Areas.Admin.Controllers
{
    public class QLChuDeAdminController : Controller
    {
        // GET: Admin/QLChuDeAdmin
        private ModelBookShop _context = new ModelBookShop();

        public ActionResult Index()
        {
            var lstCD = (from cd in _context.ChuDes
                        orderby cd.MaChuDe descending
                        select new ChuDeVM()
                        {
                            MaCD = cd.MaChuDe,
                            TenCD = cd.TenChuDe
                        }).ToList();
            return View(lstCD);
        }

        [HttpGet]
        public ActionResult AddChuDe()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddChuDe(ChuDeVM formData)
        {
            var item = new ChuDe();
            item.TenChuDe = formData.TenCD;

            _context.ChuDes.Add(item);

            _context.SaveChanges(); // save to DB

            return RedirectToAction("Index", "QLChuDeAdmin");

        }

        [HttpGet]
        public ActionResult EditChuDe(int id)
        {
            //var cd = _context.ChuDes.Where(x => x.MaChuDe == id).Select(item => new ChuDeVM()
            //{
            //    MaCD = item.MaChuDe,
            //    TenCD = item.TenChuDe
            //}).FirstOrDefault();

            var cd = (from item in _context.ChuDes
                      where item.MaChuDe == id
                      select new ChuDeVM()
                      {
                          MaCD = item.MaChuDe,
                          TenCD = item.TenChuDe
                      }).FirstOrDefault();
            
            if(cd == null)
            {
                return RedirectToAction("Index", "QLChuDeAdmin");
            }
            return View(cd);
        }

        [HttpPost]
        public ActionResult EditChuDe(ChuDeVM formData)
        {
            var item = _context.ChuDes.Where(x => x.MaChuDe == formData.MaCD).FirstOrDefault();

            if(item == null)
            {
                return RedirectToAction("Index", "QLChuDeAdmin");
            }

            item.TenChuDe = formData.TenCD;

            _context.SaveChanges();

            return RedirectToAction("Index", "QLChuDeAdmin");
        }

        public ActionResult DetaiChuDe(int id)
        {
            var item = _context.ChuDes.Where(x => x.MaChuDe == id).Select(t => new ChuDeVM()
            {
                MaCD = t.MaChuDe,
                TenCD = t.TenChuDe
            }).FirstOrDefault();
            return View(item);
        }

        [HttpGet]
        public ActionResult DeleteChuDe(int id)
        {
            var item = _context.ChuDes.Where(x => x.MaChuDe == id).Select(x => new ChuDeVM() { 
                MaCD = x.MaChuDe,
                TenCD = x.TenChuDe
            }).FirstOrDefault();
            return View(item);
        }

        [HttpPost, ActionName("DeleteChuDe")]
        public ActionResult ConfrimDeleteChuDe(int id)
        {
            var item = _context.ChuDes.Where(x => x.MaChuDe == id).FirstOrDefault();

            if(item == null)
            {
                return RedirectToAction("Index", "QLChuDeAdmin");
            }
            _context.ChuDes.Remove(item);

            _context.SaveChanges();

            return RedirectToAction("Index", "QLChuDeAdmin");
        }

    }
}