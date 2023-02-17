using System;
using System.Collections.Generic;
using System.Text;

namespace MealOrdering.Server.Data.Models
{
    public class Orders
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid CreateUserId { get; set; }
        public Guid SupplierId { get; set; }
        public string Name { get; set; }
        public string Desctription { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
