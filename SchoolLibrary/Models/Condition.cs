using System;
using System.Collections.Generic;

namespace SchoolLibrary.Models
{
    public partial class Condition
    {
        public Condition()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string BookCondition { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; }
    }
}
