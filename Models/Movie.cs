using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }

        /* EF Relations */
        public ICollection<Actor> Actors { get; set; }
    }
}