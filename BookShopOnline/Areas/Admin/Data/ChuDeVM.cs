using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookShopOnline.Areas.Admin.Data
{
    public class ChuDeVM
    {
        [Display(Name ="Mã chủ đề")]
        public int MaCD { get; set; }
        [Display(Name ="Tên chủ đề")]
        public string TenCD { get; set; }
    }
}