using System;
using System.Collections.Generic;

namespace SchoolLibrary.Models
{
    public partial class LoanedBook
    {
        public int LoanedBookId { get; set; }
        public int BookId { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime DateLoaned { get; set; }

        public virtual Book Book { get; set; } = null!;
    }
}
