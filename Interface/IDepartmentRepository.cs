using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CompanyStructuresWebAPI.Model;
using CompanyStructuresWebAPI.Model.Dto;

namespace CompanyStructuresWebAPI.Interface
{
    public interface IDepartmentRepository
    {
        List<Department> GetDepartments();

        Department GetDepartmentById(int Id);

        int Delete(int Id);

        int Create(DepartmentDto department);

        int Update(Department department);
    }
}
