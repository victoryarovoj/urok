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

            ActionModel model = new ActionModel();
            IEnumerable<ActionType> actionTypes = Enum.GetValues(typeof(ActionType))
                                                       .Cast<ActionType>();
            model.ActionsList = from action in actionTypes
                                select new SelectListItem
                                {
                                    Text = action.ToString(),
                                    Value = ((int)action).ToString()
                                };

            return View();
        }
        [HttpPost]
        public ActionResult Test(Category category, string TB)
        {
            
            int i = 0;
            i = int.Parse(TB);
            var item = context.Category.Where(model => model.Category1 == category.Category1).FirstOrDefault();
            //var rows = from type in context.CategorySet
            //           select type;
            //ViewData["Type"] = new SelectList(rows, "Id", "Category", context.Category);

            //var rows1 = from type1 in context.Category
            //           select type1;
            //var user = context.Category.Include("Category").FirstOrDefault();
            //ViewData["Types1"] = new SelectList(rows1, "Id", "Category", user.Category1);

            double res = i * item.Value;
            ViewBag.MyParam = res.ToString();
            return View();
        }
	}
}