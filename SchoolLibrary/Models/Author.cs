using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolLibrary.Models
{
    public partial class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }

        //[Remote("IsTagAvailble", "AuthorsController", ErrorMessage = "This author already exists.")]
        public string Name { get; set; } = null!;
        //public string LastName { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; }
    }
}
