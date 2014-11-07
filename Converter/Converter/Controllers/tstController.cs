using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Converter.Models;

namespace Converter.Controllers
{
    public class tstController : Controller
    {

        db1Entities contex = new db1Entities();
        public ActionResult Index()
        {
            List<SelectListItem> state = new List<SelectListItem>();
            foreach (var item in contex.CategorySet)
            {
                state.Add(new SelectListItem { Text = item.Category_Type, Value = item.Id.ToString() });
            }
            ViewBag.StateName = new SelectList(state, "Value", "Text");

            return View();
        }

        public JsonResult DistrictList(string Id)
        {
            int v = int.Parse(Id);
            var district = from s in contex.Category
                           where s.CategoryId == v
                           select s;
            int a = district.Count();
            return Json(new SelectList(district.ToList<Category>(), "Id", "Category1"), JsonRequestBehavior.AllowGet);
        }
        public JsonResult DistrictList1(string Id)
        {
            int v = int.Parse(Id);
            Category cat = new Category();
            
            var i = contex.Category.Where(model => model.Id == v).FirstOrDefault<Category>();

            int tmp = i.CategoryId;

            var district1 = from s in contex.Category
                           where s.CategoryId == tmp && s.Id != v
                           select s;
            //var  = from sc in ska
            //          where sc.Id!= v
            //          select sc;
            int a = district1.Count();
            return Json(new SelectList(district1.ToList<Category>(), "Id", "Category1"), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Index(string Name, string State, string District, string District1)
        {
            string tmp = State;
            int tmp1 = int.Parse(District);
            int tmp2 = int.Parse(District1);

            var item = contex.Category.Where(model => model.Id == tmp1).FirstOrDefault<Category>();
            var item1 = contex.Category.Where(model => model.Id == tmp2).FirstOrDefault<Category>();

            //string selected = Request.Form["Category1"];
            //string selected1 = Request.Form["Category"];
            //int p = int.Parse(selected);
            //var item = contex.Category.Where(model => model.Id == p).FirstOrDefault<Category>();

            int i = 0;
            i = int.Parse(Name);

            double res = i * item.Value;
            ViewBag.MyParam = res.ToString();
            List<SelectListItem> state = new List<SelectListItem>();
            foreach (var item4 in contex.CategorySet)
            {
                state.Add(new SelectListItem { Text = item4.Category_Type, Value = item4.Id.ToString() });
            }
            ViewBag.StateName = new SelectList(state, "Value", "Text");
            return View();
        }

        //[HttpPost]
        //public ActionResult Index(CategorySet formdata)
        //{
        //    if (formdata.Category_Type == null)
        //    {
        //        ModelState.AddModelError("Name", "Name is required field.");
        //    }
        //    if (formdata.Id == null)
        //    {
        //        ModelState.AddModelError("State", "State is required field.");
        //    }
        //    if (formdata.Category == null)
        //    {
        //        ModelState.AddModelError("District", "District is required field.");
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        ////Populate the list again
        //        //List<SelectListItem> state = new List<SelectListItem>();
        //        //state.Add(new SelectListItem { Text = "Bihar", Value = "Bihar" });
        //        //state.Add(new SelectListItem { Text = "Jharkhand", Value = "Jharkhand" });
        //        //ViewBag.StateName = new SelectList(state, "Value", "Text");

        //        return View("Index");
        //    }

        //    //TODO: Database Insertion

        //    return RedirectToAction("Index", "Home");
        //}
    }
}