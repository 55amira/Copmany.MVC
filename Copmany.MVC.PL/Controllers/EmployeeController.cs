using Company.MVC.BLL.Interface;
using Company.MVC.BLL.Repositories;
using Company.MVC.DAL.Models;
using Copmany.MVC.PL.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Copmany.MVC.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }
        
        [HttpGet]
        public IActionResult Index( string? SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                employees = _employeeRepository.GetALL();
            }
            else
            {
                employees = _employeeRepository.GetName(SearchInput);
            }
            

            //ViewData["Message"] = "Hello from ViewData";

            //ViewBag.Message = "Hello from ViewBag";

            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var departments = _departmentRepository.GetALL();
            ViewData["departments"] = departments;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatEmployeeDto model)
        {
            Console.WriteLine("ModelState.IsValid: " + ModelState.IsValid);

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(model);
            }

            var employee = new Employee()
            {
                Name = model.Name,
                Address = model.Address,
                Email = model.Email,
                Age = model.Age,
                CreateAt = model.CreateAt,
                IsActive = model.IsActive,
                IsDelete = model.IsDelete,
                Salary = model.Salary,
                Phone = model.Phone,
                HiringDate = model.HiringDate,
                DepartmentId = model.DepartmentId,
            };

            var count = _employeeRepository.Add(employee);
            if (count > 0)
            {
                TempData["Message"] = "Employee Created !!";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }




        [HttpGet]
        public IActionResult Detelis(int? Id, string viewname = "Detelis")
        {
            if (Id is null) return BadRequest("Invaild");

            var employee = _employeeRepository.Get(Id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, Message = $"Employee With Id is Not Found {Id} " });

            return View(viewname, employee);
        }

        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            var departments = _departmentRepository.GetALL();
            ViewData["departments"] = departments;
            if (Id is null) return BadRequest("Invaild");
           
            var employee = _employeeRepository.Get(Id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, Message = $"Department With Id is Not Found {Id} " });
            var employeeDto = new CreatEmployeeDto()
            {
               
                Name = employee.Name,
                Address = employee.Address,
                Email = employee.Email,
                Age = employee.Age,
                CreateAt = employee.CreateAt,
                IsActive = employee.IsActive,
                IsDelete = employee.IsDelete,
                Salary = employee.Salary,
                Phone = employee.Phone,
                HiringDate = employee.HiringDate,
                DepartmentId = employee.DepartmentId,
            };
            return View(employeeDto);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee  model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest();
                //var employee = new Employee()
                //{
                //    Id = id,
                //    Name = model.Name,
                //    Address = model.Address,
                //    Email = model.Email,
                //    Age = model.Age,
                //    CreateAt = model.CreateAt,
                //    IsActive = model.IsActive,
                //    IsDelete = model.IsDelete,
                //    Salary = model.Salary,
                //    Phone = model.Phone,
                //    HiringDate = model.HiringDate,
                //};
                var Count = _employeeRepository.Update(model);
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit([FromRoute] int id, UpdateDepartmentDto model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var department = new Department()
        //        {
        //            Id = id,
        //            Code = model.Code,
        //            Name = model.Name,
        //            CreateAt = model.CreateAt,
        //        };

        //        if (id != department.Id) return BadRequest();
        //        var Count = _employeeRepository.Update(Department);
        //        if (Count > 0)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }
        //    return View(model);
        //}
        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            if (Id is null) return BadRequest("Invaild");
            
            var department = _employeeRepository.Get(Id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, Message = $"Department With Id is Not Found {Id} " });

            return Detelis(Id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Employee model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest();
                var Count = _employeeRepository.Delete(model);
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        
    }
}
