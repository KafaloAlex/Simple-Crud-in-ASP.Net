using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using CatalogueProduit.Models;

namespace CatalogueProduit.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        BD_CATALOGUE_Entities db = new BD_CATALOGUE_Entities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddCategory()
        {
            try
            {
                ViewBag.CategoryList = db.CAT_CATEGORIE.ToList();
                return View();
            }
            catch(Exception ex)
            {
                return HttpNotFound();
            }
        }

        //Enregistrement d'une categorie
        [HttpPost]
        public ActionResult AddCategory(CAT_CATEGORIE category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    category.DATE_SAISIE = DateTime.Now;
                    db.CAT_CATEGORIE.Add(category);
                    db.SaveChanges();
                }
                return RedirectToAction("AddCategory");
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        //Suppression d'une catégorie
        public ActionResult DeleteCategory(int id)
        {
            try
            {
                CAT_CATEGORIE category = db.CAT_CATEGORIE.Find(id);
                if(category != null)
                {
                    db.CAT_CATEGORIE.Remove(category);
                    db.SaveChanges();
                }

                return RedirectToAction("AddCategory");
            }
            catch(Exception ex)
            {
                return HttpNotFound();
            }
        }

        //Modification d'une catégorie
        public ActionResult ModifyCategory(int id)
        {
            
            try
            {
                ViewBag.CategoryList = db.CAT_CATEGORIE.ToList();
                CAT_CATEGORIE category = db.CAT_CATEGORIE.Find(id);
                if (category != null)
                    return View("AddCategory", category);
                return RedirectToAction("AddCategory");
            }
            catch(Exception ex)
            {
                return HttpNotFound();
            }
        }

        //Sauvegarder de la modification
        [HttpPost]
        public ActionResult ModifyCategory(CAT_CATEGORIE category)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    category.DATE_SAISIE = DateTime.Now;
                    db.Entry(category).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("AddCategory");
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }
    }
}