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
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<CustomerDto, Customer>();
            //Also we need to initilize mapping on the application start event hence make changes in the global.asax.cs
        }
    }
}