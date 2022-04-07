using System;
using System.Collections.Generic;

namespace SchoolLibrary.Models
{
    public partial class User
    {
        public User()
        {
            LoanedBooks = new HashSet<LoanedBook>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;

        public virtual ICollection<LoanedBook> LoanedBooks { get; set; }
    }
}
