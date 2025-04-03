using Company.MVC.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.MVC.BLL.Interface
{
    public interface IGenerucRepository <T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetALLAsync();
        Task<T?> GetAsync(int id);
        Task AddAsync(T model);
        void Update(T model);
        void Delete(T model);
    }
}
