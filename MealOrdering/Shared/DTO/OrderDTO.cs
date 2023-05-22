using System;
using System.ComponentModel.DataAnnotations;

namespace MealOrdering.Shared.DTO
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid CreatedUserId { get; set; }
        public Guid SupplierId { get; set; }
        [MinLength(3)]
        [StringLength(10)]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ExpireDate { get; set; }
        public string CreatedUserFullName { get; set; }
        public string SupplierName { get; set; }

    }
}
