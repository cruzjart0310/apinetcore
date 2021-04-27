using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }

        /* EF Relations */
        public ICollection<Movie> Movies { get; set; }
    }
}