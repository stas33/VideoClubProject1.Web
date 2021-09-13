using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoClubProject1.Core.Entities;
using VideoClubProject1.Web.Areas.Histories.Models;

namespace VideoClubProject1.Web.Mappings.Profiles
{
    public class HistoryProfile : AutoMapper.Profile
    {
        public HistoryProfile()
        {
            CreateMap<CreateRentalBindingModel, History>().ForMember(x => x.Titles, opt => opt.Ignore());
            CreateMap<UpdateRentalBindingModel, History>().ForMember(x => x.Titles, opt => opt.Ignore());
        }
    }
}