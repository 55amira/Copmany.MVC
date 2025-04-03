using Company.MVC.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.MVC.BLL.Interface
{
    public interface IEmployeeRepository : IGenerucRepository<Employee>
    {
       List<Employee> GetName(string name);
    }
}
