using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClubProject1.Core.Enumerations;

namespace VideoClubProject1.Core.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public MovieType Type { get; set; }
        public List<PhysicalCopy> copies { get; set; }

        public Movie(int id, string title, string description, MovieType type)
        {
            Id = id;
            Title = title;
            Description = description;
            Type = type;
        }

        public Movie()
        {

        }
    }
}
