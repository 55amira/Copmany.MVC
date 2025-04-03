using Company.MVC.BLL;
using Company.MVC.BLL.Interface;
using Company.MVC.BLL.Repositories;
using Company.MVC.DAL.Models;
using Copmany.MVC.PL.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace Copmany.MVC.PL.Controllers
{
    public class DepartmentController : Controller
    {
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfwork _unitOfwork;

        public DepartmentController (/*\*IDepartmentRepository departmentRepository*/ IUnitOfwork unitOfwork )
        {
            //_departmentRepository = departmentRepository;
            _unitOfwork = unitOfwork;
        }

        [HttpGet] 
        public async Task<IActionResult> Index()
        {
            var departments =await _unitOfwork.DepartmentRepository.GetALLAsync();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                var department = new Department
                {
                    Name = model.Name,
                    Code = model.Code,
                    CreateAt = model.CreateAt
                };
                 await _unitOfwork.DepartmentRepository.AddAsync(department);
                var Count = await _unitOfwork.CompleteAsync();
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Detelis (int ? Id,string viewname = "Detelis")
        {
            if (Id is null) return BadRequest("Invaild");

            var department =await _unitOfwork.DepartmentRepository.GetAsync(Id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, Message = $"Department With Id is Not Found {Id} " });

            return View(viewname, department);
        }

        [HttpGet]
        public async Task<IActionResult> Edit (int ? Id)
        {
            //if (Id is null) return BadRequest("Invaild");
            //
            //var department = _departmentRepository.Get(Id.Value);
            //if (department is null) return NotFound(new { StatusCode = 404, Message = $"Department With Id is Not Found {Id} " });

            return await Detelis(Id,"Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, Department department)
        {
            if (ModelState.IsValid)
            {
                if (id != department.Id) return BadRequest();
                 _unitOfwork.DepartmentRepository.Update(department);
                var Count = await _unitOfwork.CompleteAsync();
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(department);
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
        //        var Count = _departmentRepository.Update(department);
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
           //if (Id is null) return BadRequest("Invaild");
           //
           //var department = _departmentRepository.Get(Id.Value);
           //if (department is null) return NotFound(new { StatusCode = 404, Message = $"Department With Id is Not Found {Id} " });

            return await Detelis(Id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, Department department)
        {
            if (ModelState.IsValid)
            {
                if (id != department.Id) return BadRequest();
                 _unitOfwork.DepartmentRepository.Delete(department);
                var Count = await _unitOfwork.CompleteAsync();
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(department);
        }
    }
}
