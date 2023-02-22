using System;
using System.Collections.Generic;
using System.Text;

namespace MealOrdering.Shared.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMailAddress { get; set; }
        public bool IsActive { get; set; }

        public string FullName => $"{FirstName}  {LastName}";

    }
}
