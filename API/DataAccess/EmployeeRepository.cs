using API.Model;

namespace API.DataAccess;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ConnectionContext _context = new ConnectionContext();
    
    public void Add(Employee employee)
    {
        _context.Employees.Add(employee);
        _context.SaveChanges();
    }

    public List<Employee> Get()
    {
        return _context.Employees.ToList();
    }
}