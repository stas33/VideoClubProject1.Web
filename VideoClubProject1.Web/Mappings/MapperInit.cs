using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoClubProject1.Web.Mappings.Profiles;

namespace VideoClubProject1.Web.Mappings
{
    public class MapperInit 
    {
        public static IMapper Init()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MovieProfile());
                cfg.AddProfile(new HistoryProfile());
            });
            configuration.AssertConfigurationIsValid();
            return configuration.CreateMapper();
        }
    }
}