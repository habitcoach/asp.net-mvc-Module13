using Microsoft.AspNetCore.Mvc;

namespace MVCapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

 
    public class EmployeeController : ControllerBase
    {
        private static List<Employee> employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "John Doe", Address = "123 Main St", Phone = "123-456-7890", DOJ = DateTime.Parse("2021-01-15") },
            new Employee { Id = 2, Name = "Jane Smith", Address = "456 Elm St", Phone = "987-654-3210", DOJ = DateTime.Parse("2020-11-10") }
        };

        [HttpGet]
       
        public ActionResult<IEnumerable<Employee>> GetEmployees()
        {
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployeeById(int id)
        {
            var employee = employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public ActionResult<Employee> CreateEmployee([FromBody] Employee employee)
        {
            employee.Id = employees.Max(e => e.Id) + 1;
            employees.Add(employee);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);

            //CreatedAtAction is used to set status code to 201 and it also give the route for the new item added so that client can use it
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] Employee updatedEmployee)
        {
            var existingEmployee = employees.FirstOrDefault(e => e.Id == id);
            if (existingEmployee == null)
            {
                return NotFound();
            }

            existingEmployee.Name = updatedEmployee.Name;
            existingEmployee.Address = updatedEmployee.Address;
            existingEmployee.Phone = updatedEmployee.Phone;
            existingEmployee.DOJ = updatedEmployee.DOJ;

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            employees.Remove(employee);
            return NoContent();
        }

    }
}
