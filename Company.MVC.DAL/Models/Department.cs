using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.MVC.DAL.Models
{
    public class Department :BaseEntity
    {
        
        public string Name { get; set; }
        public int Code { get; set; }

        public DateTime CreateAt { get; set; }
    }
}
