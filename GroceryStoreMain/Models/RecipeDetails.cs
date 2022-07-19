using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryStoreMain.Models
{

    public class RecipeDetails
    {
        public RecipeDetails()
        {

        }

        GroceryStoreDBEntities context;
        public RecipeDetails(int r_id)
        {
            context = new GroceryStoreDBEntities();
            var r = context.Recipes.FirstOrDefault(re => re.r_id == r_id);
            this.r_id = r.r_id;
            this.name = r.name;
            this.description = r.description;
            this.comment = r.comment;
            this.created_by = r.created_by;
            this.created_dtm = r.created_dtm;
            this.modified_by = r.modified_by;
            this.modified_dtm = r.modified_dtm;
            this.imagepath = r.imagepath;
            this.recipeStepDetails= context.Recipe_Step.Where(rs=>rs.r_id==this.r_id).ToList();

        }
        public int r_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string created_by { get; set; }
        public System.DateTime created_dtm { get; set; }
        public string modified_by { get; set; }
        public System.DateTime modified_dtm { get; set; }
        public string comment { get; set; }
        public List<Recipe_Step> recipeStepDetails { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
        public string imagepath { get; set; }
    }
}