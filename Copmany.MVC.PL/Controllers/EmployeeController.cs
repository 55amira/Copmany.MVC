using AutoMapper;
using Company.MVC.BLL.Interface;
using Company.MVC.BLL.Repositories;
using Company.MVC.DAL.Models;
using Copmany.MVC.PL.Dto;
using Copmany.MVC.PL.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Copmany.MVC.PL.Controllers
{
    public class EmployeeController : Controller
    {
        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfwork _unitOfwork;

        public EmployeeController(
            //IEmployeeRepository employeeRepository, 
            //IDepartmentRepository departmentRepository,
            IMapper mapper,
            IUnitOfwork unitOfwork
            )
        {
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
            _unitOfwork = unitOfwork;
        }
        
        [HttpGet]
        public IActionResult Index( string? SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                employees = _unitOfwork.EmployeeRepository.GetALL();
            }
            else
            {
                employees = _unitOfwork.EmployeeRepository.GetName(SearchInput);
            }
            

            //ViewData["Message"] = "Hello from ViewData";

            //ViewBag.Message = "Hello from ViewBag";

            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var departments = _unitOfwork.DepartmentRepository.GetALL();
            ViewData["departments"] = departments;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                if(model.Image is not null)
                {
                   model.ImageName= DocumentSettings.UploadFile(model.Image, "images");
                }

                var employee = _mapper.Map<Employee>(model);
                _unitOfwork.EmployeeRepository.Add(employee);
                var count = _unitOfwork.Complete();
                if (count > 0)
                {
                    TempData["Message"] = "Employee Created !!";
                    return RedirectToAction(nameof(Index));
                }

            }

            return View(model);
        }




        [HttpGet]
        public IActionResult Detelis(int? Id, string viewname = "Detelis")
        {
            if (Id is null) return BadRequest("Invaild");

            var employee = _unitOfwork.EmployeeRepository.Get(Id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, Message = $"Employee With Id is Not Found {Id} " });

            return View(viewname, employee);
        }

        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            var departments = _unitOfwork.DepartmentRepository.GetALL();
            ViewData["departments"] = departments;
            if (Id is null) return BadRequest("Invaild");
           
            var employee = _unitOfwork.EmployeeRepository.Get(Id.Value);
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
                 _unitOfwork.EmployeeRepository.Update(model);
                var Count = _unitOfwork.Complete();
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
            
            var department = _unitOfwork.EmployeeRepository.Get(Id.Value);
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
                _unitOfwork.EmployeeRepository.Delete(model);
                var Count = _unitOfwork.Complete();
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        
    }
}
