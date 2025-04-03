using Company.MVC.DAL.Models;
using Microsoft.Build.Framework;
using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace Copmany.MVC.PL.Dto
{
    public class CreatEmployeeDto
    {
        [Required(ErrorMessage = "Name is required !!")]
        public string Name { get; set; }
        [Range(20,60)]
        public int? Age { get; set; }
        [DataType(DataType.EmailAddress , ErrorMessage = "Email is Not Vaild")]
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime HiringDate { get; set; }
        public DateTime CreateAt { get; set; }
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public string? ImageName { get; set; }
        public IFormFile? Image { get; set; }
    }
}
