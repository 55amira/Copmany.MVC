﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.MVC.DAL.Models
{
    internal class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }

        public DateTime CreateAt { get; set; }
    }
}
