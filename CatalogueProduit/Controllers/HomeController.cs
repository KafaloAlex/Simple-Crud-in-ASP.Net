using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using CatalogueProduit.Models;

namespace CatalogueProduit.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        BD_CATALOGUE_Entities db = new BD_CATALOGUE_Entities();
        public ActionResult Index()
        {
            ViewBag.CategoryList = db.CAT_CATEGORIE.ToList().OrderBy(i => i.LIBELLE_CATEGORIE);
            return View();
        }
    }
}