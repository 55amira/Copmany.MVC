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
        public IActionResult Index()
        {
            var departments = _unitOfwork.DepartmentRepository.GetALL();
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
                 _unitOfwork.DepartmentRepository.Add(department);
                var Count = _unitOfwork.Complete();
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Detelis (int ? Id,string viewname = "Detelis")
        {
            if (Id is null) return BadRequest("Invaild");

            var department = _unitOfwork.DepartmentRepository.Get(Id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, Message = $"Department With Id is Not Found {Id} " });

            return View(viewname, department);
        }

        [HttpGet]
        public IActionResult Edit (int ? Id)
        {
            //if (Id is null) return BadRequest("Invaild");
            //
            //var department = _departmentRepository.Get(Id.Value);
            //if (department is null) return NotFound(new { StatusCode = 404, Message = $"Department With Id is Not Found {Id} " });

            return Detelis(Id,"Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Department department)
        {
            if (ModelState.IsValid)
            {
                if (id != department.Id) return BadRequest();
                 _unitOfwork.DepartmentRepository.Update(department);
                var Count = _unitOfwork.Complete();
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
        public IActionResult Delete(int? Id)
        {
           //if (Id is null) return BadRequest("Invaild");
           //
           //var department = _departmentRepository.Get(Id.Value);
           //if (department is null) return NotFound(new { StatusCode = 404, Message = $"Department With Id is Not Found {Id} " });

            return Detelis(Id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Department department)
        {
            if (ModelState.IsValid)
            {
                if (id != department.Id) return BadRequest();
                 _unitOfwork.DepartmentRepository.Delete(department);
                var Count = _unitOfwork.Complete();
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(department);
        }
    }
}
