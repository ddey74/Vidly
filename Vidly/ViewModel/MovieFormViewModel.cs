using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModel
{
    public class MovieFormViewModel
    {
        

        //Insted of having Movie we will define a pure model with the required fields

        public IEnumerable<Genre> Genres { get; set; }
        //public Movie Movie { get; set; }
        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [Display(Name ="Genre")]
        public byte? GenreId { get; set; }


        [Display(Name ="Release Date")]
        [Required]
        public DateTime? ReleaseDate { get; set; }//we will be getting initial values when the movie form is loaded
                                                    //so to avoid default loading in form like 1/1/0001  we make datefield nullable


        [Display(Name = "Number in Stock")]
        [Range(1, 20)]
        [Required]
        public byte? NumberInStock { get; set; }

        public string Title
        {
            get
            {
                //if (Movie != null && Movie.Id != 0)
                //    return "Edit Movie";

                //return "New Movie";
                return Id != 0 ? "Edit Movie" : "New Movie";

            }
        }

        public MovieFormViewModel()
        {
            Id = 0;
        }

        public MovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
            GenreId = movie.GenreId;
        }
    }
}