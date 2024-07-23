using BookShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShopOnline.Controllers
{
    public class QLChuDeController : Controller
    {
        // GET: QLChuDe
        private ModelBookShop _context = new ModelBookShop();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PartalChuDe()
        {
            var lstCD = _context.ChuDes.Take(10).OrderBy(x => x.TenChuDe).ToList();
            return PartialView(lstCD);
        }
    }
}