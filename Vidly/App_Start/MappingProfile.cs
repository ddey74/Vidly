using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Vidly.Models;
using Vidly.DTOs;

namespace Vidly.App_Start
{
    public class MappingProfile: Profile//From Automapper assembly
    {
        public MappingProfile()
        {
            //Domain to Dto
            Mapper.CreateMap<Customer, CustomerDto>();
            //Mapper.CreateMap<CustomerDto, Customer>();
            //Also we need to initilize mapping on the application start event hence make changes in the global.asax.cs


            //Domain to Dto
            //Added mapping between Movie and MovieDto
            Mapper.CreateMap<Movie, MovieDto>();
            //Mapper.CreateMap<MovieDto, Movie>();


            //Dto to Domain
            //while updating a particular record we will face issue due to mapping as Id will also be tried to be update
            //but Id is key property which should not be updated hen we have to ignore the Id to get updated
            Mapper.CreateMap<CustomerDto,Customer >().ForMember(c => c.Id, opt => opt.Ignore());
            Mapper.CreateMap<MovieDto, Movie>().ForMember(m => m.Id, opt => opt.Ignore());
        }
    }
}