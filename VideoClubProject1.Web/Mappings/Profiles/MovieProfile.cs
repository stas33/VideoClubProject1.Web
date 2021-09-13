using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoClubProject1.Core.Entities;
using VideoClubProject1.Web.Areas.Movies.Models;

namespace VideoClubProject1.Web.Mappings.Profiles
{
    public class MovieProfile : AutoMapper.Profile
    {
        public MovieProfile()
        {
            CreateMap<MovieBindingModel, Movie>().ForMember(x => x.copies, opt => opt.Ignore());
        }
    }
}