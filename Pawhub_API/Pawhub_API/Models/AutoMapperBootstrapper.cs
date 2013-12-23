using AutoMapper;
using blastic.pawhub.models.LostAndFound;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pawhub_API.Models
{
    public static class AutoMapperBootstrapper
    {
        public static void Bootstrap()
        {
            Mapper.CreateMap<Abuse, Abuse>();
            Mapper.CreateMap<ContactInfo, ContactInfo>();
            Mapper.CreateMap<Found, Found>();
            Mapper.CreateMap<Lost, Lost>();
            Mapper.CreateMap<Report, Report>();
            Mapper.CreateMap<ReportDetail, ReportDetail>();
            Mapper.CreateMap<Resque, Resque>();
            Mapper.CreateMap<UserAlert, UserAlert>();
        }
    }
}