using System;
using System.Collections.Generic;

namespace SchoolLibrary.Models
{
    public partial class LoanedBook
    {
        public int LoanedBookId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime DateLoaned { get; set; }

        public virtual Book Book { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
