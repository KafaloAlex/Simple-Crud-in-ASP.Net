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
    public class ProductController : Controller
    {
        // GET: Product
        BD_CATALOGUE_Entities db = new BD_CATALOGUE_Entities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddProduct()
        {
            try
            {
                ViewBag.ProductList = db.CAT_PRODUIT.ToList();
                ViewBag.CategoryList = db.CAT_CATEGORIE.ToList();
                return View();
            }
            catch(Exception ex)
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult AddProduct(CAT_PRODUIT product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(Request.Files.Count > 0)//Vérifie si un fichier est présent
                    {
                        var file = Request.Files[0];
                        if(file != null && file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);//Récupération du nom du fichier
                            var path = Path.Combine(Server.MapPath("~/Files"), fileName);//Chemin d'accès au fichier
                            file.SaveAs(path);//Enregistrement du fichier dans le dossier FILES

                            product.IMAGE_PRODUIT = fileName;
                            product.URL_IMAGE = "/Files";


                        }
                    }
                    product.DATE_SAISIE = DateTime.Now;
                    db.CAT_PRODUIT.Add(product);
                    db.SaveChanges();
                }
                return RedirectToAction("AddProduct");
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        //Suppression d'un produit
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                CAT_PRODUIT product = db.CAT_PRODUIT.Find(id);
                if (product != null)
                {
                    db.CAT_PRODUIT.Remove(product);
                    db.SaveChanges();
                }

                return RedirectToAction("AddProduct");
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        //Modification d'un produit
        public ActionResult ModifyProduct(int id)
        {
            try
            {
                ViewBag.CategoryList = db.CAT_CATEGORIE.ToList();
                ViewBag.ProductList = db.CAT_PRODUIT.ToList();
                CAT_PRODUIT product = db.CAT_PRODUIT.Find(id);
                if (product != null)
                    return View("AddProduct", product);
                return RedirectToAction("Addproduct");
            }
            catch(Exception ex)
            {
                return HttpNotFound();
            }
        }

        //Sauvegarde des Modifications
        [HttpPost]
        public ActionResult ModifyProduct(CAT_PRODUIT product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Request.Files.Count > 0)//Vérifie si un fichier est présent
                    {
                        var file = Request.Files[0];
                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);//Récupération du nom du fichier
                            var path = Path.Combine(Server.MapPath("~/Files"), fileName);//Chemin d'accès au fichier
                            file.SaveAs(path);//Enregistrement du fichier dans le dossier FILES

                            product.IMAGE_PRODUIT = fileName;
                            product.URL_IMAGE = "/Files";


                        }
                        else
                        {
                            var fileName = Path.GetFileName(file.FileName);//Récupération du nom du fichier
                            var path = Path.Combine(Server.MapPath("~/Files"), fileName);//Chemin d'accès au fichier
                            file.SaveAs(path);//Enregistrement du fichier dans le dossier FILES

                            product.IMAGE_PRODUIT = fileName;
                            product.URL_IMAGE = "/Files";
                        }
                        
                    }
                    product.DATE_SAISIE = DateTime.Now;
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("AddProduct");

            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }
    }
}