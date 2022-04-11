using System;
using System.Collections.Generic;

namespace SchoolLibrary.Models
{
    public partial class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int AuthorId { get; set; }
        public int PublisherId { get; set; }
        public int Year { get; set; }
        public int GenreId { get; set; }
        public int ConditionId { get; set; }

        public virtual Author Author { get; set; } = null!;
        public virtual Condition Condition { get; set; } = null!;
        public virtual Genre Genre { get; set; } = null!;
        public virtual Publisher Publisher { get; set; } = null!;
    }
}
