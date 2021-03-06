using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreMvcECommerce.Models
{
    [Table("Category")]
    public class Category
    {
        public Category()
        {
            InverseParents = new HashSet<Category>();
            Products = new HashSet<Product>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }

        public int? ParentId { get; set; }

        public virtual Category Parent { get; set; }
        public virtual ICollection<Category> InverseParents { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}
