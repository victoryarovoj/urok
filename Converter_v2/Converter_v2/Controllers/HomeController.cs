using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Converter_v2.Models;

namespace Converter_v2.Controllers
{
    public class HomeController : Controller
    {
        ConverterEntities context = new ConverterEntities();
        //
        // GET: /Home/
        public ActionResult Index()
        {
            List<SelectListItem> state = new List<SelectListItem>();
            foreach (var item in context.Categories)
            {
                state.Add(new SelectListItem { Text = item.Category1, Value = item.Id.ToString() });
            }
            ViewBag.StateName = new SelectList(state, "Value", "Text");
            return View();
        }
        public JsonResult DistrictList(string Id)
        {
            int v = int.Parse(Id);
            var district = from s in context.Constants
                           where s.Category == v
                           select s;
            int a = district.Count();
            return Json(new SelectList(district.ToList<Constant>(), "Id", "Category"), JsonRequestBehavior.AllowGet);
        }
        public JsonResult DistrictList1(string Id)
        {
            int v = int.Parse(Id);
            Category cat = new Category();

            var i = context.Constants.Where(model => model.Id == v).FirstOrDefault<Constant>();

            int tmp = i.Category;

            var district1 = from s in context.Constants
                            where s.Category == tmp && s.Id != v
                            select s;
            int a = district1.Count();
            return Json(new SelectList(district1.ToList<Constant>(), "Id", "Category"), JsonRequestBehavior.AllowGet);
        }
	}
}