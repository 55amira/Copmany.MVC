﻿using System.ComponentModel.DataAnnotations;

namespace Copmany.MVC.PL.Dto
{
    public class UpdateDepartmentDto
    {
         [Required(ErrorMessage = "Code is required")]
        public int Code { get; set; }

        [Required(ErrorMessage= "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "CreateAt is required")]
        public DateTime CreateAt { get; set; }
    }
}
