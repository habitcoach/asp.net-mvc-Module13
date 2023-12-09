using MVCapiConsume.Models;

namespace MVCapiConsume.Service
{
    public class EmployeeService
    {
        private readonly HttpClient _httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            //https://localhost:7109/api/employee
            var response = await _httpClient.GetFromJsonAsync<List<Employee>>("employee");
            return response;
        }
        public async Task<HttpResponseMessage> CreateEmployeeAsync(Employee employee)
        {
                                  
            var response = await _httpClient.PostAsJsonAsync("employee", employee);
            return response;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<Employee>($"employee/{id}");
            return response;
        }
        public async Task<HttpResponseMessage> UpdateEmployeeAsync(int id, Employee updatedEmployee)
        {
            var response = await _httpClient.PutAsJsonAsync($"employee/{id}", updatedEmployee);
            return response;
        }
        public async Task<HttpResponseMessage> DeleteEmployeeAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"employee/{id}");
            return response;
        }
    }
}
