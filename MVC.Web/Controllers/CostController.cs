using MVC.Data;
using MVC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Web.Controllers
{
    public class CostController : Controller
    {
        // GET: Cost
        AppDataContext db = new AppDataContext();
        public ActionResult Index()
        {           
            return View();
        }
        public ActionResult Create()
        {
            Cost cost = new Cost();
            cost.Id = 0;
            return PartialView("_CreateOrUpdateCost", cost);
        }
        public ActionResult Edit(int Id)
        {
            Cost cost = new Cost();
            cost.Id = Id;
            return PartialView("_CreateOrUpdateCost", cost);
        }
    }
}