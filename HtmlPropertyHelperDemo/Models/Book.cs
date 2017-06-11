using System;
using System.Collections.Generic;
using System.Linq;

namespace HtmlPropertyHelperDemo.Models
{
    public class Book
    {
        public Book()
        {
        }

        public Book(string title, string author, string datePublished)
        {
            this.Title = title;
            this.Author = author;
            this.DatePublished = datePublished;
        }

        public string Title
        {
            get;
            set;
        }

        public string Author
        {
            get;
            set;
        }


        public string DatePublished
        {
            get;
            set;
        }
    }
}
