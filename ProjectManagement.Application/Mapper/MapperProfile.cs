using AutoMapper;
using ProjectManagement.Application.ViewModels;
using ProjectManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Application.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Project, ProjectViewModel>().ReverseMap();
            CreateMap<Split, SplitViewModel>().ReverseMap();
            CreateMap<Job, JobViewModel>().ReverseMap();
            CreateMap<JobLog, JobLogViewModel>().ReverseMap();
        }
    }
}
