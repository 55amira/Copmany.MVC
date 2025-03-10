using Company.MVC.BLL.Interface;
using Company.MVC.BLL.Repositories;
using Company.MVC.DAL.Models;
using Copmany.MVC.PL.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Copmany.MVC.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController (IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet] 
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetALL();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                var department = new Department
                {
                    Name = model.Name,
                    Code = model.Code,
                    CreateAt = model.CreateAt
                };
                var Count = _departmentRepository.Add(department);
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }
    }
}
