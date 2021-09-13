using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoClubProject1.Core.Enumerations;

namespace VideoClubProject1.Web.Areas.Movies.Models
{
    public class MovieBindingModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public MovieType Type { get; set; }

        public MovieBindingModel(int id, string title, string description, MovieType type)
        {
            Id = id;
            Title = title;
            Description = description;
            Type = type;
        }

        public MovieBindingModel()
        {
        }
    }
}