using System;
using System.Collections.Generic;

namespace SchoolLibrary.Models
{
    public partial class Book
    {
        public Book()
        {
            LoanedBooks = new HashSet<LoanedBook>();
        }

        public int BookId { get; set; }
        public string Title { get; set; } = null!;
        public int AuthorId { get; set; }
        public DateTime PublicationDate { get; set; }
        public int GenreId { get; set; }

        public virtual Author Author { get; set; } = null!;
        public virtual Genre Genre { get; set; } = null!;
        public virtual ICollection<LoanedBook> LoanedBooks { get; set; }
    }
}
