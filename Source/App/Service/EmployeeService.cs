using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;
using Project.Repository;
using Project.ViewModel;

namespace Project.Service
{
    public interface IEmployeeService : IBaseService<Employee, EmployeeViewModel>
    {

    }

    public class EmployeeService : BaseService<Employee, EmployeeViewModel>, IEmployeeService
    {
        public EmployeeService(IEmployeeRepository repository) : base(repository)
        {

        }
    }
}
