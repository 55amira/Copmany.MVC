using AutoMapper;
using Company.MVC.BLL.Interface;
using Company.MVC.BLL.Repositories;
using Company.MVC.DAL.Models;
using Copmany.MVC.PL.Dto;
using Copmany.MVC.PL.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Copmany.MVC.PL.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Index( string? SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                employees = await _unitOfwork.EmployeeRepository.GetALLAsync();
            }
            else
            {
                employees = await _unitOfwork.EmployeeRepository.GetNameAsync(SearchInput);
            }
            

            //ViewData["Message"] = "Hello from ViewData";

            //ViewBag.Message = "Hello from ViewBag";

            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var departments = await _unitOfwork.DepartmentRepository.GetALLAsync();
            ViewData["departments"] = departments;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                if(model.Image is not null)
                {
                   model.ImageName= DocumentSettings.UploadFile(model.Image, "images");
                }

                var employee = _mapper.Map<Employee>(model);
                await _unitOfwork.EmployeeRepository.AddAsync(employee);
                var count = await _unitOfwork.CompleteAsync();
                if (count > 0)
                {
                    TempData["Message"] = "Employee Created !!";
                    return RedirectToAction(nameof(Index));
                }

            }

            return View(model);
        }




        [HttpGet]
        public async Task<IActionResult> Detelis(int? Id, string viewname = "Detelis")
        {
            if (Id is null) return BadRequest("Invaild");

            var employee = await _unitOfwork.EmployeeRepository.GetAsync(Id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, Message = $"Employee With Id is Not Found {Id} " });

            return View(viewname, employee);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            var departments = await _unitOfwork.DepartmentRepository.GetALLAsync();
            ViewData["departments"] = departments;
            if (Id is null) return BadRequest("Invaild");
           
            var employee =await _unitOfwork.EmployeeRepository.GetAsync(Id.Value);
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
        public async Task<IActionResult> Edit([FromRoute] int id, Employee  model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest();
                
                 _unitOfwork.EmployeeRepository.Update(model);
                var Count = await _unitOfwork.CompleteAsync();
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
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id is null) return BadRequest("Invaild");
            
            var department = await _unitOfwork.EmployeeRepository.GetAsync(Id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, Message = $"Department With Id is Not Found {Id} " });

            return await Detelis(Id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, Employee model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest();
                _unitOfwork.EmployeeRepository.Delete(model);
                var Count = await _unitOfwork.CompleteAsync();
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        
    }
}
