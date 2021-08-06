using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreMvcECommerce.Models
{
    [Table("Photo")]
    public class Photo
    {
        

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public int ProductId { get; set; }
        public bool Featured { get; set; }


        public virtual Product Product { get; set; }
    }
}
