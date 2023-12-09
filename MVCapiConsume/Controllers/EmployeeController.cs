using Microsoft.AspNetCore.Mvc;
using MVCapiConsume.Models;
using MVCapiConsume.Service;

namespace MVCapiConsume.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return View(employees);
        }

        //Create
        #region CREATE
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                // Call the API to create a new employee
                var response = await _employeeService.CreateEmployeeAsync(employee);

                if (response.IsSuccessStatusCode)
                {
                    // Employee creation successful, redirect to the employee list page or show a success message
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Handle API error response (e.g., validation errors) here
                    ModelState.AddModelError(string.Empty, "API Error: Unable to create employee.");
                }
            }
            return View(employee);
        }
        #endregion

        //Edit
        #region Edit

        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Call the API to update the employee
                var response = await _employeeService.UpdateEmployeeAsync(id, employee);

                if (response.IsSuccessStatusCode)
                {
                    // Employee update successful, redirect to the employee list page or show a success message
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Handle API error response (e.g., validation errors) here
                    ModelState.AddModelError(string.Empty, "API Error: Unable to update employee.");
                }
            }
            return View(employee);
        }
        #endregion

        //Delete
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        public async Task<IActionResult> JsCall()
        {
           
            return View();
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Call the API to delete the employee
            var response = await _employeeService.DeleteEmployeeAsync(id);

            if (response.IsSuccessStatusCode)
            {
                // Employee deletion successful, redirect to the employee list page or show a success message
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Handle API error response (e.g., employee not found) here
                ModelState.AddModelError(string.Empty, "API Error: Unable to delete employee.");
                return View("Delete", await _employeeService.GetEmployeeByIdAsync(id));
            }
        }

    }
}
