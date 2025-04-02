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

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var employees = _employeeRepository.GetALL();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
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
            };

            var count = _employeeRepository.Add(employee);
            if (count > 0)
            {
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
            if (Id is null) return BadRequest("Invaild");
            
            var department = _employeeRepository.Get(Id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, Message = $"Department With Id is Not Found {Id} " });

            return Detelis(Id, "Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest();
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
