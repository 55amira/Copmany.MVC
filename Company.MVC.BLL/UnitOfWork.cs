using Company.MVC.BLL.Interface;
using Company.MVC.BLL.Repositories;
using Company.MVC.DAL.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.MVC.BLL
{
    public class UnitOfWork : IUnitOfwork
    {
        public IDepartmentRepository DepartmentRepository { get; }

        public IEmployeeRepository EmployeeRepository { get; }
        public CompanyDbContext _context { get; }

        public UnitOfWork(CompanyDbContext context)
        {
            _context = context;
            DepartmentRepository = new DepartmentRepository(_context);
            EmployeeRepository = new EmployeeRepository(_context);

        }

        public async Task<int> CompleteAsync()
        {
           return await _context.SaveChangesAsync ();

        }

       

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
