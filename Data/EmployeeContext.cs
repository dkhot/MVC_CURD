using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class EmployeeContext : DbContext, IEmployeeContext
    {
        static EmployeeContext()
        {
            Database.SetInitializer<EmployeeContext>(null);
        }

        public EmployeeContext()
            : base("name=EmployeeContext")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }


        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public void CommiteChanges()
        {
            SaveChanges();
        }
    }

    public interface IUnitofWork : IDisposable
    {
        void CommiteChanges();
        void Dispose();

    }

    public interface IEmployeeContext : IUnitofWork
    {
        DbSet<Employee> Employees { get; set; }
        DbSet<Department> Departments { get; set; }
    }

    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Department> Departments { get; set; }
    }

    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

    }
}
