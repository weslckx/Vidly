﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ReleaseDate { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime AddDate { get; set; }
        [Required]
        [Display(Name ="Number in Stock")]
        [Range(1,20)]
        public int Stock { get; set; }
        public byte NumberAvailable { get; set; }

        //NAV PROP
        [Display(Name ="Genre")] 
        public byte GenreId { get; set; } // ook byte zetten, anders twee ID
        public Genre Genre { get; set; }


    }
}