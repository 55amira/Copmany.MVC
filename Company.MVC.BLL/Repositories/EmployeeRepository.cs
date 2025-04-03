using Company.MVC.BLL.Interface;
using Company.MVC.DAL.Data.Context;
using Company.MVC.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.MVC.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly CompanyDbContext _context;

        public EmployeeRepository(CompanyDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetNameAsync(string name)
        {
           // return _context.Employees.Where(E => E.Name.ToLower() .Contains(name.ToLower())).ToList();
               return await _context.Employees.Include(E=> E.Department).Where(E => E.Name.ToLower().Contains(name.ToLower())).ToListAsync();
        }
    }
}
