using AspNetCoreMvcECommerce.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AspNetCoreMvcECommerce.Areas.Admin.Models.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public List<SelectListItem> Categories { get; set; }
    }
}
