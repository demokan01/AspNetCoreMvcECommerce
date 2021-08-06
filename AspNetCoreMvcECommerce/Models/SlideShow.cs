﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreMvcECommerce.Models
{
    [Table("SlideShow")]
    public class SlideShow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
}
