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
            return Json(new SelectList(district.ToList<Category>(), "CategoryId", "Category1"), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Index(CategorySet formdata)
        {
            if (formdata.Category_Type == null)
            {
                ModelState.AddModelError("Name", "Name is required field.");
            }
            if (formdata.Id == null)
            {
                ModelState.AddModelError("State", "State is required field.");
            }
            if (formdata.Category == null)
            {
                ModelState.AddModelError("District", "District is required field.");
            }

            if (!ModelState.IsValid)
            {
                //Populate the list again
                List<SelectListItem> state = new List<SelectListItem>();
                state.Add(new SelectListItem { Text = "Bihar", Value = "Bihar" });
                state.Add(new SelectListItem { Text = "Jharkhand", Value = "Jharkhand" });
                ViewBag.StateName = new SelectList(state, "Value", "Text");

                return View("Index");
            }

            //TODO: Database Insertion

            return RedirectToAction("Index", "Home");
        }
    }
}