﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VseTShirts.DB.Models
{
    public class Collection
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Product> Products { get; set; }
    }
}
