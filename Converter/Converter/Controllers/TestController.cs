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

            var rows1 = from type1 in context.Category
                        select type1;
            //var user = context.Category.Include("Category").FirstOrDefault();
            ViewData["Types1"] = new SelectList(rows1, "Id", "Category1", context.Category);

            
            return View();
        }
        [HttpPost]
        public ActionResult Test(Category category, string TB)
        {
            string selected = Request.Form["Category1"];
            int p = int.Parse(selected);

            var item = context.Category.Where(model => model.Id == p).FirstOrDefault<Category>();

            int i = 0;
            i = int.Parse(TB);

            var rows = from type in context.CategorySet
                       select type;
            ViewData["Types"] = new SelectList(rows, "Id", "Category_Type", context.CategorySet);

            var rows1 = from type1 in context.Category
                        select type1;
            var user = context.Category.Include("CategoryId");
            ViewData["Types1"] = new SelectList(rows1, "Id", "Category1", context.Category);

            double res = i * item.Value;
            ViewBag.MyParam = res.ToString();
            return View();
        }
	}
}