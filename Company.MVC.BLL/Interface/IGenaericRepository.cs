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
        IEnumerable<T> GetALL();
        T? Get(int id);
        int Add(T model);
        int Update(T model);
        int Delete(T model);
    }
}
