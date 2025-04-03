using AutoMapper;
using Company.MVC.DAL.Models;
using Copmany.MVC.PL.Dto;

namespace Copmany.MVC.PL.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreatEmployeeDto, Employee>();
        }
    }
}
