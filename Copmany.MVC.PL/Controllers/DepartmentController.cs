using Company.MVC.BLL.Interface;
using Company.MVC.BLL.Repositories;
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
    }
}
