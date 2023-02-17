﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MealOrdering.Server.Data.Models
{
    public class Supplier
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Name { get; set; }
        public string WebUrl { get; set; }
        public bool IsActive { get; set; }
    }
}
