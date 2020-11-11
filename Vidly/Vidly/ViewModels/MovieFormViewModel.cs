using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }

        // individuel movie properties to solve initialising date (releasedate, number in stock) in form
        public int? Id { get; set; }
        [Required]
        public string Title { get; set; } // already nullable
        [Required]
        public DateTime? ReleaseDate { get; set; }
        //public DateTime AddDate { get; set; } not captured in the form
        [Required]
        [Display(Name = "Number in Stock")]
        [Range(1, 20)]
        public int? Stock { get; set; }

        //NAV PROP
        [Display(Name = "Genre")]
        public byte? GenreId { get; set; } // ook byte zetten, anders twee ID
        //public Genre Genre { get; set; }

        public string FormTitle
        {
            get
            {
                return (Id != null) ? "Edit Movie" : "New Movie";
            }
        }

        //used when creating a new movie
        public MovieFormViewModel()
        {
            Id = 0; // to populate hidden field
        }

        //used when editing a existing one
        public MovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            GenreId = movie.GenreId;
            ReleaseDate = movie.ReleaseDate;
            Stock = movie.Stock;
            Title = movie.Title;
        }
    }
}
