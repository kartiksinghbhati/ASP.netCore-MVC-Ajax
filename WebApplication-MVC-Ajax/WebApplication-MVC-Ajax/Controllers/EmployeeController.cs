using Microsoft.AspNetCore.Mvc;
using WebApplication_MVC_Ajax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebApplication_MVC_Ajax.Controllers
{
    public class EmployeeController : Controller
    {
        private static List<Employee> employees = new List<Employee>();

        // GET: Employee
        public IActionResult Index()
        {
            return View(employees);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee employee, IFormFile photo)
        {
            try
            {
                // First Name validation
                if (string.IsNullOrEmpty(employee.FirstName))
                    throw new Exception("Please enter first name");

                if (employee.FirstName.Length > 50)
                    throw new Exception("Invalid first name. Length sholud be less then 50");

                if (!Regex.IsMatch(employee.FirstName, "^[a-zA-Z]+$"))
                    throw new Exception("Invalid first name. Only alphabetical characters are allowed.");

                // Last Name validation
                if (string.IsNullOrEmpty(employee.LastName))
                    throw new Exception("Please enter last name");

                if (employee.LastName.Length > 50)
                    throw new Exception("Invalid last name. Length sholud be less then 50");

                if (!Regex.IsMatch(employee.LastName, "^[a-zA-Z]+$"))
                    throw new Exception("Invalid last name. Only alphabetical characters are allowed.");

                // Age validation
                if (employee.DateOfBirth > DateTime.Now.AddYears(-18))
                    throw new Exception("Invalid date of birth.");

                // Salary validation
                if (employee.Salary < 0)
                    throw new Exception("Invalid salary.");               

                // Gender validation
                if (string.IsNullOrEmpty(employee.Gender))
                    throw new Exception("Please select Gender");

                // IsActive validation
                if (string.IsNullOrEmpty(employee.IsActive))
                    throw new Exception("Please select Status");

                // Converting Photo to base64 string and storing it
                if (photo != null && photo.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        photo.CopyTo(ms);
                        byte[] photoBytes = ms.ToArray();
                        employee.PhotoBase64 = Convert.ToBase64String(photoBytes);
                    }
                }

                // Add the employee to the list
                employee.Id = employees.Count + 1;
                employees.Add(employee);

                return Json(new { success = true, message = "Employee added successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while adding the employee: " + ex.Message });
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            Employee employee = employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Employee updatedEmployee, IFormFile updatedPhoto)
        {
            try
            {
                Employee employee = employees.FirstOrDefault(e => e.Id == id);
                if (employee == null)
                    return NotFound();
                
                // First Name validation
                if (string.IsNullOrEmpty(updatedEmployee.FirstName))
                    throw new Exception("Please enter first name");

                if (updatedEmployee.FirstName.Length > 50)
                    throw new Exception("Invalid first name. Length sholud be less then 50");

                if (!Regex.IsMatch(updatedEmployee.FirstName, "^[a-zA-Z]+$"))
                    throw new Exception("Invalid first name. Only alphabetical characters are allowed.");

                // Last Name validation
                if (string.IsNullOrEmpty(updatedEmployee.LastName))
                    throw new Exception("Please enter last name");

                if (updatedEmployee.LastName.Length > 50)
                    throw new Exception("Invalid last name. Length sholud be less then 50");

                if (!Regex.IsMatch(updatedEmployee.LastName, "^[a-zA-Z]+$"))
                    throw new Exception("Invalid last name. Only alphabetical characters are allowed.");

                // Age validation 
                if (updatedEmployee.DateOfBirth > DateTime.Now.AddYears(-18))
                    throw new Exception("Invalid date of birth.");

                // Salary validation
                if (updatedEmployee.Salary < 0)
                    throw new Exception("Invalid salary.");

                // Gender validation 
                if (string.IsNullOrEmpty(employee.Gender))
                    throw new Exception("Please select Gender");

                // IsActive validation
                if (string.IsNullOrEmpty(employee.IsActive))
                    throw new Exception("Please select Status");

                // Converting Photo to base64 string and storing it
                if (updatedPhoto != null && updatedPhoto.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        updatedPhoto.CopyTo(ms);
                        byte[] photoBytes = ms.ToArray();
                        employee.PhotoBase64 = Convert.ToBase64String(photoBytes);
                    }
                }
                

                // Update the employee details
                employee.FirstName = updatedEmployee.FirstName;
                employee.LastName = updatedEmployee.LastName;
                employee.DateOfBirth = updatedEmployee.DateOfBirth;
                employee.Salary = updatedEmployee.Salary;
                employee.Gender = updatedEmployee.Gender;
                employee.IsActive = updatedEmployee.IsActive;
                employee.Hobbies = updatedEmployee.Hobbies;

                return Json(new { success = true, message = "Employee updated successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while updating the employee: " + ex.Message });
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            Employee employee = employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Employee employee)
        {
            try
            {
                employees.RemoveAll(e => e.Id == id);
                return Json(new { success = true, message = "Employee deleted successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while deleting the employee: " + ex.Message });
            }
        }

        // GET: Employee/Read/5
        public ActionResult Read(int id)
        {
            Employee employee = employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        // GET: Employee/GetAll
        public ActionResult GetAll()
        {
            return Json(employees);
        }
    }
}


