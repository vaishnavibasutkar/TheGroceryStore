//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GroceryStoreMain.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Recipe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Recipe()
        {
            this.Recipe_Step = new HashSet<Recipe_Step>();
        }
    
        public int r_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string created_by { get; set; }
        public System.DateTime created_dtm { get; set; }
        public string modified_by { get; set; }
        public System.DateTime modified_dtm { get; set; }
        public string comment { get; set; }
        public string imagepath { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Recipe_Step> Recipe_Step { get; set; }
    }
}
