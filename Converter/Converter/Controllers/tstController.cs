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
            int a = district1.Count();
            return Json(new SelectList(district1.ToList<Category>(), "Id", "Category1"), JsonRequestBehavior.AllowGet);
        }

        public ActionResult List()
        {
            List<Category> cat = new List<Category>();
            foreach (var item in contex.Category)
            {
                cat.Add(item);
            }
            return View(cat);
        }


        
        public ActionResult Edit(int Id)
        {
            ViewBag.Title = "Edit";
            var item = contex.Category.Where(model => model.Id == Id).FirstOrDefault<Category>();
            var rows = from type in contex.CategorySet
                       select type;
            ViewData["Types"] = new SelectList(rows, "Id", "Category_Type", contex.CategorySet);

            
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(Category cat)
        {
            ViewBag.Title = "Edit";
            var item = contex.Category.Where(model => model.Id == cat.Id).FirstOrDefault<Category>();
            if (item != null)
            {
                item.Category1 =cat.Category1;
                item.Value = cat.Value;
                
                int TypeId = Int32.Parse(Request.Form["TypeId"]);

                item.CategoryId = TypeId;
                contex.SaveChanges();
            }
            //var rows = from type in contex.CategorySet
            //           select type;
            //ViewData["Types"] = new SelectList(rows, "Id", "Category_Type", contex.CategorySet);
            
            return RedirectToAction("List");
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

            double res = (i * item1.Value) * item.Value;
            ViewBag.MyParam = res.ToString();

            List<SelectListItem> state = new List<SelectListItem>();
            foreach (var item4 in contex.CategorySet)
            {
                state.Add(new SelectListItem { Text = item4.Category_Type, Value = item4.Id.ToString() });
            }
            ViewBag.StateName = new SelectList(state, "Value", "Text");
            return View();
        }

        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(string name)
        {
            CategorySet catSet = new CategorySet();
            catSet.Category_Type = name;
            if (catSet.Category_Type != null)
            {
                contex.CategorySet.Add(catSet);
                contex.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult AddParams()
        {
            var rows = from type in contex.CategorySet
                       select type;
            ViewData["Types"] = new SelectList(rows, "Id", "Category_Type", contex.CategorySet);

            var rows1 = from type1 in contex.Category
                        select type1;
            var user = contex.Category.Include("CategoryId");
            ViewData["Types1"] = new SelectList(rows1, "Id", "Category1", contex.Category);
            return View();
        }

        [HttpPost]
        public ActionResult AddParams(Category cat)
        {
            Category category = new Category();
            if (cat != null)
            {
                category.Category1 = cat.Category1;
                category.CategoryId = cat.CategoryId;
                category.Value = cat.Value;

                contex.Category.Add(category);
                contex.SaveChanges();
            }
            return RedirectToAction("List");
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