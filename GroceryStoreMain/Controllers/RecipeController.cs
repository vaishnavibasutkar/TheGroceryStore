using GroceryStoreMain.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Web;
using System.Web.Mvc;

namespace GroceryStoreMain.Controllers
{
    public class RecipeController : Controller
    {
        GroceryStoreDBEntities context;
        public RecipeController()
        {
            context = new GroceryStoreDBEntities();
        }
        // GET: Recipe
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index1()
        {
            return View();
        }

        #region Distributor
        /// <summary>
        /// 1. Add recipe
        /// </summary>

        public ActionResult AddNewRecipe()
        {
            if (Session["Username"] != null)//check for role
            {
                //ViewBag.ProductCategory = this.context.Product_Category.ToList();
                //ViewBag.Distributor = this.context.Distributors.ToList();
                //ViewBag.UOM = this.context.Unit_Of_Measurement.ToList();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Distributor");
            }
        }

        [HttpPost]
        //[AntiForgeryToken]   
        public ActionResult AddNewRecipe(Recipe recipe)
        {
            if (Session["Username"] != null)
            {
                if (context.Recipes.Where(r => r.name == recipe.name).Any())
                {
                    TempData["Message"] = "Recipe Name Already Exist.";
                    return View();
                }

                recipe.created_by = Session["Username"].ToString();
                recipe.created_dtm = DateTime.Now;
                recipe.modified_by = Session["Username"].ToString();
                recipe.modified_dtm = DateTime.Now;
                context.Recipes.Add(recipe);
                context.SaveChanges();
                var recipeId = context.Recipes.FirstOrDefault(r => r.name == recipe.name).r_id;
                //var fileName = Guid.NewGuid().ToString() + Path.GetFileName(productModel.ImageFile.FileName);
                //string UploadPath = ConfigurationManager.AppSettings["ProductImagePath"].ToString();
                //var path = Path.Combine(UploadPath, fileName);
                //// store the uploaded file on the file system
                //productModel.ImageFile.SaveAs(path);


                //Product product = new Product();
                //product.name = productModel.name;
                //product.description = productModel.description;
                //product.pc_id = productModel.pc_id;
                //product.uom_id = productModel.uom_id;
                //product.d_id = productModel.d_id;
                //product.price = productModel.price;
                //product.imagepath = path;
                //context.Products.Add(product);
                //context.SaveChanges();
                //TempData["Message"] = "Added New Record in Product.";
                return RedirectToAction("AddNewRecipeDetail", "Recipe", new { recipeID = recipeId });
            }
            else
            {
                return RedirectToAction("Login", "Distributor");
            }
        }


        public ActionResult AddNewRecipeDetail(int recipeID)
        {
            Session["Username"] = "distributor";
            Session["Name"] = "test";
            Session["id"] = "1";

            if (Session["Username"] != null)
            {
                //ViewBag.Product = context.Products.ToList();
                //var ProductCategory = context.Product_Category.ToList();
                //ProductCategory.Add(new Product_Category() { name = "---Product Category---", pc_id = 0 });
                //ViewBag.ProductCategory = ProductCategory.OrderBy(pc => pc.pc_id);
                //RecipeDetails recipeDetails = new RecipeDetails(recipeID);
                //return View(recipeDetails);


                ViewBag.Product = context.Products.ToList();
                var ProductCategory = context.Product_Category.ToList();
                ProductCategory.Add(new Product_Category() { name = "---Product Category---", pc_id = 0 });
                ViewBag.ProductCategory = ProductCategory.OrderBy(pc => pc.pc_id);
                RecipeDetails recipeDetails = new RecipeDetails(recipeID);
                if (recipeDetails.imagepath != null)
                {
                    string UploadPath = ConfigurationManager.AppSettings["RecipeImagePath"].ToString();
                    recipeDetails.imagepath = Path.Combine("~\\Images\\Recipe", recipeDetails.imagepath);
                }
                return View(recipeDetails);

            }
            else
            {
                return RedirectToAction("Login", "Distributor");
            }
        }
        [HttpPost]
        public JsonResult UploadFiles(HttpPostedFileBase ImageFile, bool saveImage, int r_id)
        {

            if (ImageFile != null)
            {
                var recipe = context.Recipes.FirstOrDefault(r => r.r_id == r_id);

                string UploadPath = ConfigurationManager.AppSettings["RecipeImagePath"].ToString();
                if (recipe.imagepath != null)
                {
                    var fullPath = Path.Combine(UploadPath, recipe.imagepath);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }

                    var fileName = Guid.NewGuid().ToString() + Path.GetFileName(ImageFile.FileName);
                    var path = Path.Combine(UploadPath, fileName);
                    // store the uploaded file on the file system

                    ImageFile.SaveAs(path);

                    recipe.imagepath = fileName;

                    context.SaveChangesAsync();
                }
                else
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetFileName(ImageFile.FileName);
                    var path = Path.Combine(UploadPath, fileName);
                    // store the uploaded file on the file system

                    ImageFile.SaveAs(path);

                    recipe.imagepath = fileName;

                    context.SaveChangesAsync();
                }
                saveImage = saveImage && false;
                return Json(new { success = true, uploadMessage = "Recipe Image uploaded successfully." });

            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult UploadRecipe(string recipe)
        {
            if (Session["Username"] != null)
            {
                var _recipe = JsonSerializer.Deserialize<RecipeDetails>(recipe);
                var _recipeNew = context.Recipes.FirstOrDefault(a => a.r_id == _recipe.r_id);
                if (_recipeNew != null)
                {
                    _recipeNew.name = _recipe.name;
                    _recipeNew.description = _recipe.description;
                    _recipeNew.comment = _recipe.comment;
                    _recipeNew.Recipe_Step = _recipe.recipeStepDetails;
                    _recipeNew.modified_by = Session["Username"].ToString();
                    _recipeNew.modified_dtm = DateTime.Now;
                    List<Recipe_Step> recipe_Steps = _recipe.recipeStepDetails.ToList();
                    foreach (var recipe_Step in context.Recipe_Step.Where(rs=>rs.r_id==_recipe.r_id))
                    {
                        context.Recipe_Step.Remove(recipe_Step);
                    }
                    context.Recipe_Step.AddRange(recipe_Steps);
                    context.Recipes.Attach(_recipeNew);
                    context.Entry(_recipeNew).State = System.Data.Entity.EntityState.Modified;
                }
                if (context.SaveChanges() > 0)
                {
                    TempData["Message"] = "Recipe - " + _recipe.name + " is Added/Updated.";
                }

                //RecipeDetails recipeDetails = new RecipeDetails(recipeID);
                return Json(new { success = true, url = "https://localhost:44350/Distributor/Recipe" });
            }
            else
            {
                return RedirectToAction("Login", "Distributor");
            }
        }



        public ActionResult RemoveRecipe(int rid)
        {
            if (Session["Username"] != null)
            {
                Recipe r_todelete = context.Recipes.Where(p => p.r_id == rid).FirstOrDefault();
                List<Recipe_Step> rss_todelete = context.Recipe_Step.Where(r => r.r_id == rid).ToList();
                context.Recipe_Step.RemoveRange(rss_todelete);
                context.Recipes.Remove(r_todelete);
                context.SaveChanges();
                TempData["Message"] = "Recipe - " + r_todelete.name + " is Deleted.";
                return RedirectToAction("Recipe", "Distributor");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult EditRecipe(int rid)
        {
            if (Session["Username"] != null)
            {
                ViewBag.Product = context.Products.ToList();
                ViewBag.ProductCategory = context.Product_Category.ToList();
                RecipeDetails recipeDetails = new RecipeDetails(rid);
                return RedirectToAction("AddNewRecipeDetail", "Recipe", new { recipeID = rid });
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult AddNewRecipeDPartial()
        {
            const string ViewName = "~/Views/Recipe/_PartialPage1.cshtml";
            var a = PartialView(ViewName);
            return Json(new { partialView = a }, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region Customer

        public ActionResult RecipeHome()
        {
            var customer = context.Customers.FirstOrDefault(c => c.c_id == 2);
            Session["Username"] = customer.email_id;
            Session["Name"] = customer.full_name;
            Session["id"] = customer.c_id;
            Session["Cart"] = context.Carts.Where(c => c.c_id == customer.c_id).ToList();

            var recipes = context.Recipes.ToList();
            string UploadPath = ConfigurationManager.AppSettings["RecipeImagePath"].ToString();
            recipes.Where(r => r.imagepath != null).ToList().ForEach(r => { r.imagepath = Path.Combine("~\\Images\\Recipe", r.imagepath); });

            return View(recipes);
        }
        public ActionResult ViewRecipeDetails(int id)
        {
            if (context.Recipes.Any(r => r.r_id == id))
            {
                ViewBag.Product = context.Products.ToList();
                ViewBag.ProductCategory = context.Product_Category.ToList();
                Recipe recipe = context.Recipes.FirstOrDefault(r => r.r_id == id);
                if (recipe.imagepath != null)
                {
                    string UploadPath = ConfigurationManager.AppSettings["RecipeImagePath"].ToString();
                    recipe.imagepath = Path.Combine("~\\Images\\Recipe", recipe.imagepath);
                }
                List<Recipe_Step> recipe_Steps = context.Recipe_Step.Where(rs => rs.r_id == id).ToList();
                return View(recipe);
            }
            else
            {
                //redirect to error page -- 
                return View("Error");
            }


        }

        [HttpPost]
        public ActionResult RecipeHome(string search)
        {
            if (search == null || search == "")
            {
                var recipes = context.Recipes.ToList();
                string UploadPath = ConfigurationManager.AppSettings["RecipeImagePath"].ToString();
                recipes.Where(r => r.imagepath != null).ToList().ForEach(r => { r.imagepath = Path.Combine("~\\Images\\Recipe", r.imagepath); });

                return View(recipes);
            }
            else
            {
                var terms = search.Split(' ');
                var recipes = context.Recipes
                    .Where(q => terms.Any(term => q.name.Contains(term)))
                    .ToList();
                string UploadPath = ConfigurationManager.AppSettings["RecipeImagePath"].ToString();
                recipes.Where(r => r.imagepath != null).ToList().ForEach(r => { r.imagepath = Path.Combine("~\\Images\\Recipe", r.imagepath); });

                return View(recipes);
            }
        }

        public ActionResult AllIngredientCart(int RecipeID, int NoOfServing)
        {
            //Retrived All Product Required for Recipe
            List<Product> products = context.Recipe_Step.Where(rs => rs.r_id == RecipeID)
                                                        .Select(rs => rs.Product)
                                                        .ToList();

            //Created new Cart
            var c_id = Convert.ToInt32(Session["id"]);
            Cart cart = new Cart();
            string name = Session["Username"] + " - " + DateTime.Now.ToString();
            cart.name = name;
            cart.comment = name;
            cart.c_id = c_id;
            Cart cart_new = context.Carts.Add(cart); //check if id present
            context.SaveChanges();


            //Add All Required Product to cart
            Cart cart1 = context.Carts.FirstOrDefault(c => c.name == name && c.c_id == c_id);
            foreach (var product in products)
            {
                for (int i = 0; i < NoOfServing; i++)
                {
                    Cart_Product_Assoc cart_Product_Assoc = new Cart_Product_Assoc();
                    cart_Product_Assoc.cart_id = cart_new.cart_id;
                    cart_Product_Assoc.p_id = product.p_id;
                    context.Cart_Product_Assoc.Add(cart_Product_Assoc);
                }
            }

            context.SaveChanges();
            string tempMessage = "All Ingredient Added to Cart. Please <a href=https://localhost:44350/Customer/home>Click Here</a>";
            return Json(new { Success = true, tempMessage = tempMessage }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectedIngrdientCart(List<string> SelectedIngredient, int NoOfServing)
        {
            int RecipeID = 2;
            //Retrived All Product Required for Recipes
            List<Product> products = context.Recipe_Step.Where(rs => SelectedIngredient.Contains(rs.p_id.ToString()))
                                                        .Select(rs => rs.Product)
                                                        .ToList();

            //Created new Cart
            var c_id = Convert.ToInt32(Session["id"]);
            Cart cart = new Cart();
            string name = Session["Username"] + " - " + DateTime.Now.ToString();
            cart.name = name;
            cart.comment = name;
            cart.c_id = c_id;
            Cart cart_new = context.Carts.Add(cart); //check if id present
            context.SaveChanges();


            //Add All Required Product to cart
            Cart cart1 = context.Carts.FirstOrDefault(c => c.name == name && c.c_id == c_id);
            foreach (var product in products)
            {
                for (int i = 0; i < NoOfServing; i++)
                {
                    Cart_Product_Assoc cart_Product_Assoc = new Cart_Product_Assoc();
                    cart_Product_Assoc.cart_id = cart_new.cart_id;
                    cart_Product_Assoc.p_id = product.p_id;
                    context.Cart_Product_Assoc.Add(cart_Product_Assoc);
                }
            }

            context.SaveChanges();
            string tempMessage = "All Ingredient Added to Cart. Please <a href=https://localhost:44350/Customer/home>Click Here</a>";
            return Json(new { Success = true, tempMessage = tempMessage }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Contact developer
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.Layout = "~/Views/Shared/_RecipeLayoutPage1.cshtml";
            ViewBag.Title = "Contact";
            return View();
        }
        #endregion
        #region About 
        public ActionResult About()
        {
            ViewBag.Message = "Your About page.";
            ViewBag.Layout = "~/Views/Shared/_RecipeLayoutPage1.cshtml";
            ViewBag.Title = "About";
            return View();
        }
        #endregion
    }
}