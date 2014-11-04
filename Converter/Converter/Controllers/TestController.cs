using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Converter.Models;

namespace Converter.Controllers
{
    public class TestController : Controller
    {
        db1Entities context = new db1Entities();
        Category categ = new Category();
        //
        // GET: /Test/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Test()
        {
            var rows = from type in context.CategorySet
                       select type;
            ViewData["Types"] = new SelectList(rows, "Id", "Category_Type", context.CategorySet);
            return View();
        }
        [HttpPost]
        public ActionResult Test(string TB)
        {
            int i = 0;
            i = int.Parse(TB);
            var rows = from type in context.CategorySet
                       select type;
            ViewData["Types"] = new SelectList(rows, "Id", "Category_Type", context.CategorySet);
            
            double res = i * categ.Value;
            ViewBag.MyParam = res.ToString();
            return View();
        }
	}
}